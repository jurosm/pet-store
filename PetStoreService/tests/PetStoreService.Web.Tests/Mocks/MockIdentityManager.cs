using Microsoft.IdentityModel.Tokens;
using PetStoreService.Application.Interfaces.IdentityManager;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace PetStoreService.Web.Tests.Mocks;

public class MockIdentityManager : IIdentityManager
{
    public static Application.Models.Request.Auth.LoginRequest ValidRequest = new Application.Models.Request.Auth.LoginRequest { Email = "valid@email.com", Password = "asdf_123" };
    public static Application.Models.Request.Auth.LoginRequest InvalidRequest = new Application.Models.Request.Auth.LoginRequest { Email = "invalid@email.com", Password = "asdf_123" };

    public Task<IdentityLoginResponse> Login(IdentityLoginRequest request)
    {
        if (request.Email == ValidRequest.Email && request.Password == ValidRequest.Password)
        {
            return Task.FromResult(new IdentityLoginResponse(GenerateToken("asdfasdfasdfasdfasdfasddfasdfasdfafsdaffdsfafffdaafafadsfdsafdd", "issuer", "audience", 3600)));
        }
        else
        {
            throw new Auth0.Core.Exceptions.ErrorApiException();
        }
    }

    public static string GenerateToken(string secretKey, string issuer, string audience, int expiryMinutes)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, "user-id-123"),  // Substitute with your test user ID
            new Claim(JwtRegisteredClaimNames.Email, "testuser@example.com"),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim("scope", "read:messages")  // Add relevant claims like scope or roles
        };

        var token = new JwtSecurityToken(
            issuer: issuer,
            audience: audience,
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(expiryMinutes),
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}