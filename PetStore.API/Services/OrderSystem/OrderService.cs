using AutoMapper;
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

        public async Task<IActionResult> Buy(OrderRequest orderRequest)
        {
            var address = Accessor.HttpContext.Connection.RemoteIpAddress.ToString();
            IPInfoResponse ipInfoAddress = await IPInfo.GetLocation(address);

            if (!OrderRepository.CheckValidOrder(orderRequest.OrderItems)) return new BadRequestResult();
            decimal amount = OrderRepository.PricePerOrder(orderRequest.OrderItems);

            string chargeId = StripeService.CreateCharge(orderRequest.TokenId, (long)amount);
            string status = await StripeService.GetStatusAsync(chargeId);

            Order order = Mapper.Map<Order>(orderRequest);
            orderRequest.OrderItems.ForEach(x => order.OrderItem.Add(Mapper.Map<OrderItem>(x)));
            order.ShippingAddress = ipInfoAddress.Country + "," + ipInfoAddress.City;
            order.IpinfoAddress = ipInfoAddress.Country + "" + ipInfoAddress.City;
            order.ExternalReferenceId = chargeId;
            order.OrderStatus = "pending";
            order.OrderStatus = status;
            await OrderRepository.CreateAsync(order);

            return new StatusCodeResult(200);
        }
    }
}
