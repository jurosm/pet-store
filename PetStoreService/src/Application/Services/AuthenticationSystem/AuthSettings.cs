namespace PetStoreService.Application.Services.AuthenticationSystem;

public class AuthSettings
{
    public string Auth0Audience;
    public string Auth0Domain;
    public string Auth0ClientId;
    public string Auth0ClientSecret;

    public AuthSettings()
    {
        Auth0Audience = Environment.GetEnvironmentVariable("AUTH0_AUDIENCE");
        Auth0Domain = Environment.GetEnvironmentVariable("AUTH0_DOMAIN");
        Auth0ClientId = Environment.GetEnvironmentVariable("AUTH0_CLIENT_ID");
        Auth0ClientSecret = Environment.GetEnvironmentVariable("AUTH0_CLIENT_SECRET");
    }
}