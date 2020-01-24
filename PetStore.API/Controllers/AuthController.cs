using Microsoft.AspNetCore.Mvc;
using PetStore.API.Models.Request.Auth;
using PetStore.API.Models.Response.Auth;
using PetStore.API.Services.AuthenticationSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetStore.API.Controllers
{
    [Route("/auth")]
    public class AuthController : BaseApiController
    {
        private readonly AuthService AuthService;
        public AuthController(AuthService authService)
        {
            this.AuthService = authService;
        }
        [HttpPost("login")]
        public async Task<LoginResponse> Login([FromBody]LoginRequest loginRequest)
        {
            return await AuthService.LoginUser(loginRequest);
        }
    }
}
