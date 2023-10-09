using ProgramManagement.Core.Interfaces;
using ProgramManagement.Core.Services;
using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProgramManagement.Persistence.Db;
using ProgramManagement.Persistence.Repository.Services;
using ProgramManagement.Persistence.Repository.Interfaces;
using ProgramManagement.Persistence.Entity;

namespace ProgramManagement.Core.Helpers;

public static class DepedencyRegistration
{
    /// <summary>
    /// Add core services dependencies
    /// </summary>
    /// <param name="services"></param>
    public static void AddCoreServices(this IServiceCollection services)
    {
        //Core services
        services.AddScoped<ILoggingInterface, LoggingService>();
        services.AddScoped<IProgramInterface, ProgramService>();
        services.AddScoped<IGeneralInterface, GeneralService>();
    }

    /// <summary>
    /// Add db dependencies
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configuration"></param>
    public static void AddDbDependencies(this IServiceCollection services, IConfiguration configuration)
    {
        //Data access
        services.AddSingleton<ICosmosDbService>(InitializeCosmosClientInstanceAsync(configuration.GetSection("CosmosDb")).GetAwaiter().GetResult());
        services.AddScoped<IApiCallLogRepository<ApiCallLog>, ApiCallLogRepository>();
        services.AddScoped<IProgramRepository<Program>, ProgramRepository>();
        services.AddScoped<IApplicationFormRepository<ApplicationForm>, ApplicationFormRepository>();
        services.AddScoped<IWorkFlowRepository<WorkFlow>, WorkFlowRepository>();
    }

    /// <summary>
    /// Set up the db and container
    /// </summary>
    /// <param name="configurationSection"></param>
    /// <returns></returns>
    private static async Task<CosmosDbService> InitializeCosmosClientInstanceAsync(IConfigurationSection configurationSection)
    {
        var databaseName = configurationSection["DatabaseName"];
        var containerName = configurationSection["ContainerName"];
        var url = configurationSection["Url"];
        var key = configurationSection["Key"];
        var client = new CosmosClient(url, key);
        var database = await client.CreateDatabaseIfNotExistsAsync(databaseName);
        await database.Database.CreateContainerIfNotExistsAsync(containerName, "/id");
        var cosmosDbService = new CosmosDbService(client, databaseName, containerName);
        return cosmosDbService;
    }
}