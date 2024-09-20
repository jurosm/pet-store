using Microsoft.EntityFrameworkCore;
using PetStore.API.Db;
using PetStore.API.Helper;
using System.Threading.Tasks;

namespace PetStore.API.Services.CRUD
{

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
}