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
    /// <summary>API системы заказов</summary>
    [ApiController]
    [Route(WebAPIAddresses.Orders)]
    public class OrdersApiController : ControllerBase
    {
        private readonly IOrderService _OrderService;
        public OrdersApiController(IOrderService OrderService) 
        {
            _OrderService = OrderService;
        }
        /// <summary>Получить список заказов указанного пользователя</summary>
        /// <param name="UserName">Имя пользователя</param>
        /// <returns>Список заказов указанного пользователя</returns>
        [HttpGet("user/{UserName}")] //api/orders/user/Petrov
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<OrderDTO>))]
        public async Task<IActionResult> GetUserOrders(string UserName)
        {
            var orders = await _OrderService.GetUserOrder(UserName);
            return Ok(orders.ToDTO());
        }

        /// <summary>Получить заказ по его идентификатору</summary>
        /// <param name="id">Идентификатор заказа</param>
        /// <returns>Информация по заказу с указанным идентификатором</returns>
        
        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(OrderDTO))]
        public async  Task<IActionResult> GetOrderById(int id)
        {
            var order = await _OrderService.GetOrderById(id);
            return Ok(order.ToDTO());
        }
        /// <summary>Создание нового заказа</summary>
        /// <param name="UserName">Имя пользователя, для которого оформляется заказ</param>
        /// <param name="OrderModel">Информация о заказе</param>
        /// <returns>Сформированный заказ</returns>
        [HttpPost("{UserName}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(OrderDTO))]
        public async Task<IActionResult> CreateOrders(string UserName, [FromBody] CreateOrderDTO OrderModel)
        {
            var order = await _OrderService.CreateOrder(UserName, OrderModel.Items.ToCartView(), OrderModel.Order);
            return Ok(order.ToDTO());
        }
    }
}
