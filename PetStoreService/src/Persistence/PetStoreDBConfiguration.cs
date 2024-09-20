using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace PetStoreService.Persistence
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