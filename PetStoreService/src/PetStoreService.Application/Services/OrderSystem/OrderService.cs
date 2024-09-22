using AutoMapper;
using Microsoft.AspNetCore.Http;
using PetStoreService.Application.Exceptions.Services.Order;
using PetStoreService.Application.Interfaces.Payment;
using PetStoreService.Application.Models.Request.Order;
using PetStoreService.Application.Models.Response.ExternalServices;
using PetStoreService.Application.Models.Response.Order;
using PetStoreService.Application.Models.Services.Order;
using PetStoreService.Application.Services.ExternalServices;
using PetStoreService.Domain.Entities;

namespace PetStoreService.Application.Services.OrderSystem;

public class OrderService(IHttpContextAccessor accessor, OrderRepository orderRepository, IPInfoService ipInfo, IPaymentService stripeService, IMapper mapper)
{
    private readonly IHttpContextAccessor _accessor = accessor;
    private readonly OrderRepository _orderRepository = orderRepository;
    private readonly IPInfoService _iPInfo = ipInfo;
    private readonly IPaymentService _stripeService = stripeService;
    private readonly IMapper _mapper = mapper;

    public IEnumerable<OrderListItem> GetAllOrders()
    {
        return _orderRepository.ReadAll().OrderByDescending(x => x.OrderDate).Select(x => _mapper.Map<OrderListItem>(x));
    }

    public async Task<OrderInfo> Create(OrderRequest orderRequest)
    {
        if (!_orderRepository.CheckValidOrder(orderRequest.OrderItems)) throw new MessageException("Invalid order");
        decimal amount = _orderRepository.PricePerOrder(orderRequest.OrderItems);

        Order order = _mapper.Map<Order>(orderRequest);
        await InitializeOrderAsync(orderRequest, order);

        var paymentIntent = await _stripeService.CreatePaymentIntent(new PaymentIntentRequest() { Amount = amount, Currency = "usd" });
        order.ExternalReferenceId = paymentIntent.PaymentIntentId;

        await _orderRepository.CreateOrderAndRemoveItems(order);
        return new OrderInfo() { Message = "Order created" };
    }

    private async Task InitializeOrderAsync(OrderRequest orderRequest, Order order)
    {
        var address = _accessor.HttpContext.Connection.RemoteIpAddress.ToString();
        IPInfoResponse ipInfoAddress = await _iPInfo.GetLocation(address);

        orderRequest.OrderItems.ForEach(x => order.OrderItem.Add(_mapper.Map<OrderItem>(x)));
        order.ShippingAddress = orderRequest.Country + "," + orderRequest.City + "," + orderRequest.StreetAddress;
        order.IpinfoAddress = ipInfoAddress.Country + "," + ipInfoAddress.City;
        order.OrderStatus = OrderStatus.Draft;
    }


}