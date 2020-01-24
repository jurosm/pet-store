using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IpInfo.Api.Client;
using IpInfo.Api.Client.Models;

namespace PetStore.API.Services.ExternalServices
{
    public class IPInfoService
    {
        IIpInfoApiClient IPInfo;
        public IPInfoService()
        {
            IPInfo = new IpInfoApiClient("https://localhost:5001");
        }
        public BaseResponse<GetIpInfoResponse> GetLocation(string ipAddress)
        {
            return IPInfo.GetIpInfo(ipAddress);
        }
    }
}
