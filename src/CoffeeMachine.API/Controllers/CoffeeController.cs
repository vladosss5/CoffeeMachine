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
        /// <inheritdoc cref="IAdminService"/>
        /// </summary>
        private readonly IAdminService _adminService;
        
        /// <summary>
        /// Сервис автомаппера.
        /// </summary>
        private readonly IMapper _mapper;
        
        /// <summary>
        /// Конструктор класса.
        /// </summary>
        /// <param name="adminService">Cервис администратора.</param>
        /// <param name="mapper">Сервис автомаппера.</param>
        public CoffeeController(IAdminService adminService, IMapper mapper)
        {
            _adminService = adminService;   
            _mapper = mapper;
        }

        /// <summary>
        /// Получить кофе по Id.
        /// </summary>
        /// <param name="id">Идентификатор кофе.</param>
        /// <returns>Кофе.</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(Coffee))]
        public async Task<IActionResult> GetCoffeeByIdAsync(long id)
        {
            var coffeeResponse = await _adminService.GetCoffeeByIdAsync(id);
            return Ok(coffeeResponse);
        }

        /// <summary>
        /// Получить список всех кофе.
        /// </summary>
        /// <returns>Список кофе.</returns>
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Coffee>))]
        public async Task<IActionResult> GetAllCoffeesAsync()
        {
            var coffeesResponse = await _adminService.GetAllCoffeesAsync();
            return Ok(coffeesResponse);
        }

        /// <summary>
        /// Создать новый кофе.
        /// </summary>
        /// <param name="coffeeRequest"></param>
        /// <returns>Кофе.</returns>
        [HttpPost]
        [ProducesResponseType(200, Type = typeof(Coffee))]
        public async Task<IActionResult> CreateNewCoffeeAsync([FromBody] CoffeeCreateRequestDto coffeeRequest)
        {
            var coffee = _mapper.Map<Coffee>(coffeeRequest);
            var response = await _adminService.CreateNewCoffeeAsync(coffee);
            var coffeeResponse = _mapper.Map<CoffeeDto>(response);
            
            return Ok(coffeeResponse);
        }

        /// <summary>
        /// Изменить кофе.
        /// </summary>
        /// <param name="coffeeRequest">Изменяемый кофе.</param>
        /// <returns>Кофе.</returns>
        [HttpPut("{id}")]
        [ProducesResponseType(200, Type = typeof(Coffee))]
        public async Task<IActionResult> UpdateCoffeeAsync(CoffeeDto coffeeRequest)
        {
            var coffee = _mapper.Map<Coffee>(coffeeRequest);
            var response = await _adminService.UpdateCoffeeAsync(coffee);
            var coffeeResponse = _mapper.Map<CoffeeDto>(response);
            
            return Ok(coffeeResponse);
        }
        
        /// <summary>
        /// Удаление кофе.
        /// </summary>
        /// <param name="id">Идентификатор кофе.</param>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCoffeeAsync(long id)
        {
            await _adminService.DeleteCoffeeAsync(id);
            return NoContent();
        }
    }
}
