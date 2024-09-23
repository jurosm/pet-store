using Microsoft.EntityFrameworkCore;
using PetStoreService.Application.Exceptions.Services.Order;
using PetStoreService.Domain.Entities;
using PetStoreService.Persistence;

namespace PetStoreService.Application.Services.OrderSystem;

public class OrderRepository(PetStoreDBContext context) : Repository<Order>(context)
{
    public bool CheckValidOrder(ICollection<OrderItem> orderItems)
    {
        var toys = Context.Toy.Where(t => orderItems.Select(oi => oi.ToyId).Contains(t.Id)).ToList();
        return toys.All(toy => orderItems.Any(oi => (oi.ToyId == toy.Id) && (oi.Quantity <= toy.Quantity)));
    }

    public decimal PricePerOrder(ICollection<OrderItem> orderItems)
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

    public async Task<decimal> CreateOrderAndRemoveItems(Order order)
    {
        using var transaction = Context.Database.BeginTransaction();

        if (!CheckValidOrder(order.OrderItem)) throw new MessageException("Invalid order");

        decimal amount = PricePerOrder(order.OrderItem);

        // Todo: This should be nullable
        order.ExternalReferenceId = string.Empty;

        await CreateAsync(order);

        await RemoveItemsAsync(order.OrderItem);

        await Context.SaveChangesAsync();

        transaction.Commit();

        return amount;
    }
}