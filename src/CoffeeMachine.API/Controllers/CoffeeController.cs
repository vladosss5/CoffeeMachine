using AutoMapper;
using CoffeeMachine.API.DTOs.Coffee;
using CoffeeMachine.Application.Interfaces.IServices;
using CoffeeMachine.Core.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CoffeeMachine.API.Controllers
{
    /// <summary>
    /// Контроллер для работы с кофе.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class CoffeeController : ControllerBase
    {
        /// <summary>
        /// Сервис администратора.
        /// </summary>
        private readonly IAdminService _adminService;
        
        /// <summary>
        /// Сервис автомаппера.
        /// </summary>
        private readonly IMapper _mapper;
        
        public CoffeeController(IAdminService adminService, IMapper mapper)
        {
            _adminService = adminService;   
            _mapper = mapper;
        }

        /// <summary>
        /// Контроллер для получения кофе по Id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(Coffee))]
        public async Task<IActionResult> GetCoffeeByIdAsync(long id)
        {
            var coffeeResponse = await _adminService.GetCoffeeByIdAsync(id);
            return Ok(coffeeResponse);
        }

        /// <summary>
        /// Контроллер для получения списка всех кофе.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Coffee>))]
        public async Task<IActionResult> GetAllCoffeesAsync()
        {
            var coffeesResponse = await _adminService.GetAllCoffeesAsync();
            return Ok(coffeesResponse);
        }

        /// <summary>
        /// Контроллер для создания нового кофе.
        /// </summary>
        /// <param name="coffeeRequest"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(200, Type = typeof(Coffee))]
        public async Task<IActionResult> CreateNewCoffeeAsync([FromBody] CoffeeReqestDto coffeeRequest)
        {
            var coffee = _mapper.Map<Coffee>(coffeeRequest);
            var response = await _adminService.CreateNewCoffeeAsync(coffee);
            var coffeeResponse = _mapper.Map<CoffeeFullResponseDto>(response);
            
            return Ok(coffeeResponse);
        }

        /// <summary>
        /// Контроллер для изменения кофе.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="coffeeRequest"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [ProducesResponseType(200, Type = typeof(Coffee))]
        public async Task<IActionResult> UpdateCoffeeAsync([FromRoute] long id, [FromBody] CoffeeReqestDto coffeeRequest)
        {
            var coffee = _mapper.Map<Coffee>(coffeeRequest);
            coffee.Id = id;
            var response = await _adminService.UpdateCoffeeAsync(coffee);
            var coffeeResponse = _mapper.Map<CoffeeFullResponseDto>(response);
            
            return Ok(coffeeResponse);
        }
        
        /// <summary>
        /// Удаление кофе.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCoffeeAsync(long id)
        {
            await _adminService.DeleteCoffeeAsync(id);
            return NoContent();
        }

        /// <summary>
        /// Получение списка кофе из кофемашины.
        /// </summary>
        /// <param name="machineId"></param>
        /// <returns></returns>
        [HttpGet("GetCoffeesFromMachine/{machineId}")]
        public async Task<IActionResult> GetCoffeesFromMachineAsync(long machineId)
        {
            var response = await _adminService.GetCoffeesFromMachineAsync(machineId);
            var coffeesResponse = response.Select(c => _mapper.Map<CoffeeFullResponseDto>(c)).ToList();
            
            return Ok(coffeesResponse);
        }
    }
}
