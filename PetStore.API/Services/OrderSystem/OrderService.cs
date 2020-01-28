﻿using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PetStore.API.Db;
using PetStore.API.Models.Request.Order;
using PetStore.API.Models.Response.ExternalServices;
using PetStore.API.Services.ExternalServices;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PetStore.API.Services.OrderSystem
{
    public class OrderService
    {
        private IHttpContextAccessor Accessor;
        private readonly OrderRepository OrderRepository;
        private readonly IPInfoService IPInfo;
        private readonly StripeService StripeService;
        private readonly IMapper Mapper;

        public OrderService(IHttpContextAccessor accessor, OrderRepository orderRepository, IPInfoService ipInfo, StripeService stripeService, IMapper mapper)
        {
            this.Accessor = accessor;
            this.OrderRepository = orderRepository;
            this.IPInfo = ipInfo;
            this.StripeService = stripeService;
            this.Mapper = mapper;
        }
        //order
        public async Task<string> Buy(OrderRequest orderRequest)
        {
            var address = Accessor.HttpContext.Connection.RemoteIpAddress.ToString();
            IPInfoResponse ipInfoAddress = await IPInfo.GetLocation(address);

            if (!OrderRepository.CheckValidOrder(orderRequest.OrderItems)) return "Items not valid!";

            decimal amount = OrderRepository.PricePerOrder(orderRequest.OrderItems);
            //order.
            Order order = Mapper.Map<Order>(orderRequest);
            orderRequest.OrderItems.ForEach(x => order.OrderItem.Add(Mapper.Map<OrderItem>(x)));
            order.ShippingAddress = ipInfoAddress.Country + "," + ipInfoAddress.City;
            order.IpinfoAddress = ipInfoAddress.Country + "," + ipInfoAddress.City;
            order.OrderStatus = "pending";
            await OrderRepository.CreateAsync(order);

            string chargeId = StripeService.CreateCharge(orderRequest.TokenId, (long)amount);
            order.ExternalReferenceId = chargeId;

            string status = await StripeService.GetStatusAsync(chargeId);
            order.OrderStatus = status;

            await OrderRepository.UpdateAsync(order);

            if(order.OrderStatus == "succeeded" || order.OrderStatus == "amount_capturable_updated")
            {
                await OrderRepository.RemoveItemsAsync(orderRequest.OrderItems);
                return "Transaction succeeded!";
            }

            return "Payment failed!";
        }
    }
}
