﻿using Microsoft.EntityFrameworkCore;
using PetStoreService.Application.Exceptions;
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
            Toy? toy = await Context.Toy.FindAsync(orderItem.ToyId) ?? throw new BadRequestException("Toy not found", "order.toy.not_found");
            toy.Quantity -= orderItem.Quantity;
        }
    }

    public async Task CreateOrderAndRemoveItems(Order order)
    {
        await using var transaction = await Context.Database.BeginTransactionAsync();

        string toyIds = string.Join(",", order.OrderItem.Select(oi => oi.ToyId));
#pragma warning disable EF1002 // Risk of vulnerability to SQL injection.
        var toys = await Context.Toy.FromSqlRaw($"select * from petstore.\"Toy\" where petstore.\"Toy\".\"Id\" in ({toyIds}) for update").ToListAsync();
#pragma warning restore EF1002 // Risk of vulnerability to SQL injection.

        if (!CheckValidOrder(order.OrderItem, toys)) throw new BadRequestException("Toy quantity exceeded.", "order.toy.quantity_exceeded");

        var total = order.OrderItem.Sum(x => x.Quantity * toys.Find(t => t.Id == x.ToyId)!.Price);
        order.Total = total;

        await RemoveItemsAsync(order.OrderItem);

        await CreateAsync(order);

        await transaction.CommitAsync();
    }

    public async Task<Order> GetOrderByIdAsync(int id)
    {
        return await Context.Order.FindAsync(id) ?? throw new NotFoundException("Order not found");
    }
}