using Microsoft.Extensions.DependencyInjection;

namespace PetStoreService.Application.Services.ExternalServices;

public static class ExternalServicesConfiguration
{
    public static void AddExternalServices(this IServiceCollection services)
    {
        services.AddScoped<IPInfoService>();
    }
}