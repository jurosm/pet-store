using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetStore.API.Models.Response.Auth
{
    public class LoginResponse
    {
        [JsonProperty("jwtToken")]
        public string JwtToken { get; set; }
    }
}
