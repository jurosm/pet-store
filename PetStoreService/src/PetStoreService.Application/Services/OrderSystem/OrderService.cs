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

    public async Task<CreateOrderResponse> CreateAsync(OrderRequest orderRequest)
    {
        Order order = _mapper.Map<Order>(orderRequest);

        await _orderRepository.CreateOrderAndRemoveItems(order);

        var response = _mapper.Map<CreateOrderResponse>(order);

        return response;
    }

    public async Task<CreatePaymentIntentResponse> CreatePaymentIntentAsync(CreatePaymentIntentRequest request)
    {
        var order = await _orderRepository.GetOrderByIdAsync(request.OrderId);

        var paymentIntent = await _stripeService.CreatePaymentIntent(new PaymentIntentRequest
        {
            Amount = order.Total,
            Currency = "usd"
        });

        order.OrderStatus = OrderStatus.PaymentProcessing;

        await _orderRepository.UpdateAsync(order);

        return new CreatePaymentIntentResponse
        {
            ClientSecret = paymentIntent.ClientSecret,
        };
    }

    public async Task<GetOrderResponse> GetOrderById(int orderId)
    {
        var order = await _orderRepository.GetOrderByIdAsync(orderId);
        return _mapper.Map<GetOrderResponse>(order);
    }
}