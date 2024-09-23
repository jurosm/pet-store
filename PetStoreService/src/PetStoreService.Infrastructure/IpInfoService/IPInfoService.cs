using Newtonsoft.Json;
using PetStoreService.Application.Interfaces.IpInfoService;

namespace PetStoreService.Infrastructure.IpInfoService;

public class IPInfoService : IIpInfoService
{
    private readonly HttpClient _client;
    public IPInfoService()
    {
        _client = new HttpClient();
    }
    public async Task<IPInfoResponse?> GetLocation(IPInfoRequest request)
    {
        if (request.Ip == "::1")
        {
            return null;
        }

        HttpResponseMessage httpResponse = await _client.GetAsync("https://ipinfo.io/");
        string res = await httpResponse.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<IPInfoResponse>(res);
    }
}