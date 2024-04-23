using AutoMapper;
using CoffeeMachine.API.DTOs;
using CoffeeMachine.API.DTOs.Order;
using CoffeeMachine.Application.Interfaces.IServices;
using CoffeeMachine.Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace CoffeeMachine.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IOrderService _orderService;
        private readonly IAdminService _adminService;

        public OrderController(IMapper mapper, IOrderService orderService, IAdminService adminService)
        {
            _mapper = mapper;
            _orderService = orderService;
            _adminService = adminService;
        }
        
        /// <summary>
        /// Получить список всех заказов
        /// </summary>
        /// <returns></returns>
        // [HttpGet]
        // public async Task<IActionResult> GetAllOrdersAsync()
        // {
        //     var orders = await _adminService.GetAllOrdersAsync();
        //     var ordersResp = orders.Select(o => _mapper.Map<OrderRespForAdmin>(o));
        //     return Ok(ordersResp);
        // }
        
        /// <summary>
        /// Создать новый заказ
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
