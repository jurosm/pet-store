namespace PetStoreService.Application.Interfaces.IpInfoService;

public interface IIpInfoService
{
    Task<IPInfoResponse?> GetLocation(IPInfoRequest request);
}

public class IPInfoResponse
{
    public string Ip { get; set; }
    public string City { get; set; }
    public string Region { get; set; }
    public string Country { get; set; }
}

public class IPInfoRequest
{
    public string Ip { get; set; }
}