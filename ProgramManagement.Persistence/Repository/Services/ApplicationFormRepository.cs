using ProgramManagement.Persistence.Db;
using ProgramManagement.Persistence.Entity;
using ProgramManagement.Persistence.Repository.Interfaces;

namespace ProgramManagement.Persistence.Repository.Services;

public class ApplicationFormRepository : BaseRepository<ApplicationForm>, IApplicationFormRepository<ApplicationForm>
{
    private readonly ICosmosDbService _cosmosDbService;

    public ApplicationFormRepository(ICosmosDbService cosmosDbService) : base(cosmosDbService)
    {
        _cosmosDbService = cosmosDbService;
    }
}