using AutoMapper;
using CoffeeMachine.API.DTOs.Banknote;
using CoffeeMachine.API.DTOs.BanknoteToMachine;
using CoffeeMachine.API.DTOs.Coffee;
using CoffeeMachine.API.DTOs.CoffeesInMachine;
using CoffeeMachine.API.DTOs.Machine;
using CoffeeMachine.Application.Interfaces.IServices;
using CoffeeMachine.Core.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CoffeeMachine.API.Controllers
{
    /// <summary>
    /// Класс управления кофемашинами.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class MachineController : ControllerBase
    {
        /// <summary>
        /// Сервис администратора.
        /// </summary>
        private readonly IAdminService _adminService;
        
        /// <summary>
        /// Сервис автомаппера.
        /// </summary>
        private readonly IMapper _mapper;
        
        public MachineController(IAdminService adminService, IMapper mapper)
        {
            _adminService = adminService;   
            _mapper = mapper;
        }
        
        /// <summary>
        /// Контроллер для получения списка всех кофемашин.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Machine>))]
        public async Task<IActionResult> GetAllMachinesAsync()
        {
            var machines = await _adminService.GetAllMachinesAsync();
            var machinesResponse = machines.Select(m => _mapper.Map<MachineFullResponseDto>(m));
            return Ok(machinesResponse);
        }
        
        /// <summary>
        /// Контроллер для получения кофемашины по Id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(Machine))]
        public async Task<IActionResult> GetMachineByIdAsync(long id)
        {
            var machine = await _adminService.GetMachineByIdAsync(id);
            var machineResponse = _mapper.Map<MachineFullResponseDto>(machine);
            return Ok(machineResponse);
        }

        /// <summary>
        /// Контроллер для создания новой кофемашины.
        /// </summary>
        /// <param name="machineRequest"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(200, Type = typeof(Machine))]
        public async Task<IActionResult> CreateNewMachineAsync([FromBody] MachineCreateDto machineRequest)
        {
            var machine = _mapper.Map<Machine>(machineRequest);
            var result = await _adminService.CreateNewMachineAsync(machine);
            var machineResponse = _mapper.Map<MachineFullResponseDto>(result);
            return Ok(machineResponse);
        }

        /// <summary>
        /// Контроллер для изменения кофемашины.
        /// </summary>
        /// <param name="machineRequest"></param>
        /// <returns></returns>
        [HttpPut]
        [ProducesResponseType(200, Type = typeof(Machine))]
        public async Task<IActionResult> UpdateMachineAsync([FromBody] MachineEditDto machineRequest)
        {
            var machine = _mapper.Map<Machine>(machineRequest);
            var response = await _adminService.UpdateMachineAsync(machine);
            var machineResponse = _mapper.Map<MachineFullResponseDto>(response);
            return Ok(machineResponse);
        }
        
        /// <summary>
        /// Контроллер для удаления кофемашины.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMachineAsync(long id)
        {
            await _adminService.DeleteMachineAsync(id);
            return NoContent();
        }

        /// <summary>
        /// Контроллер для добавления кофе в кофемашину.
        /// </summary>
        /// <param name="machineId"></param>
        /// <param name="coffeeId"></param>
        /// <returns></returns>
        [HttpPost("AddCoffeeToMachines/{machineId}")]
        public async Task<IActionResult> AddCoffeeToMachinesAsync(long machineId, [FromBody] long coffeeId)
        {
            var machine = await _adminService.AddCoffeeInMachineAsync(coffeeId, machineId);
            var machineResponse = new CoffeesInMachineResponseDto()
            {
                Machine = _mapper.Map<MachineFullResponseDto>(machine),
                Coffees = machine.CoffeesToMachines.Select(cm => _mapper.Map<CoffeeFullResponseDto>(cm.Coffee)).ToList()
            };
            
            return Ok(machineResponse);
        }

        /// <summary>
        /// Контроллер для удаления кофе из кофемашины.
        /// </summary>
        /// <param name="machineId"></param>
        /// <param name="coffeeId"></param>
        /// <returns></returns>
        [HttpPost("DeleteCoffeeFromMachines/{machineId}")]
        public async Task<IActionResult> DeleteCoffeeFromMachinesAsync(
            [FromRoute] long machineId, [FromBody] long coffeeId)
        {
            var machine = await _adminService.DeleteCoffeeFromMachineAsync(coffeeId, machineId);
            var machineResponse = new CoffeesInMachineResponseDto()
            {
                Machine = _mapper.Map<MachineFullResponseDto>(machine),
                Coffees = machine.CoffeesToMachines.Select(cm => _mapper.Map<CoffeeFullResponseDto>(cm.Coffee)).ToList()
            };
            
            return Ok(machineResponse);
        }
        
        /// <summary>
        /// Контроллер для добавления банкнот в кофемашину.
        /// </summary>
        /// <param name="machineId"></param>
        /// <param name="banknotesRequest"></param>
        /// <returns></returns>
        [HttpPost("AddBanknotesToMachines/{machineId}")]
        public async Task<IActionResult> AddBanknoteToMachinesAsync(
            [FromRoute] long machineId, [FromBody] IEnumerable<BanknoteDto> banknotesRequest)
        {
            var banknotes = banknotesRequest.Select(b => _mapper.Map<Banknote>(b));
            var response = await _adminService.AddBanknotesToMachineAsync(banknotes, machineId);
            var machineResponse = _mapper.Map<MachineEditDto>(response);
            
            return Ok(machineResponse);
        }
        
        /// <summary>
        /// Контроллер для вычитания банкнот из кофемашины.
        /// </summary>
        /// <param name="machineId"></param>
        /// <param name="banknotesRequest"></param>
        /// <returns></returns>
        [HttpPost("DeleteBanknotesFromMachines/{machineId}")]
        public async Task<IActionResult> SubtractBanknotesFromMachinesAsync(
            [FromRoute] long machineId, [FromBody] IEnumerable<BanknoteDto> banknotesRequest)

        {
            var banknotes = banknotesRequest.Select(b => _mapper.Map<Banknote>(b));
            var response = await _adminService.SubtractBanknotesFromMachineAsync(banknotes, machineId);
            var machineResponse = _mapper.Map<MachineEditDto>(response);
            
            return Ok(machineResponse);
        }

        /// <summary>
        /// Контроллер для получения списка банкнот в кофемашине.
        /// </summary>
        /// <param name="machineId"></param>
        /// <returns></returns>
        [HttpGet("GetBanknotesByMachine/{machineId}")]
        public async Task<IActionResult> GetBanknotesByMachineAsync([FromRoute] long machineId)
        {
            var banknotesToMachines = await _adminService.GetBanknotesByMachineAsync(machineId);
            var banknotesResponse = banknotesToMachines.Select(bm => _mapper.Map<BanknoteToMachineResponseDto>(bm));
            
            return Ok(banknotesResponse);
        }
    }
}
