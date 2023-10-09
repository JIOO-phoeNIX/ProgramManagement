
namespace ProgramManagement.Persistence.Repository.Interfaces;

/// <summary>
/// This holds the basic crud operaions to be performed on an entity, every repository can inherit from this to
/// perform its basic crud operation using generics
/// </summary>
/// <typeparam name="T">The entity type</typeparam>
public interface IBaseRepository<T> where T : class
{
    Task<IEnumerable<T>> GetMultipleAsync(string query);
    Task<T> GetAsync(string id);
    Task AddAsync(T item, string id);
    Task UpdateAsync(string id, T item);
    Task DeleteAsync(string id);
}