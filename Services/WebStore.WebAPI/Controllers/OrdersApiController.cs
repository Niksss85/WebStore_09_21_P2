using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStore.Domain.DTO;
using WebStore.Interfaces;
using WebStore.Interfaces.Services;

namespace WebStore.WebAPI.Controllers { 

    [ApiController]
    [Route(WebAPIAddresses.Orders)]
    public class OrdersApiController : ControllerBase
    {
        private readonly IOrderService _OrderService;
        public OrdersApiController(IOrderService OrderService) 
        {
            _OrderService = OrderService;
        }
        [HttpGet("user/{UserName}")] //api/orders/user/Petrov
        public async Task<IActionResult> GetUserOrders(string UserName)
        {
            var orders = await _OrderService.GetUserOrder(UserName);
            return Ok(orders.ToDTO());
        }
        [HttpGet("{id:int}")] 
        public async  Task<IActionResult> GetOrderById(int id)
        {
            var order = await _OrderService.GetOrderById(id);
            return Ok(order.ToDTO());
        }
        [HttpPost("{UserName}")]

        public async Task<IActionResult> CreateOrders(string UserName, [FromBody] CreateOrderDTO OrderModel)
        {
            var order = await _OrderService.CreateOrder(UserName, OrderModel.Items.ToCartView(), OrderModel.Order);
            return Ok(order.ToDTO());
        }
    }
}
