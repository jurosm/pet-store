using Newtonsoft.Json;
using PetStoreService.Application.Models.Request.Order;
using PetStoreService.Application.Models.Response.Order;
using PetStoreService.Application.Models.Services.Order;
using PetStoreService.Web.Tests.Helper;
using System.Text;

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

    [Fact]
    public async void CreateOrder_ValidRequest_ShouldReturnCreated()
    {
        var client = _factory.CreateClient();
        var authHeader = await Auth.Login(client);

        client.DefaultRequestHeaders.Authorization = authHeader;

        var toy1 = await ToyHelper.CreateToy(client);
        var toy2 = await ToyHelper.CreateToy(client);

        var orderRequest = new OrderRequest
        {
            City = "City",
            Country = "Country",
            CustomerName = "CustomerName",
            CustomerSurname = "CustomerSurname",
            OrderItem =
            [
                new OrderItemRequest
                {
                    ToyId = toy1.Id,
                    Quantity = 1
                },
                new OrderItemRequest
                {
                    ToyId = toy2.Id,
                    Quantity = 1
                }
            ],
            StreetAddress = "StreetAddress"
        };

        var createOrderRes = await client.PostAsync("/order", new StringContent(JsonConvert.SerializeObject(orderRequest), Encoding.UTF8, "application/json"));

        Assert.Equal(System.Net.HttpStatusCode.Created, createOrderRes.StatusCode);

        var createOrderResBodyString = createOrderRes.Content.ReadAsStringAsync();
        var createOrderResBody = JsonConvert.DeserializeObject<CreateOrderResponse>(createOrderResBodyString.Result);

        Assert.NotNull(createOrderResBody);

        Assert.Equal(toy1.Price + toy2.Price, createOrderResBody.Total);
        Assert.Equal(OrderStatus.Draft, createOrderResBody.OrderStatus);
    }

    [Fact]
    public async void CreateOrder_ToyQuantityExceeded_ShouldReturnBadRequest()
    {
        var client = _factory.CreateClient();
        var authHeader = await Auth.Login(client);

        client.DefaultRequestHeaders.Authorization = authHeader;

        var toy = await ToyHelper.CreateToy(client);

        var orderRequest = new OrderRequest
        {
            City = "City",
            Country = "Country",
            CustomerName = "CustomerName",
            CustomerSurname = "CustomerSurname",
            OrderItem =
            [
                new OrderItemRequest
                {
                    ToyId = toy.Id,
                    Quantity = 10000000
                }
            ],
            StreetAddress = "StreetAddress"
        };

        var createOrderRes = await client.PostAsync("/order", new StringContent(JsonConvert.SerializeObject(orderRequest), Encoding.UTF8, "application/json"));

        Assert.Equal(System.Net.HttpStatusCode.BadRequest, createOrderRes.StatusCode);
    }

    [Fact]
    public async void CreateOrder_ToyQuantityExceededParallel_ShouldReturnBadRequest()
    {
        var client = _factory.CreateClient();
        var authHeader = await Auth.Login(client);

        client.DefaultRequestHeaders.Authorization = authHeader;

        var toy = await ToyHelper.CreateToy(client);

        var orderRequest = new OrderRequest
        {
            City = "City",
            Country = "Country",
            CustomerName = "CustomerName",
            CustomerSurname = "CustomerSurname",
            OrderItem =
            [
                new OrderItemRequest
                {
                    ToyId = toy.Id,
                    Quantity = toy.Quantity
                }
            ],
            StreetAddress = "StreetAddress"
        };

        StringContent request = new(JsonConvert.SerializeObject(orderRequest), Encoding.UTF8, "application/json");

        var results = await Task.WhenAll([client.PostAsync("/order", request), client.PostAsync("/order", request)]);

        Assert.Contains(results, res => res.StatusCode == System.Net.HttpStatusCode.Created);
        Assert.Contains(results, res => res.StatusCode == System.Net.HttpStatusCode.BadRequest);
    }


    [Fact]
    public async void CreateOrderAndPay_ValidRequest_ShouldReturnOk()
    {
        var client = _factory.CreateClient();
        var authHeader = await Auth.Login(client);

        client.DefaultRequestHeaders.Authorization = authHeader;

        var toy = await ToyHelper.CreateToy(client);

        var orderRequest = new OrderRequest
        {
            City = "City",
            Country = "Country",
            CustomerName = "CustomerName",
            CustomerSurname = "CustomerSurname",
            OrderItem =
            [
                new OrderItemRequest
                {
                    ToyId = toy.Id,
                    Quantity = 1
                }
            ],
            StreetAddress = "StreetAddress"
        };

        var createOrderRes = await client.PostAsync("/order", new StringContent(JsonConvert.SerializeObject(orderRequest), Encoding.UTF8, "application/json"));

        Assert.Equal(System.Net.HttpStatusCode.Created, createOrderRes.StatusCode);

        var createOrderResBodyString = createOrderRes.Content.ReadAsStringAsync();
        var createOrderResBody = JsonConvert.DeserializeObject<CreateOrderResponse>(createOrderResBodyString.Result);

        Assert.NotNull(createOrderResBody);

        var createPaymentIntentRes = await client.PostAsync($"/order/{createOrderResBody.Id}/payment-intent", new StringContent(JsonConvert.SerializeObject(new { })));

        Assert.Equal(System.Net.HttpStatusCode.OK, createPaymentIntentRes.StatusCode);

        var getOrderRes = await client.GetAsync($"/order/{createOrderResBody.Id}");

        Assert.Equal(System.Net.HttpStatusCode.OK, getOrderRes.StatusCode);

        var getOrderResBodyString = getOrderRes.Content.ReadAsStringAsync();
        var getOrderResBody = JsonConvert.DeserializeObject<GetOrderResponse>(getOrderResBodyString.Result)!;

        Assert.Equal(OrderStatus.PaymentProcessing, getOrderResBody.OrderStatus);
    }
}