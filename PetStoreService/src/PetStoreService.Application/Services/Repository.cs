using Microsoft.EntityFrameworkCore;
using PetStoreService.Application.Helper;
using PetStoreService.Persistence;
using System.Linq.Expressions;

namespace PetStoreService.Application.Services;

public class Repository<T>(PetStoreDBContext context) : IRepository<T> where T : class
{
    protected readonly PetStoreDBContext Context = context;

    public DbSet<T> Table { get; set; } = Property<T>.AccessOnCompile(context)!;


    public async Task<T> CreateAsync(T entity)
    {
        await Table.AddAsync(entity);
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

    public Task<List<T>> ReadAllAsync()
    {
        return Table.ToListAsync();
    }

    public Task<T?> ReadOneAsync(Expression<Func<T, bool>> predicate)
    {
        return Table.FirstOrDefaultAsync(predicate);
    }

    public async Task<T> UpdateAsync(T entity)
    {
        Table.Update(entity);
        await Context.SaveChangesAsync();
        return entity;
    }

    public async Task DeleteAsync(int id)
    {
        T? entity = await Table.FindAsync(id);

        if (entity != null)
        {
            Table.Remove(entity);
            await Context.SaveChangesAsync();
        }
        else
        {
            throw new FileNotFoundException("Entity not found");
        }
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