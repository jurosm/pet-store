using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PetStore.API.Models.Request.Order;
using PetStore.API.Models.Response.Order;
using PetStore.API.Services.OrderSystem;

namespace PetStore.API.Controllers
{
    [Route("/orders")]

    public class OrderController(OrderService orderService) : BaseApiController
    {
        private readonly OrderService OrderService = orderService;

        [HttpGet]
        [Authorize]
        public IEnumerable<OrderListItem> GetAllOrders()
        {
            return OrderService.GetAllOrders();
        }

        [HttpPost("buy")]
        public async Task<IActionResult> Buy([FromBody] OrderRequest orderRequest)
        {
            return Ok(await OrderService.Buy(orderRequest));
        }
    }
}
