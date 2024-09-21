using Newtonsoft.Json;

namespace PetStoreService.Application.Models.Response.Auth;

public class LoginResponse
{
    [JsonProperty("jwtToken")]
    public string JwtToken { get; set; }
}