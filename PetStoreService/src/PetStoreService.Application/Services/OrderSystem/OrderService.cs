using AutoMapper;
using Microsoft.AspNetCore.Http;
using PetStoreService.Application.Exceptions.Services.Order;
using PetStoreService.Application.Models.Request.Order;
using PetStoreService.Application.Models.Response.ExternalServices;
using PetStoreService.Application.Models.Response.Order;
using PetStoreService.Application.Models.Services.Order;
using PetStoreService.Application.Services.ExternalServices;
using PetStoreService.Domain.Entities;

namespace PetStoreService.Application.Services.OrderSystem;

public class OrderService(IHttpContextAccessor accessor, OrderRepository orderRepository, IPInfoService ipInfo, StripeService stripeService, IMapper mapper)
{
    private readonly IHttpContextAccessor _accessor = accessor;
    private readonly OrderRepository _orderRepository = orderRepository;
    private readonly IPInfoService _iPInfo = ipInfo;
    private readonly StripeService _stripeService = stripeService;
    private readonly IMapper _mapper = mapper;

    public IEnumerable<OrderListItem> GetAllOrders()
    {
        return _orderRepository.ReadAll().OrderByDescending(x => x.OrderDate).Select(x => _mapper.Map<OrderListItem>(x));
    }

    public async Task<OrderInfo> Buy(OrderRequest orderRequest)
    {
        if (!_orderRepository.CheckValidOrder(orderRequest.OrderItems)) throw new MessageException("Invalid order");
        decimal amount = _orderRepository.PricePerOrder(orderRequest.OrderItems);

        Order order = _mapper.Map<Order>(orderRequest);
        await InitializeOrderAsync(orderRequest, order);

        string chargeId = _stripeService.CreateCharge(orderRequest.TokenId, (long)amount);
        order.ExternalReferenceId = chargeId;

        string status = await _stripeService.GetStatusAsync(chargeId);
        order.OrderStatus = status;

        await _orderRepository.UpdateAsync(order);

        if (order.OrderStatus == "succeeded" || order.OrderStatus == "amount_capturable_updated")
        {
            await _orderRepository.RemoveItemsAsync(orderRequest.OrderItems);
            return new OrderInfo() { Message = "Transaction succeeded!" };
        }

        return new OrderInfo() { Message = "Payment failed!" };
    }
    private async Task InitializeOrderAsync(OrderRequest orderRequest, Order order)
    {
        var address = _accessor.HttpContext.Connection.RemoteIpAddress.ToString();
        IPInfoResponse ipInfoAddress = await _iPInfo.GetLocation(address);

        orderRequest.OrderItems.ForEach(x => order.OrderItem.Add(_mapper.Map<OrderItem>(x)));
        order.ShippingAddress = orderRequest.Country + "," + orderRequest.City + "," + orderRequest.StreetAddress;
        order.IpinfoAddress = ipInfoAddress.Country + "," + ipInfoAddress.City;
        order.OrderStatus = "pending";
        await _orderRepository.CreateAsync(order);
    }
}