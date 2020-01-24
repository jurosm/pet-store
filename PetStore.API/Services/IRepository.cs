using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace PetStore.API.Services
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> ReadAll();
        T ReadOne(Expression<Func<T, bool>> predicate);
        Task<T> CreateAsync(T entity);
        Task<T> UpdateAsync(T entity);
        Task DeleteAsync(T entity);
        Task DeleteAsync(Expression<Func<T, bool>> predicate);
        Task DeleteAsync(int id);
        Task PutAsync(int id, T entity);
    }
}
