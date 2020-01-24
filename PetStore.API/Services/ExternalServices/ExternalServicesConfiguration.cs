using Microsoft.Extensions.DependencyInjection;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetStore.API.Services.ExternalServices
{
    public static class ExternalServicesConfiguration
    {
        public static void AddExternalServices(this IServiceCollection services, string secret)
        {
            StripeConfiguration.ApiKey = secret;
            services.AddScoped<IPInfoService>();
            services.AddScoped<StripeService>();
        }
    }
}
