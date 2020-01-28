using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PetStore.API.Models.Request.Order;
using PetStore.API.Models.Response;
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
        public async Task<IActionResult> Buy([FromBody]OrderRequest orderRequest)
        {
            return Ok(await OrderService.Buy(orderRequest));
        }
    }
}
