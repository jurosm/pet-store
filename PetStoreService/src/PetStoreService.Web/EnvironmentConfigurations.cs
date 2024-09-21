using PetStoreService.Application.Services.AuthenticationSystem;
using System;

namespace PetStoreService.Web;

public class EnvironmentConfigurations
{
    public string StripeSecret;
    public AuthSettings AuthSettings;

    public EnvironmentConfigurations()
    {
        StripeSecret = Environment.GetEnvironmentVariable("STRIPE_SECRET");
        AuthSettings = new AuthSettings();
    }
}
