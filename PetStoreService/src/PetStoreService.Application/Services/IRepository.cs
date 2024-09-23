using System.Linq.Expressions;

namespace PetStoreService.Application.Services;

public interface IRepository<T> where T : class
{
    Task<List<T>> ReadAllAsync();
    Task<T?> ReadOneAsync(Expression<Func<T, bool>> predicate);
    Task<T> CreateAsync(T entity);
    Task<T> UpdateAsync(T entity);
    Task DeleteAsync(T entity);
    Task DeleteAsync(Expression<Func<T, bool>> predicate);
    Task DeleteAsync(int id);
    Task PutAsync(int id, T entity);
}