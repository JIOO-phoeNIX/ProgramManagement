using Microsoft.Azure.Cosmos;
using ProgramManagement.Persistence.Db;
using ProgramManagement.Persistence.Repository.Interfaces;

namespace ProgramManagement.Persistence.Repository.Services;

public class BaseRepository<T> : IBaseRepository<T> where T : class
{
    private readonly ICosmosDbService _cosmosDbService;

    public BaseRepository(ICosmosDbService cosmosDbService)
    {
        _cosmosDbService = cosmosDbService;
    }

    public async Task AddAsync(T item, string id)
    {
        await _cosmosDbService.GetContainer().CreateItemAsync(item, new PartitionKey(id));
    }

    public async Task DeleteAsync(string id)
    {
        await _cosmosDbService.GetContainer().DeleteItemAsync<T>(id, new PartitionKey(id));
    }

    public async Task<T> GetAsync(string id)
    {
        try
        {
            var response = await _cosmosDbService.GetContainer().ReadItemAsync<T>(id, new PartitionKey(id));
            return response.Resource;
        }
        catch (CosmosException)
        {
            return null;
        }
    }

    public async Task<IEnumerable<T>> GetMultipleAsync(string queryString)
    {
        var query = _cosmosDbService.GetContainer().GetItemQueryIterator<T>(new QueryDefinition(queryString));
        var results = new List<T>();
        while (query.HasMoreResults)
        {
            var response = await query.ReadNextAsync();
            results.AddRange(response.ToList());
        }
        return results;
    }

    public async Task UpdateAsync(string id, T item)
    {
        await _cosmosDbService.GetContainer().UpsertItemAsync(item, new PartitionKey(id));
    }
}