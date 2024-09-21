using Microsoft.EntityFrameworkCore;
using PetStoreService.Persistence;

namespace PetStoreService.Application.Services;

public class ContextWrapper<T>(PetStoreDBContext blogContext) where T : class
{
    public readonly PetStoreDBContext PSContext = blogContext;

    public DbSet<T> Table { get; set; } = Property<T>.AccessOnCompile(blogContext);

    public async Task SaveChangesAsync()
    {
        await PSContext.SaveChangesAsync();
    }

    internal void SaveChanges()
    {
        PSContext.SaveChanges();
    }
}