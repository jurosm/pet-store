using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PetStoreService.Application.Models.Request.Order;
using PetStoreService.Application.Models.Response.Order;
using PetStoreService.Application.Models.Services.Order;
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
        public async Task<ActionResult<IEnumerable<OrderListItem>>> GetAllOrders()
        {
            return Ok(await _orderService.GetAllOrdersAsync());
        }

        [HttpPost]
        public async Task<ActionResult<CreateOrderResponse>> Create([FromBody] OrderRequest orderRequest)
        {
            try
            {
                var order = await _orderService.CreateAsync(orderRequest);

                return base.CreatedAtAction(nameof(Create), order);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        [HttpPost("{orderId}/payment-intent")]
        public async Task<ActionResult<CreatePaymentIntentResponse>> CreatePaymentIntent([FromRoute] int orderId)
        {
            return Ok(await _orderService.CreatePaymentIntentAsync(new CreatePaymentIntentRequest { OrderId = orderId }));
        }

        [HttpGet("{orderId}")]
        public async Task<ActionResult<GetOrderResponse>> GetOrderById([FromRoute] int orderId)
        {
            return Ok(await _orderService.GetOrderById(orderId));
        }
    }
}