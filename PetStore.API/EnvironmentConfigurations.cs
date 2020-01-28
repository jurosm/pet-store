using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetStore.API
{
    public class EnvironmentConfigurations
    {
        public string StripeSecret;
        public EnvironmentConfigurations()
        {
            this.StripeSecret = Environment.GetEnvironmentVariable("STRIPE_SECRET");
        }
    }
}
