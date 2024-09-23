using AutoMapper;
using Microsoft.AspNetCore.Http;
using PetStoreService.Application.Interfaces.Payment;
using PetStoreService.Application.Models.Request.Order;
using PetStoreService.Application.Models.Response.Order;
using PetStoreService.Application.Models.Services.Order;
using PetStoreService.Domain.Entities;

namespace PetStoreService.Application.Services.OrderSystem;

public class OrderService(IHttpContextAccessor accessor, OrderRepository orderRepository, IPaymentService stripeService, IMapper mapper)
{
    private readonly IHttpContextAccessor _accessor = accessor;
    private readonly OrderRepository _orderRepository = orderRepository;
    private readonly IPaymentService _stripeService = stripeService;
    private readonly IMapper _mapper = mapper;

    public IEnumerable<OrderListItem> GetAllOrders()
    {
        return _orderRepository.ReadAll().OrderByDescending(x => x.OrderDate).Select(x => _mapper.Map<OrderListItem>(x));
    }

    public async Task<OrderInfo> Create(OrderRequest orderRequest)
    {
        Order order = _mapper.Map<Order>(orderRequest);

        // Todo: IpinfoAddress should be nullable
        order.IpinfoAddress = _accessor.HttpContext?.Connection.RemoteIpAddress?.ToString() ?? string.Empty;

        var amount = await _orderRepository.CreateOrderAndRemoveItems(order);

        var paymentIntent = await _stripeService.CreatePaymentIntent(new PaymentIntentRequest() { Amount = amount, Currency = "usd" });
        order.ExternalReferenceId = paymentIntent.PaymentIntentId;

        await _orderRepository.UpdateAsync(order);

        return new OrderInfo() { Message = "Order created" };
    }
}