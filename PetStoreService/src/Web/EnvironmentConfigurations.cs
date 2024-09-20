using System;

namespace PetStore.API
{
    public class EnvironmentConfigurations
    {
        public string StripeSecret;
        public string DBConnectionString;
        public string Auth0Audience;
        public string Auth0Domain;
        public string Auth0ClientId;
        public string Auth0ClientSecret;
        public EnvironmentConfigurations()
        {
            StripeSecret = Environment.GetEnvironmentVariable("STRIPE_SECRET");
            DBConnectionString = Environment.GetEnvironmentVariable("DB_CONNECTION_STRING");
            Auth0Audience = Environment.GetEnvironmentVariable("AUTH0_AUDIENCE");
            Auth0Domain = Environment.GetEnvironmentVariable("AUTH0_DOMAIN");
            Auth0ClientId = Environment.GetEnvironmentVariable("AUTH0_CLIENT_ID");
            Auth0ClientSecret = Environment.GetEnvironmentVariable("AUTH0_CLIENT_SECRET");
        }
    }
}