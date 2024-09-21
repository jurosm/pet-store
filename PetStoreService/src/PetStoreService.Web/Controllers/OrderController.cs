﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PetStoreService.Application.Models.Request.Order;
using PetStoreService.Application.Models.Response.Order;
using PetStoreService.Application.Services.OrderSystem;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PetStoreService.Web.Controllers
{
    [Route("/order")]

    public class OrderController(OrderService orderService) : BaseApiController
    {
        private readonly OrderService _orderService = orderService;

        [HttpGet]
        [Authorize]
        public ActionResult<IEnumerable<OrderListItem>> GetAllOrders()
        {
            return Ok(_orderService.GetAllOrders());
        }

        [HttpPost("buy")]
        public async Task<IActionResult> Buy([FromBody] OrderRequest orderRequest)
        {
            return Ok(await _orderService.Buy(orderRequest));
        }
    }
}