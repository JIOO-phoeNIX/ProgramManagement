using ProgramManagement.Persistence.Db;
using ProgramManagement.Persistence.Entity;
using ProgramManagement.Persistence.Repository.Interfaces;

namespace ProgramManagement.Persistence.Repository.Services;

public class ApiCallLogRepository : BaseRepository<ApiCallLog>, IApiCallLogRepository<ApiCallLog>
{
    private readonly ICosmosDbService _cosmosDbService;

    public ApiCallLogRepository(ICosmosDbService cosmosDbService) : base(cosmosDbService)
    {
        _cosmosDbService = cosmosDbService;
    }
}