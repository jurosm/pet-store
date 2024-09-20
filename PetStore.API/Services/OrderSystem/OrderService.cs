﻿using AutoMapper;
using Microsoft.AspNetCore.Http;
using PetStore.API.Db;
using PetStore.API.Exceptions.Services.Order;
using PetStore.API.Models.Request.Order;
using PetStore.API.Models.Response.ExternalServices;
using PetStore.API.Models.Response.Order;
using PetStore.API.Models.Services.Order;
using PetStore.API.Services.ExternalServices;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetStore.API.Services.OrderSystem
{
    public class OrderService(IHttpContextAccessor accessor, OrderRepository orderRepository, IPInfoService ipInfo, StripeService stripeService, IMapper mapper)
    {
        private readonly IHttpContextAccessor Accessor = accessor;
        private readonly OrderRepository OrderRepository = orderRepository;
        private readonly IPInfoService IPInfo = ipInfo;
        private readonly StripeService StripeService = stripeService;
        private readonly IMapper Mapper = mapper;

        public IEnumerable<OrderListItem> GetAllOrders()
        {
            return OrderRepository.ReadAll().OrderByDescending(x => x.OrderDate).Select(x => Mapper.Map<OrderListItem>(x));
        }

        public async Task<OrderInfo> Buy(OrderRequest orderRequest)
        {
            if (!OrderRepository.CheckValidOrder(orderRequest.OrderItems)) throw new MessageException("Invalid order");
            decimal amount = OrderRepository.PricePerOrder(orderRequest.OrderItems);

            Order order = Mapper.Map<Order>(orderRequest);
            await InitializeOrderAsync(orderRequest, order);

            string chargeId = StripeService.CreateCharge(orderRequest.TokenId, (long)amount);
            order.ExternalReferenceId = chargeId;

            string status = await StripeService.GetStatusAsync(chargeId);
            order.OrderStatus = status;

            await OrderRepository.UpdateAsync(order);

            if (order.OrderStatus == "succeeded" || order.OrderStatus == "amount_capturable_updated")
            {
                await OrderRepository.RemoveItemsAsync(orderRequest.OrderItems);
                return new OrderInfo() { Message = "Transaction succeeded!" };
            }

            return new OrderInfo() { Message = "Payment failed!" };
        }
        private async Task InitializeOrderAsync(OrderRequest orderRequest, Order order)
        {
            var address = Accessor.HttpContext.Connection.RemoteIpAddress.ToString();
            IPInfoResponse ipInfoAddress = await IPInfo.GetLocation(address);

            orderRequest.OrderItems.ForEach(x => order.OrderItem.Add(Mapper.Map<OrderItem>(x)));
            order.ShippingAddress = orderRequest.Country + "," + orderRequest.City + "," + orderRequest.StreetAddress;
            order.IpinfoAddress = ipInfoAddress.Country + "," + ipInfoAddress.City;
            order.OrderStatus = "pending";
            await OrderRepository.CreateAsync(order);
        }
    }
}