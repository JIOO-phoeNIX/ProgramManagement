using Microsoft.Azure.Cosmos;

namespace ProgramManagement.Persistence.Db;

public class CosmosDbService : ICosmosDbService
{
    private readonly Container _container;

    public CosmosDbService(CosmosClient cosmosDbClient, string databaseName, string containerName)
    {
        _container = cosmosDbClient.GetContainer(databaseName, containerName);
    }

    public Container GetContainer()
    {
        return _container;
    }
}