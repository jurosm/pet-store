using Microsoft.Extensions.DependencyInjection;

namespace PetStoreService.Persistence
{
    public static class PetStoreDBConfiguration
    {
        public static void AddPetStoreDBServices(this IServiceCollection services)
        {
            services.AddDbContext<PetStoreDBContext>();
        }
    }
}