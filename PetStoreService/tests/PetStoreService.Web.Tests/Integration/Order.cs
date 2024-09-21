using PetStoreService.Web.Tests.Integration.Helper;

namespace PetStoreService.Web.Tests.Integration;

public class OrderTests : TestBase
{
    [Fact]
    public async void FetchOrders_ValidRequest_ReturnsOk()
    {
        var client = _factory.CreateClient();
        var authHeader = await Auth.Login(client);

        client.DefaultRequestHeaders.Authorization = authHeader;

        var orderRes = await client.GetAsync("/order");

        Assert.Equal(System.Net.HttpStatusCode.OK, orderRes.StatusCode);
    }
}