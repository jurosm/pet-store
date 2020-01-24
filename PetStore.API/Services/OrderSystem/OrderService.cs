using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using PetStore.API.Models.Request.Order;
using PetStore.API.Models.Response.ExternalServices;
using PetStore.API.Services.ExternalServices;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetStore.API.Services.OrderSystem
{
    public class OrderService
    {
        private IHttpContextAccessor Accessor;
        private readonly OrderRepository OrderRepository;
        private readonly IPInfoService IPInfo;
        private readonly StripeService StripeService;

        public OrderService(IHttpContextAccessor accessor, OrderRepository orderRepository, IPInfoService ipInfo, StripeService stripeService)
        {
            this.Accessor = accessor;
            this.OrderRepository = orderRepository;
            this.IPInfo = ipInfo;
            this.StripeService = stripeService;
        }

        public async Task Buy(OrderRequest orderRequest)
        {
            var address = Accessor.HttpContext.Connection.RemoteIpAddress.ToString();
            IPInfoResponse ipInfoAddress = await IPInfo.GetLocation("31.223.140.239");

        }
    }
}
