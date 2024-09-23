using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using PetStoreService.Application.Interfaces.IpInfoService;

namespace PetStoreService.Infrastructure.IpInfoService;

public class IPInfoService(IHttpContextAccessor accessor) : IIpInfoService
{
    private readonly HttpClient _client = new HttpClient();
    private readonly IHttpContextAccessor _accessor = accessor;

    public async Task<IPInfoResponse?> GetLocation()
    {
        HttpResponseMessage httpResponse = await _client.GetAsync("https://ipinfo.io/");
        string res = await httpResponse.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<IPInfoResponse>(res);
    }
}