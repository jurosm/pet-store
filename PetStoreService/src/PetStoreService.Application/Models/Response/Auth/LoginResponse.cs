namespace PetStoreService.Application.Models.Response.Auth;

public class LoginResponse
{
    public required string JwtToken { get; set; }
}