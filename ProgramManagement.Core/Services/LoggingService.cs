using Microsoft.Extensions.Logging;
using ProgramManagement.Core.Interfaces;
using ProgramManagement.Persistence.Entity;
using ProgramManagement.Persistence.Repository.Interfaces;

namespace ProgramManagement.Core.Services;

public class LoggingService : ILoggingInterface
{
    private readonly ILogger<LoggingService> _logger;
    private readonly IApiCallLogRepository<ApiCallLog> _apiCallLogRepository;
    private readonly IGeneralInterface _generalService;

    public LoggingService(ILogger<LoggingService> logger, IApiCallLogRepository<ApiCallLog> apiCallLogRepository, IGeneralInterface generalService)
    {
        _logger = logger;
        _apiCallLogRepository = apiCallLogRepository;
        _generalService = generalService;
    }

    public async Task SaveApiLog(ApiCallLog apiCallLog)
    {
        try
        {
            apiCallLog.id = _generalService.GenerateUniqueId();
            await _apiCallLogRepository.AddAsync(apiCallLog, apiCallLog.id);
        }
        catch (Exception ex)
        {
            _logger.LogError("Error in SaveApiLog message = " + ex.Message + " \nStack trace = " + ex.StackTrace);
        }
    }
}
