using AspNetCoreRateLimit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetStore.API
{
    public static class MyRateLimitConfiguration
    {
        public static void AddRateLimitServices(this IServiceCollection services, IConfigurationSection ipRateLimiting, IConfigurationSection ipRateLimitPolicies)
        {
            services.Configure<IpRateLimitOptions>(ipRateLimiting);
            services.Configure<IpRateLimitPolicies>(ipRateLimitPolicies);
            services.AddSingleton<IIpPolicyStore, MemoryCacheIpPolicyStore>();
            services.AddSingleton<IRateLimitCounterStore, MemoryCacheRateLimitCounterStore>();
            services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();
        }
    }
}
