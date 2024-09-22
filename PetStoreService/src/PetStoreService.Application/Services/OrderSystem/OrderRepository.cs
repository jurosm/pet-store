using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PetStoreService.Application.Models.Request.Order;
using PetStoreService.Domain.Entities;
using PetStoreService.Persistence;

namespace PetStoreService.Application.Services.OrderSystem;

public class OrderRepository(PetStoreDBContext context, IMapper mapper) : Repository<Order>(context)
{
    private readonly IMapper _mapper = mapper;

    public bool CheckValidOrder(List<OrderItemRequest> orderItems)
    {
        List<OrderItemRequest> toys = [.. Context.Toy.Select(_mapper.Map<OrderItemRequest>)];
        return orderItems.All(x => toys.Any(y => (y.ToyId == x.ToyId) && (y.Quantity >= x.Quantity)));
    }

    public decimal PricePerOrder(List<OrderItemRequest> orderItems)
    {
        return orderItems.Sum(x => Context.Toy.ToList().Find(y => y.Id == x.ToyId).Price * x.Quantity);
    }

    public async Task RemoveItemsAsync(ICollection<OrderItem> orderItems)
    {
        orderItems.ToList().ForEach(async x =>
        {
            Toy? toy = await Context.Toy.FindAsync(x.ToyId);
            if (toy == null)
            {
                throw new Exception("Toy not found");
            }
            toy.Quantity -= x.Quantity;
        });

        await Context.SaveChangesAsync();
    }

    public async Task CreateOrderAndRemoveItems(Order order)
    {
        using var transaction = Context.Database.BeginTransaction();
        await CreateAsync(order);

        await RemoveItemsAsync(order.OrderItem);

        transaction.Commit();
    }
}