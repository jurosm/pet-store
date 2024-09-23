namespace PetStoreService.Application.Interfaces.IpInfoService;

public interface IIpInfoService
{
    Task<IPInfoResponse?> GetLocation();
}

public class IPInfoResponse
{
    public required string Ip { get; set; }
    public required string City { get; set; }
    public required string Region { get; set; }
    public required string Country { get; set; }
}