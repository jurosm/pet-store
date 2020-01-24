using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using IpInfo.Api.Client;
using IpInfo.Api.Client.Models;
using Newtonsoft.Json;
using PetStore.API.Models.Response.ExternalServices;

namespace PetStore.API.Services.ExternalServices
{
    public class IPInfoService
    {
        HttpClient client;
        public IPInfoService()
        {
            client = new HttpClient();
        }
        public async Task<IPInfoResponse> GetLocation(string ipAddress)
        {
            HttpResponseMessage httpResponse = await client.GetAsync("https://ipinfo.io/" + ipAddress + "?token=" + "237e92574bcfed");
            string res = await httpResponse.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<IPInfoResponse>(res);
        }
    }
}
