using AutoMapper;
using CoffeeMachine.API.DTOs.Order;
using CoffeeMachine.Application.Interfaces.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol;

namespace CoffeeMachine.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IAdminService _adminService;
        private readonly IMapper _mapper;
        
        public AdminController(IAdminService adminService, IMapper mapper)
        {
            _adminService = adminService;   
            _mapper = mapper;
        }

        [HttpGet("Orders")]
        public async Task<IActionResult> GetAllMachinesAsync()
        {
            var orders = await _adminService.GetAllOrdersAsync();
            var ordersResp = orders.Select(o => _mapper.Map<OrderRespForAdmin>(o));
            return Ok(ordersResp);
        }
    }
}
