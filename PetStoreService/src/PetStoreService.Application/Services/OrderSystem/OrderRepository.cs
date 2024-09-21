using AutoMapper;
using PetStoreService.Application.Models.Request.Order;
using PetStoreService.Domain.Entities;

namespace PetStoreService.Application.Services.OrderSystem;

public class OrderRepository(ContextWrapper<Order> context, IMapper mapper) : Repository<Order>(context)
{
    private readonly IMapper _mapper = mapper;

    public bool CheckValidOrder(List<OrderItemRequest> orderItems)
    {
        List<OrderItemRequest> toys = [.. Context.PSContext.Toy.Select(_mapper.Map<OrderItemRequest>)];
        return orderItems.All(x => toys.Any(y => (y.ToyId == x.ToyId) && (y.Quantity >= x.Quantity)));
    }

    public decimal PricePerOrder(List<OrderItemRequest> orderItems)
    {
        return orderItems.Sum(x => Context.PSContext.Toy.ToList().Find(y => y.Id == x.ToyId).Price * x.Quantity);
    }

    public async Task RemoveItemsAsync(List<OrderItemRequest> orderItems)
    {
        orderItems.ForEach(async x =>
        {
            Toy toy = await Context.PSContext.Toy.FindAsync(x.ToyId);
            toy.Quantity -= x.Quantity;
        });

        await Context.SaveChangesAsync();
    }
}