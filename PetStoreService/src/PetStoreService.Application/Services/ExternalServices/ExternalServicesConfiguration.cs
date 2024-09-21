using Microsoft.Extensions.DependencyInjection;
using Stripe;

namespace PetStoreService.Application.Services.ExternalServices;

public static class ExternalServicesConfiguration
{
    public static void AddExternalServices(this IServiceCollection services, string secret)
    {
        StripeConfiguration.ApiKey = secret;
        services.AddScoped<IPInfoService>();
        services.AddScoped<StripeService>();
    }
}
