using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
            this.StripeSecret = Environment.GetEnvironmentVariable("STRIPE_SECRET");
            this.DBConnectionString = Environment.GetEnvironmentVariable("DB_CONNECTION_STRING");
            this.Auth0Audience = Environment.GetEnvironmentVariable("AUTH0_AUDIENCE");
            this.Auth0Domain = Environment.GetEnvironmentVariable("AUTH0_DOMAIN");
            this.Auth0ClientId = Environment.GetEnvironmentVariable("AUTH0_CLIENT_ID");
            this.Auth0ClientSecret = Environment.GetEnvironmentVariable("AUTH0_CLIENT_SECRET");
        }
    }
}
