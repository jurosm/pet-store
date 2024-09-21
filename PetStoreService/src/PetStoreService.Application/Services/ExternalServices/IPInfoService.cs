using Newtonsoft.Json;
using PetStoreService.Application.Models.Response.ExternalServices;

namespace PetStoreService.Application.Services.ExternalServices;

public class IPInfoService
{
    private readonly HttpClient _client;
    public IPInfoService()
    {
        _client = new HttpClient();
    }
    public async Task<IPInfoResponse> GetLocation(string ipAddress)
    {
        if (ipAddress == "::1")
        {
            HttpClient client = new();
            _ = await (await client.GetAsync("https://api.ipify.org/")).Content.ReadAsStringAsync();
        }

        HttpResponseMessage httpResponse = await _client.GetAsync("https://ipinfo.io/");
        string res = await httpResponse.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<IPInfoResponse>(res);
    }
}