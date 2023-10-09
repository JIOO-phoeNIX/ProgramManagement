using ProgramManagement.Core.Interfaces;
using ProgramManagement.Persistence.Entity;
using System.Diagnostics;
using System.Security.Claims;
using System.Text;

namespace ProgramManagement.Api.Middleware;

/// <summary>
/// Use this to log the API Call request/response.
/// </summary>
public class RequestLoggingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<RequestLoggingMiddleware> _logger;

    public RequestLoggingMiddleware(RequestDelegate pNext, ILogger<RequestLoggingMiddleware> logger)
    {
        _next = pNext;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext httpRequest, IServiceProvider serviceProvider)
    {
        try
        {
            //only log http requests
            if (httpRequest.Request.Path.StartsWithSegments(new PathString("/api")))
            {
                var ipAddress = httpRequest.Request.Headers["X-Forwarded-For"].ToString();
                string requestBody;
                string responseBody;
                DateTime requestTime = DateTime.UtcNow;
                Stopwatch stopWatch;
                httpRequest.Request.EnableBuffering();

                using (StreamReader reader = new StreamReader(httpRequest.Request.Body, encoding: Encoding.UTF8, detectEncodingFromByteOrderMarks: false, leaveOpen: true))
                {
                    requestBody = await reader.ReadToEndAsync();
                    httpRequest.Request.Body.Position = 0;
                }

                Stream originalResponseStream = httpRequest.Response.Body;
                string userEmail = string.Empty;
                await using (MemoryStream responseStream = new MemoryStream())
                {
                    httpRequest.Response.Body = responseStream;

                    stopWatch = Stopwatch.StartNew();
                    try
                    {
                        await _next(httpRequest);
                    }
                    catch (Exception e)
                    {
                        _logger.LogError(" responseStream " + e.Message);
                    }
                    finally { stopWatch.Stop(); }

                    httpRequest.Response.Body.Seek(0, SeekOrigin.Begin);
                    responseBody = await new StreamReader(httpRequest.Response.Body).ReadToEndAsync();
                    httpRequest.Response.Body.Seek(0, SeekOrigin.Begin);

                    try
                    {
                        await responseStream.CopyToAsync(originalResponseStream);
                        userEmail = httpRequest.User.Identity.IsAuthenticated ? httpRequest.User.Claims.First(c => c.Type == ClaimTypes.Name).Value : string.Empty;
                    }
                    catch (Exception e)
                    {
                        _logger.LogError("Error getting username RequestLoggingMiddleware : " + e.Message);
                    }
                }

                using var scope = serviceProvider.CreateScope();
                ILoggingInterface _loggingService = scope.ServiceProvider.GetRequiredService<ILoggingInterface>();

                if (httpRequest.Request.Method.Trim() != "OPTIONS")
                {
                    var result = await SaveLog(_loggingService, requestTime, stopWatch.ElapsedMilliseconds, httpRequest.Response.StatusCode, httpRequest.Request.Method,
                    httpRequest.Request.Path, httpRequest.Request.QueryString.ToString(), requestBody, responseBody, ipAddress, userEmail);
                }
            }
            else
            {
                await _next(httpRequest);
            }
        }
        catch (Exception ex)
        {
            _logger.LogError("RequestLoggingMiddleware InvokeAsync : " + ex.Message + " \nStack trace = " + ex.StackTrace);
        }
    }
    private async Task<ApiCallLog> SaveLog(ILoggingInterface _loggingService, DateTime requestTime, long responseMillis, int statusCode, string method, string path, string queryString,
        string requestBody, string responseBody, string requestIp, string email)
    {
        string localEmail = string.Empty;


        if (requestBody.Length > 950)
        {
            requestBody = $"(Truncated) {requestBody.Substring(0, 950)}";
        }

        if (responseBody.Length > 450)
        {
            responseBody = $"(Truncated to 3000 chars) {responseBody.Substring(0, 450)}";
        }

        if (queryString.Length > 100)
        {
            queryString = $"(Truncated to 3000 chars) {queryString.Substring(0, 100)}";
        }

        if (string.IsNullOrWhiteSpace(email))
        {
            email = localEmail;
        }

        var apiLog = new ApiCallLog
        {
            RequestTime = requestTime,
            ResponseMillis = responseMillis,
            StatusCode = statusCode,
            Method = method,
            Path = path,
            QueryString = queryString,
            RequestBody = requestBody,
            ResponseBody = responseBody,
            ResponseTime = DateTime.UtcNow,
            RequestIp = requestIp,
            UserEmail = email
        };

        await _loggingService.SaveApiLog(apiLog);

        return apiLog;
    }
}
