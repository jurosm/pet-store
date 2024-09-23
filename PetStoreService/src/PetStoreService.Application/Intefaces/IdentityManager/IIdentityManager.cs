namespace PetStoreService.Application.Interfaces.IdentityManager;

public interface IIdentityManager
{
    Task<IdentityLoginResponse> Login(IdentityLoginRequest request);
}

public record IdentityLoginRequest(string Email, string Password);
public record IdentityLoginResponse(string AccessToken);