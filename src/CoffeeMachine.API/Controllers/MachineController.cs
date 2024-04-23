using AutoMapper;
using CoffeeMachine.API.DTOs.Banknote;
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
        private readonly IAdminService _adminService;
        private readonly IMapper _mapper;
        
        public MachineController(IAdminService adminService, IMapper mapper)
        {
            _adminService = adminService;   
            _mapper = mapper;
        }
        
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Machine>))]
        public async Task<IActionResult> GetAllMachinesAsync()
        {
            var machines = await _adminService.GetAllMachinesAsync();
            return Ok(machines);
        }
        
        [HttpGet("{Id}")]
        [ProducesResponseType(200, Type = typeof(Machine))]
        public async Task<IActionResult> GetMachineByIdAsync(long id)
        {
            var machine = await _adminService.GetMachineByIdAsync(id);
            return Ok(machine);
        }

        [HttpPost]
        [ProducesResponseType(200, Type = typeof(Machine))]
        public async Task<IActionResult> CreateNewMachineAsync([FromBody] MachineCreateDto machineRequest)
        {
            var machine = _mapper.Map<Machine>(machineRequest);
            var machineResponse = await _adminService.CreateNewMachineAsync(machine);
            return Ok(machineResponse);
        }

        [HttpPut]
        [ProducesResponseType(200, Type = typeof(Machine))]
        public async Task<IActionResult> UpdateMachineAsync([FromBody] Machine machineRequest)
        {
            var machineResponse = await _adminService.UpdateMachineAsync(machineRequest);
            return Ok(machineResponse);
        }
        
        [HttpDelete("{Id}")]
        public async Task<IActionResult> DeleteMachineAsync(long id)
        {
            await _adminService.DeleteMachineAsync(id);
            return NoContent();
        }

        [HttpPost("AddCoffeeToMachines/{idMachine}")]
        public async Task<IActionResult> AddCoffeeToMachinesAsync(long idMachine, [FromBody] long idCoffee)
        {
            var machine = await _adminService.AddCoffeeInMachineAsync(idCoffee, idMachine);
            return Ok(machine);
        }

        [HttpPost("DeleteCoffeeFromMachines/{idMachine}")]
        public async Task<IActionResult> DeleteCoffeeFromMachinesAsync(
            [FromRoute] long idMachine, [FromBody] long idCoffee)
        {
            var machine = await _adminService.DeleteCoffeeFromMachineAsync(idCoffee, idMachine);
            return Ok(machine);
        }

        [HttpPost("AddBanknotesToMachines/{idMachine}")]
        public async Task<IActionResult> AddBanknoteToMachinesAsync(
            [FromRoute] long idMachine, [FromBody] IEnumerable<BanknoteDto> banknotesRequest)
        {
            var banknotes = banknotesRequest.Select(b => _mapper.Map<Banknote>(b));
            var response = await _adminService.AddBanknotesToMachineAsync(banknotes, idMachine);
            var machineResponse = _mapper.Map<MachineEditBalanseDto>(response);
            
            return Ok(machineResponse);
        }
        
        [HttpPost("DeleteBanknotesFromMachines/{idMachine}")]
        public async Task<IActionResult> DeleteBanknotesFromMachinesAsync(
            [FromRoute] long idMachine, [FromBody] IEnumerable<BanknoteDto> banknotesRequest)

        {
            var banknotes = banknotesRequest.Select(b => _mapper.Map<Banknote>(b));
            var response = await _adminService.SubtractBanknotesFromMachineAsync(banknotes, idMachine);
            var machineResponse = _mapper.Map<MachineEditBalanseDto>(response);
            
            return Ok(machineResponse);
        }

        [HttpGet("GetBanknotesByMachine/{idMachine}")]
        public async Task<IActionResult> GetBanknotesByMachineAsync([FromRoute] long idMachine)
        {
            var banknotes = await _adminService.GetBanknotesByMachineAsync(idMachine);
            var banknotesResponse = banknotes.Select(b => _mapper.Map<BanknoteDto>(b));
            
            return Ok(banknotesResponse);
        }
    }
}
