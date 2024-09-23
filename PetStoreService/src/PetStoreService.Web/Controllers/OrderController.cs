using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PetStoreService.Application.Models.Request.Order;
using PetStoreService.Application.Models.Response.Order;
using PetStoreService.Application.Services.OrderSystem;
using System;
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

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] OrderRequest orderRequest)
        {
            try
            {
                var order = await _orderService.Create(orderRequest);

                return base.CreatedAtAction(nameof(Create), order);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }

        }
    }
}