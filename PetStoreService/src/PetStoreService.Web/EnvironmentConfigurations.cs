using PetStoreService.Application.Services.AuthenticationSystem;

namespace PetStoreService.Web;

public class EnvironmentConfigurations
{
    public AuthSettings AuthSettings;

    public EnvironmentConfigurations()
    {
        AuthSettings = new AuthSettings();
    }
}