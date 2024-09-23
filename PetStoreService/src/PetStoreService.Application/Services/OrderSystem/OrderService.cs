using AutoMapper;
using PetStoreService.Application.Interfaces.Payment;
using PetStoreService.Application.Models.Request.Order;
using PetStoreService.Application.Models.Response.Order;
using PetStoreService.Application.Models.Services.Order;
using PetStoreService.Domain.Entities;

namespace PetStoreService.Application.Services.OrderSystem;

public class OrderService(OrderRepository orderRepository, IPaymentService stripeService, IMapper mapper)
{
    private readonly OrderRepository _orderRepository = orderRepository;
    private readonly IPaymentService _stripeService = stripeService;
    private readonly IMapper _mapper = mapper;

    public async Task<IEnumerable<OrderListItem>> GetAllOrdersAsync()
    {
        return (await _orderRepository.ReadAllAsync()).OrderByDescending(x => x.OrderDate).Select(x => _mapper.Map<OrderListItem>(x));
    }

    public async Task<OrderInfo> CreateAsync(OrderRequest orderRequest)
    {
        Order order = _mapper.Map<Order>(orderRequest);

        // Todo: IpinfoAddress should be nullable
        order.IpinfoAddress = string.Empty;

        var amount = await _orderRepository.CreateOrderAndRemoveItems(order);

        var paymentIntent = await _stripeService.CreatePaymentIntent(new PaymentIntentRequest() { Amount = amount, Currency = "usd" });
        order.ExternalReferenceId = paymentIntent.PaymentIntentId;

        await _orderRepository.UpdateAsync(order);

        return new OrderInfo() { Message = "Order created" };
    }
}