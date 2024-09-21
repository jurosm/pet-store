using PetStoreService.Application.Helper.Pagination;
using System.Linq.Expressions;

namespace PetStoreService.Application.Services;

public class Repository<T>(ContextWrapper<T> context) : IRepository<T> where T : class
{
    public readonly ContextWrapper<T> Context = context;

    public PagedResult<T> ReadPage(int page, int pageSize)
    {
        return PagedResult<T>.GetPaged(Context.Table.AsQueryable<T>(), page, pageSize);
    }

    public async Task<T> CreateAsync(T entity)
    {
        Context.Table.Add(entity);
        await Context.SaveChangesAsync();
        return entity;
    }

    public async Task DeleteAsync(T entity)
    {
        if (Context.Table.Contains(entity))
        {
            Context.Table.Remove(entity);
            await Context.SaveChangesAsync();
        }
    }

    public async Task DeleteAsync(Expression<Func<T, bool>> predicate)
    {
        var res = await Context.Table.FindAsync(predicate);
        if (res != null)
        {
            Context.Table.Remove(res);
            await Context.SaveChangesAsync();
        }
    }

    public IEnumerable<T> ReadAll()
    {
        return Context.Table;
    }

    public T ReadOne(Expression<Func<T, bool>> predicate)
    {
        return Context.Table.FirstOrDefault(predicate);
    }

    public async Task<T> UpdateAsync(T entity)
    {
        Context.Table.Update(entity);
        await Context.SaveChangesAsync();
        return entity;
    }

    public async Task DeleteAsync(int id)
    {
        Context.Table.Remove(await Context.Table.FindAsync(id));
        await Context.SaveChangesAsync();
    }

    public async Task PutAsync(int id, T entity)
    {
        Context.Table.Update(entity);
        await Context.SaveChangesAsync();
    }

    public async Task SaveChangesAsync()
    {
        await Context.SaveChangesAsync();
    }
}
