using Microsoft.AspNetCore.Identity.Data;
using PetStoreService.Application.Interfaces.IdentityManager;
using PetStoreService.Application.Models.Response.Auth;

namespace PetStoreService.Application.Services.AuthenticationSystem;

public class AuthService(IIdentityManager identityManager)
{
    private readonly IIdentityManager _identityManager = identityManager;

    public async Task<LoginResponse> LoginUser(LoginRequest login)
    {
        var res = await _identityManager.Login(new IdentityLoginRequest(login.Email, login.Password));
        return new LoginResponse { JwtToken = res.AccessToken };
    }
}