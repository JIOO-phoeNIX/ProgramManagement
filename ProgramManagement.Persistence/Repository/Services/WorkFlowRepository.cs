using ProgramManagement.Persistence.Db;
using ProgramManagement.Persistence.Entity;
using ProgramManagement.Persistence.Repository.Interfaces;

namespace ProgramManagement.Persistence.Repository.Services;

public class WorkFlowRepository : BaseRepository<WorkFlow>, IWorkFlowRepository<WorkFlow>
{
    private readonly ICosmosDbService _cosmosDbService;

    public WorkFlowRepository(ICosmosDbService cosmosDbService) : base(cosmosDbService)
    {
        _cosmosDbService = cosmosDbService;
    }
}
