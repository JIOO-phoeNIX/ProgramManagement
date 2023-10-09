// See https://aka.ms/new-console-template for more information
//Console.WriteLine("Hello, World!"); 

using Microsoft.AspNetCore.Mvc;
using ProgramManagement.Api.Attribute;
using ProgramManagement.Api.Middleware;
using ProgramManagement.Core.Helpers;
using Serilog;

var MyAllowSpecificOrigins = "ProgramManagement";

var builder = WebApplication.CreateBuilder(args);

//Serilog setup
var logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .Enrich.FromLogContext()
    .CreateLogger();
builder.Logging.ClearProviders();
builder.Logging.AddSerilog(logger);

builder.Services.AddScoped<ValidationFilterAttribute>();
//Add db and services dependencies
builder.Services.AddCoreServices();
builder.Services.AddDbDependencies(builder.Configuration);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(p => p.AddPolicy(MyAllowSpecificOrigins, builder =>
{
    builder.WithOrigins("*").AllowAnyMethod().AllowAnyHeader();
}));

//enable model attribute validation  
builder.Services.Configure<ApiBehaviorOptions>(options
    => options.SuppressModelStateInvalidFilter = true);
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());


var app = builder.Build();

//log request/response, this can be commented to stop it.
app.UseMiddleware<RequestLoggingMiddleware>();

app.UseSwagger();
app.UseSwaggerUI();

app.UseCors(MyAllowSpecificOrigins);

app.UseHttpsRedirection();

app.MapControllers();

app.Run();