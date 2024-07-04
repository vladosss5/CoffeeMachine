using AutoMapper;
using CoffeeMachine.API.DTOs.Banknote;
using CoffeeMachine.API.DTOs.BanknoteToMachine;
using CoffeeMachine.API.DTOs.Coffee;
using CoffeeMachine.API.DTOs.CoffeesInMachine;
using CoffeeMachine.API.DTOs.Machine;
using CoffeeMachine.Application.Interfaces.IServices;
using CoffeeMachine.Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CoffeeMachine.API.Controllers
{
    /// <summary>
    /// Контроллер для работы с кофемашинами.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class MachineController : ControllerBase
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
        /// <param name="adminService">Сервис администратора.</param>
        /// <param name="mapper">Сервис автомаппера.</param>
        public MachineController(IAdminService adminService, IMapper mapper)
        {
            _adminService = adminService;   
            _mapper = mapper;
        }
        
        /// <summary>
        /// Получить список всех кофемашин.
        /// </summary>
        /// <returns>Список кофемашин.</returns>
        [HttpGet]
        [Authorize(Policy = "AdminPolicy")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Machine>))]
        public async Task<IActionResult> GetAllMachinesAsync()
        {
            var machines = await _adminService.GetAllMachinesAsync();
            var machinesResponse = machines.Select(m => _mapper.Map<MachineDto>(m));
            return Ok(machinesResponse);
        }
        
        /// <summary>
        /// Получить кофемашину по Id.
        /// </summary>
        /// <param name="id">Идентификатор кофемашины.</param>
        /// <returns>Кофемашина.</returns>
        [HttpGet("{id}")]
        [Authorize(Policy = "AdminPolicy")]
        [ProducesResponseType(200, Type = typeof(Machine))]
        public async Task<IActionResult> GetMachineByIdAsync(long id)
        {
            var machine = await _adminService.GetMachineByIdAsync(id);
            var machineResponse = _mapper.Map<MachineDto>(machine);
            return Ok(machineResponse);
        }

        /// <summary>
        /// Создать новую̆ кофемашину.
        /// </summary>
        /// <param name="machineRequest">Кофемашина.</param>
        /// <returns>Кофемашина.</returns>
        [HttpPost]
        [Authorize(Policy = "AdminPolicy")]
        [ProducesResponseType(200, Type = typeof(Machine))]
        public async Task<IActionResult> CreateNewMachineAsync([FromBody] MachineCreateDto machineRequest)
        {
            var machine = _mapper.Map<Machine>(machineRequest);
            var result = await _adminService.CreateNewMachineAsync(machine);
            var machineResponse = _mapper.Map<MachineDto>(result);
            return Ok(machineResponse);
        }

        /// <summary>
        /// Изменить кофемашину.
        /// </summary>
        /// <param name="machineRequest">Кофемашина.</param>
        /// <returns>Кофемашина.</returns>
        [HttpPut]
        [Authorize(Policy = "AdminPolicy")]
        [ProducesResponseType(200, Type = typeof(Machine))]
        public async Task<IActionResult> UpdateMachineAsync([FromBody] MachineDto machineRequest)
        {
            var machine = _mapper.Map<Machine>(machineRequest);
            var response = await _adminService.UpdateMachineAsync(machine);
            var machineResponse = _mapper.Map<MachineDto>(response);
            return Ok(machineResponse);
        }
        
        /// <summary>
        /// Удалить кофемашину.
        /// </summary>
        /// <param name="id">Идентификатор кофемашины.</param>
        [HttpDelete("{id}")]
        [Authorize(Policy = "AdminPolicy")]
        public async Task<IActionResult> DeleteMachineAsync(long id)
        {
            await _adminService.DeleteMachineAsync(id);
            return NoContent();
        }

        /// <summary>
        /// Добавить кофе в кофемашину.
        /// </summary>
        /// <param name="machineId">Идентификатор кофемашины.</param>
        /// <param name="coffeeId">Идентификатор кофе.</param>
        /// <returns>Кофе в кофемашине.</returns>
        [HttpPost("AddCoffeeToMachines/{machineId}")]
        [Authorize(Policy = "AdminPolicy")]
        public async Task<IActionResult> AddCoffeeToMachinesAsync(long machineId, [FromBody] long coffeeId)
        {
            var machine = await _adminService.AddCoffeeInMachineAsync(coffeeId, machineId);
            var machineResponse = new CoffeesInMachineDto()
            {
                Machine = _mapper.Map<MachineDto>(machine),
                Coffees = machine.CoffeesToMachines.Select(cm => _mapper.Map<CoffeeDto>(cm.Coffee)).ToList()
            };
            
            return Ok(machineResponse);
        }

        /// <summary>
        /// Удалить кофе из кофемашины.
        /// </summary>
        /// <param name="machineId">Идентификатор кофемашины.</param>
        /// <param name="coffeeId">Идентификатор кофе.</param>
        /// <returns>Кофе в кофемашине.</returns>
        [HttpPost("DeleteCoffeeFromMachines/{machineId}")]
        [Authorize(Policy = "AdminPolicy")]
        public async Task<IActionResult> DeleteCoffeeFromMachinesAsync(
            [FromRoute] long machineId, [FromBody] long coffeeId)
        {
            var machine = await _adminService.DeleteCoffeeFromMachineAsync(coffeeId, machineId);
            var machineResponse = new CoffeesInMachineDto()
            {
                Machine = _mapper.Map<MachineDto>(machine),
                Coffees = machine.CoffeesToMachines.Select(cm => _mapper.Map<CoffeeDto>(cm.Coffee)).ToList()
            };
            
            return Ok(machineResponse);
        }
        
        /// <summary>
        /// Добавление банкнот в кофемашину.
        /// </summary>
        /// <param name="machineId">Идентификатор кофемашины.</param>
        /// <param name="banknotesRequest">Список банкнот.</param>
        /// <returns>Кофемашина.</returns>
        [HttpPost("AddBanknotesToMachines/{machineId}")]
        [Authorize(Policy = "AdminPolicy")]
        public async Task<IActionResult> AddBanknoteToMachinesAsync(
            [FromRoute] long machineId, [FromBody] IEnumerable<BanknoteDto> banknotesRequest)
        {
            var banknotes = banknotesRequest.Select(b => _mapper.Map<Banknote>(b));
            var response = await _adminService.AddBanknotesToMachineAsync(banknotes, machineId);
            var machineResponse = _mapper.Map<MachineDto>(response);
            
            return Ok(machineResponse);
        }
        
        /// <summary>
        /// Вычитание банкнот из кофемашины.
        /// </summary>
        /// <param name="machineId">Идентификатор кофемашины.</param>
        /// <param name="banknotesRequest">Список банкнот.</param>
        /// <returns>Кофемашина.</returns>
        [HttpPost("DeleteBanknotesFromMachines/{machineId}")]
        [Authorize(Policy = "AdminPolicy")]
        public async Task<IActionResult> SubtractBanknotesFromMachinesAsync(
            [FromRoute] long machineId, [FromBody] IEnumerable<BanknoteDto> banknotesRequest)
        {
            var banknotes = banknotesRequest.Select(b => _mapper.Map<Banknote>(b));
            var response = await _adminService.SubtractBanknotesFromMachineAsync(banknotes, machineId);
            var machineResponse = _mapper.Map<MachineDto>(response);
            
            return Ok(machineResponse);
        }

        /// <summary>
        /// Получить список банкнот в кофемашине.
        /// </summary>
        /// <param name="machineId">Идентификатор кофемашины.</param>
        /// <returns>Список банкнот в кофемашине.</returns>
        [HttpGet("GetBanknotesByMachine/{machineId}")]
        [Authorize(Policy = "AdminPolicy")]
        public async Task<IActionResult> GetBanknotesByMachineAsync([FromRoute] long machineId)
        {
            var banknotesToMachines = await _adminService.GetBanknotesByMachineAsync(machineId);
            var banknotesResponse = banknotesToMachines.Select(bm => _mapper.Map<BanknoteToMachineDto>(bm));
            
            return Ok(banknotesResponse);
        }
        
        /// <summary>
        /// Получить список кофе из кофемашины.
        /// </summary>
        /// <param name="machineId">Идентификатор кофемашины.</param>
        /// <returns>Список кофе.</returns>
        [HttpGet("GetCoffeesFromMachine/{machineId}")]
        [Authorize(Policy = "AdminPolicy")]
        public async Task<IActionResult> GetCoffeesFromMachineAsync(long machineId)
        {
            var response = await _adminService.GetCoffeesFromMachineAsync(machineId);
            var coffeesResponse = response.Select(c => _mapper.Map<CoffeeDto>(c)).ToList();
            
            return Ok(coffeesResponse);
        }
    }
}
