using ProgramManagement.Persistence.Entity;

namespace ProgramManagement.Core.Interfaces;

public interface ILoggingInterface
{
    public Task SaveApiLog(ApiCallLog apiCallLog);
}