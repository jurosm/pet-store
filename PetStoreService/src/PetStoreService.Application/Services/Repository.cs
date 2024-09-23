using Microsoft.EntityFrameworkCore;
using PetStoreService.Application.Helper.Pagination;
using PetStoreService.Persistence;
using System.Linq.Expressions;

namespace PetStoreService.Application.Services;

public class Repository<T>(PetStoreDBContext context) : IRepository<T> where T : class
{
    protected readonly PetStoreDBContext Context = context;

    public DbSet<T> Table { get; set; } = Property<T>.AccessOnCompile(context);

    public PagedResult<T> ReadPage(int page, int pageSize)
    {
        return PagedResult<T>.GetPaged(Table.AsQueryable(), page, pageSize);
    }

    public async Task<T> CreateAsync(T entity)
    {
        Table.Add(entity);
        await Context.SaveChangesAsync();
        return entity;
    }

    public async Task DeleteAsync(T entity)
    {
        if (Table.Contains(entity))
        {
            Table.Remove(entity);
            await Context.SaveChangesAsync();
        }
    }

    public async Task DeleteAsync(Expression<Func<T, bool>> predicate)
    {
        var res = await Table.FindAsync(predicate);
        if (res != null)
        {
            Table.Remove(res);
            await Context.SaveChangesAsync();
        }
    }

    public IEnumerable<T> ReadAll()
    {
        return Table;
    }

    public T ReadOne(Expression<Func<T, bool>> predicate)
    {
        return Table.FirstOrDefault(predicate);
    }

    public async Task<T> UpdateAsync(T entity)
    {
        Table.Update(entity);
        await Context.SaveChangesAsync();
        return entity;
    }

    public async Task DeleteAsync(int id)
    {
        Table.Remove(await Table.FindAsync(id));
        await Context.SaveChangesAsync();
    }

    public async Task PutAsync(int id, T entity)
    {
        Table.Update(entity);
        await Context.SaveChangesAsync();
    }

    public async Task SaveChangesAsync()
    {
        await Context.SaveChangesAsync();
    }
}