﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PetStore.API.Models.Request.Order;
using PetStore.API.Services.OrderSystem;

namespace PetStore.API.Controllers
{
    [Route("/orders")]

    public class OrderController : BaseApiController
    {
        private readonly OrderService OrderService;
        public OrderController(OrderService orderService)
        {
            this.OrderService = orderService;
        }

        [HttpPost("buy")]
        public void Buy([FromBody]OrderRequest orderRequest)
        {
            OrderService.Buy(orderRequest);
        }
    }
}
