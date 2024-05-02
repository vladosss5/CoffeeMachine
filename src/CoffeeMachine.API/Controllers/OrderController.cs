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
        /// <inheritdoc cref="IOrderService"/>
        /// </summary>
        private readonly IOrderService _orderService;
        
        /// <summary>
        /// <inheritdoc cref="IAdminService"/>
        /// </summary>
        private readonly IAdminService _adminService;

        /// <summary>
        /// Конструктор класса.
        /// </summary>
        /// <param name="mapper">Сервис автомаппера.</param>
        /// <param name="orderService">Сервис заказов.</param>
        /// <param name="adminService">Сервис администратора.</param>
        public OrderController(IMapper mapper, IOrderService orderService, IAdminService adminService)
        {
            _mapper = mapper;
            _orderService = orderService;
            _adminService = adminService;
        }

        /// <summary>
        /// Получить заказ по Id.
        /// </summary>
        /// <param name="id">Идентификатор заказа.</param>
        /// <returns>Заказ.</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrderByIdAsync(long id)
        {
            var result = await _adminService.GetOrderByIdAsync(id);
            var orderResponse = _mapper.Map<OrderResponseDto>(result);
            return Ok(orderResponse);
        }

        /// <summary>
        /// Создать новый заказ.
        /// </summary>
        /// <param name="orderRequest">Запрос заказа.</param>
        /// <returns>Заказ.</returns>
        [HttpPost]
        public async Task<IActionResult> CreateOrderAsync([FromBody] OrderAddRequestDto orderRequest)
        {
            var order = _mapper.Map<OrderAddResponseDto>(await _orderService.CreateOrderAsync(_mapper.Map<Order>(orderRequest)));
            return Ok(order);
        }
    }
}
