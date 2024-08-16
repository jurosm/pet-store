using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using PetStore.API.Models.Response.ExternalServices;

namespace PetStore.API.Services.ExternalServices
{
    public class IPInfoService
    {
        readonly HttpClient client;
        public IPInfoService()
        {
            client = new HttpClient();
        }
        public async Task<IPInfoResponse> GetLocation(string ipAddress)
        {
            if (ipAddress == "::1")
            {
                HttpClient client = new();
                ipAddress = await (await client.GetAsync("https://api.ipify.org/")).Content.ReadAsStringAsync();
            }

            HttpResponseMessage httpResponse = await client.GetAsync("https://ipinfo.io/");
            string res = await httpResponse.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<IPInfoResponse>(res);
        }
    }
}
