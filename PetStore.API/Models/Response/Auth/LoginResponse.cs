using Newtonsoft.Json;

namespace PetStore.API.Models.Response.Auth
{
    public class LoginResponse
    {
        [JsonProperty("jwtToken")]
        public string JwtToken { get; set; }
    }
}
