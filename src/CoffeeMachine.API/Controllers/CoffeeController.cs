using AutoMapper;
using CoffeeMachine.API.DTOs.Coffee;
using CoffeeMachine.Application.Interfaces.IServices;
using CoffeeMachine.Core.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CoffeeMachine.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoffeeController : ControllerBase
    {
        private readonly IAdminService _adminService;
        private readonly IMapper _mapper;
        
        public CoffeeController(IAdminService adminService, IMapper mapper)
        {
            _adminService = adminService;   
            _mapper = mapper;
        }
        
        /// <summary>
        /// Получить список всех кофе
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetAllCoffees()
        {
            var coffees = await _adminService.GetAllCoffeesAsync();
            var coffeesResp = coffees.Select(c => _mapper.Map<CoffeeRespForAdmin>(c));
            return Ok(coffeesResp);
        }
        
        /// <summary>
        /// Добавить кофе
        /// </summary>
        /// <param name="coffeeAddReq"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> AddCoffee([FromBody] CoffeeAddReq coffeeAddReq)
        {
            var coffee = _mapper.Map<Coffee>(coffeeAddReq);
            var resp = await _adminService.CreateNewCoffeeAsync(coffee);
            var coffeeResp = _mapper.Map<CoffeeRespForAdmin>(resp);
            return Ok(coffeeResp);
        }
        /// <summary>
        /// Удалить кофе
        /// </summary>
        /// <param name="coffeeReq"></param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<IActionResult> DeleteCoffee([FromBody] CoffeeReq coffeeReq)
        {
            var coffee = _mapper.Map<Coffee>(coffeeReq);
            var resp = await _adminService.DeleteCoffeeAsync(coffee);
            
            return Ok(resp);
        }

        /// <summary>
        /// Изменить кофе
        /// </summary>
        /// <param name="coffeeUpdateReq"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<IActionResult> UpdateCoffee([FromBody] CoffeeUpdateDto coffeeUpdateReq)
        {
            var coffee = _mapper.Map<Coffee>(coffeeUpdateReq);
            var coffeeResp = await _adminService.UpdateCoffeeAsync(coffee);
            var resp = _mapper.Map<CoffeeRespForAdmin>(coffeeResp);
            return Ok(resp);
        }
    }
}
