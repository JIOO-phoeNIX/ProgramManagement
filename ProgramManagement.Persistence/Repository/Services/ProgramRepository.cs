using ProgramManagement.Persistence.Db;
using ProgramManagement.Persistence.Entity;
using ProgramManagement.Persistence.Repository.Interfaces;

namespace ProgramManagement.Persistence.Repository.Services;

public class ProgramRepository : BaseRepository<Program>, IProgramRepository<Program>
{
    private readonly ICosmosDbService _cosmosDbService;

    public ProgramRepository(ICosmosDbService cosmosDbService) : base(cosmosDbService)
    {
        _cosmosDbService = cosmosDbService;
    }
}