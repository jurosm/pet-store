
using Microsoft.AspNetCore.Mvc;
using PetStoreService.Application.Models.Request.Auth;
using PetStoreService.Application.Models.Response.Auth;
using PetStoreService.Application.Services.AuthenticationSystem;
using System.Threading.Tasks;

namespace PetStoreService.Web.Controllers
{
    [Route("/auth")]
    public class AuthController(AuthService authService) : BaseApiController
    {
        private readonly AuthService _authService = authService;

        [HttpPost("login")]
        public async Task<LoginResponse> Login([FromBody] LoginRequest loginRequest)
        {
            return await _authService.LoginUser(loginRequest);
        }
    }
}