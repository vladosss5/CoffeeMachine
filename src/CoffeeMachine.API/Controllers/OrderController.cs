using AutoMapper;
using CoffeeMachine.API.DTOs;
using CoffeeMachine.API.DTOs.Order;
using CoffeeMachine.Application.Interfaces.IServices;
using CoffeeMachine.Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace CoffeeMachine.API.Controllers
{
    /// <summary>
    /// Контроллер для работы с заказами.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        /// <summary>
        /// Сервис автомаппера.
        /// </summary>
        private readonly IMapper _mapper;
        
        /// <summary>
        /// Сервис для работы с заказами.
        /// </summary>
        private readonly IOrderService _orderService;
        
        /// <summary>
        /// Сервис администратора.
        /// </summary>
        private readonly IAdminService _adminService;

        public OrderController(IMapper mapper, IOrderService orderService, IAdminService adminService)
        {
            _mapper = mapper;
            _orderService = orderService;
            _adminService = adminService;
        }

        /// <summary>
        /// Контроллер для получения заказа по Id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrderById(long id)
        {
            var result = await _adminService.GetOrderByIdAsync(id);
            var orderResponse = _mapper.Map<OrderResponseDto>(result);
            return Ok(orderResponse);
        }

        /// <summary>
        /// Контроллер для создания нового заказа.
        /// </summary>
        /// <param name="orderRequest"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> CreateOrder([FromBody] OrderAddRequestDto orderRequest)
        {
            var order = _mapper.Map<OrderAddResponseDto>(await _orderService.CreateOrderAsync(_mapper.Map<Order>(orderRequest)));
            return Ok(order);
        }
    }
}
