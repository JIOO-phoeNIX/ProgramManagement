using Microsoft.Azure.Cosmos;

namespace ProgramManagement.Persistence.Db;

/// <summary>
/// This creates and return the db container
/// </summary>
public interface ICosmosDbService
{
    /// <summary>
    /// Return the container instance
    /// </summary>
    /// <returns></returns>
    public Container GetContainer();
}