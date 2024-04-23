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
        /// Получить список всех кофе.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<CoffeeRespForAdminDto>))]
        public async Task<IActionResult> GetAllCoffees()
        {
            var coffees = await _adminService.GetAllCoffeesAsync();
            var coffeesResp = coffees.Select(c => _mapper.Map<CoffeeRespForAdminDto>(c));
            return Ok(coffeesResp);
        }
        
        [HttpGet("Id")]
        [ProducesResponseType(200, Type = typeof(CoffeeRespForAdminDto))]
        public async Task<IActionResult> GetCoffeeById([FromQuery] int id)
        {
            // var coffee = await _adminService.(id);
            // var coffeeResp = _mapper.Map<CoffeeRespForAdminDto>(coffee);
            return Ok();
        }
        
        /// <summary>
        /// Добавить кофе.
        /// </summary>
        /// <param name="coffeeAddReq"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(200, Type = typeof(CoffeeRespForAdminDto))]
        public async Task<IActionResult> AddCoffee([FromBody] CoffeeAddReq coffeeAddReq)
        {
            var coffee = _mapper.Map<Coffee>(coffeeAddReq);
            var resp = await _adminService.CreateNewCoffeeAsync(coffee);
            var coffeeResp = _mapper.Map<CoffeeRespForAdminDto>(resp);
            return Ok(coffeeResp);
        }
        /// <summary>
        /// Удалить кофе.
        /// </summary>
        /// <param name="coffeeReq"></param>
        /// <returns></returns>
        [HttpDelete]
        [ProducesResponseType(200, Type = typeof(bool))]
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
            var resp = _mapper.Map<CoffeeRespForAdminDto>(coffeeResp);
            return Ok(resp);
        }
    }
}
