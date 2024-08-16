using Microsoft.AspNetCore.Mvc;
using PetStore.API.Models.Request.Auth;
using PetStore.API.Models.Response.Auth;
using PetStore.API.Services.AuthenticationSystem;
using System.Threading.Tasks;

namespace PetStore.API.Controllers
{
    [Route("/auth")]
    public class AuthController(AuthService authService) : BaseApiController
    {
        private readonly AuthService AuthService = authService;

        [HttpPost("login")]
        public async Task<LoginResponse> Login([FromBody] LoginRequest loginRequest)
        {
            return await AuthService.LoginUser(loginRequest);
        }
    }
}
