using PetStoreService.Application.Services.AuthenticationSystem;
using System;

namespace PetStore.API
{
    public class EnvironmentConfigurations
    {
        public string StripeSecret;
        public string DBConnectionString;
        public AuthSettings AuthSettings;

        public EnvironmentConfigurations()
        {
            StripeSecret = Environment.GetEnvironmentVariable("STRIPE_SECRET");
            DBConnectionString = Environment.GetEnvironmentVariable("DB_CONNECTION_STRING");
            AuthSettings = new AuthSettings();
        }
    }
}