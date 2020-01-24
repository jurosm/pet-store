using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PetStore.API.Db;

namespace CodeSpaceBlog.API.Db
{
    public static class PetStoreDBConfiguration
    {
        public static void AddPetStoreDBServices(this IServiceCollection services, string connectionString)
        {
           services.AddDbContext<PetStoreDBContext>(options =>
            options.UseNpgsql(connectionString));
        }
    }
}
