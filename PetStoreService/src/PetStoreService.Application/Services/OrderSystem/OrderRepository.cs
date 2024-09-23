using Microsoft.EntityFrameworkCore;
using PetStoreService.Application.Exceptions.Services.Order;
using PetStoreService.Domain.Entities;
using PetStoreService.Persistence;

namespace PetStoreService.Application.Services.OrderSystem;

public class OrderRepository(PetStoreDBContext context) : Repository<Order>(context)
{
    public static bool CheckValidOrder(ICollection<OrderItem> orderItems, ICollection<Toy> toys)
    {
        return toys.All(toy => orderItems.Any(oi => (oi.ToyId == toy.Id) && (oi.Quantity <= toy.Quantity)));
    }

    public static decimal PricePerOrder(ICollection<OrderItem> orderItems, List<Toy> toys)
    {
        return orderItems.Sum(x => toys.Find(y => y.Id == x.ToyId)!.Price * x.Quantity);
    }

    public async Task RemoveItemsAsync(ICollection<OrderItem> orderItems)
    {
        foreach (var orderItem in orderItems)
        {
            Toy? toy = await Context.Toy.FindAsync(orderItem.ToyId);
            if (toy == null)
            {
                throw new Exception("Toy not found");
            }
            toy.Quantity -= orderItem.Quantity;
        }
    }

    public async Task<decimal> CreateOrderAndRemoveItems(Order order)
    {
        await using var transaction = await Context.Database.BeginTransactionAsync();

        string toyIds = string.Join(",", order.OrderItem.Select(oi => oi.ToyId));
        var toys = await Context.Toy.FromSqlRaw($"select * from petstore.\"Toy\" where petstore.\"Toy\".\"Id\" in ({toyIds}) for update").ToListAsync();

        if (!CheckValidOrder(order.OrderItem, toys)) throw new MessageException("Invalid order");

        await RemoveItemsAsync(order.OrderItem);

        decimal amount = PricePerOrder(order.OrderItem, toys);

        // Todo: This should be nullable
        order.ExternalReferenceId = string.Empty;

        await CreateAsync(order);

        await Context.SaveChangesAsync();

        await transaction.CommitAsync();

        return amount;
    }
}