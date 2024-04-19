using AutoMapper;
using CoffeeMachine.API.DTOs.Banknote;
using CoffeeMachine.API.DTOs.BanknoteToMachine;
using CoffeeMachine.API.DTOs.Coffee;
using CoffeeMachine.API.DTOs.CoffeesInMachine;
using CoffeeMachine.API.DTOs.Machine;
using CoffeeMachine.API.DTOs.Order;
using CoffeeMachine.Application.Interfaces.IServices;
using CoffeeMachine.Core.Models;
using Microsoft.AspNetCore.Mvc;

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
        public async Task<IActionResult> GetAllOrdersAsync()
        {
            var orders = await _adminService.GetAllOrdersAsync();
            var ordersResp = orders.Select(o => _mapper.Map<OrderRespForAdmin>(o));
            return Ok(ordersResp);
        }

        [HttpGet("Machines")]
        public async Task<IActionResult> GetAllMachines()
        {
            var machines = await _adminService.GetAllMachinesAsync();
            var machineResp = machines.Select(m => _mapper.Map<MachineRespForAdmin>(m));
            return Ok(machineResp);
        }

        [HttpPost("Machines/Banknotes")]
        public async Task<IActionResult> GetBanknotesFromMachine([FromBody] MachineReq machineReq)
        {
            var machine = _mapper.Map<Machine>(machineReq);
            var banknotes = await _adminService.GetBanknotesByMachineAsync(machine);
            var banknotesResp = banknotes.Select(b => _mapper.Map<BanknoteDto>(b));
            
            return Ok(banknotesResp);
        }

        [HttpPost("Machines")]
        public async Task<IActionResult> CreateMachine([FromBody] MachineAddReq machineReq)
        {
            var machine = _mapper.Map<Machine>(machineReq);
            var resp = await _adminService.CreateNewMachineAsync(machine);
            var machineResp = _mapper.Map<MachineAddResp>(resp);

            return Ok(machineResp);
        }

        [HttpDelete("Machines")]
        public async Task<IActionResult> DeleteMachine([FromBody] MachineReq machineReq)
        {
            var machine = _mapper.Map<Machine>(machineReq);
            var resp = await _adminService.DeleteMachineAsync(machine);
            return Ok(resp);
        }

        [HttpPost("Machines/Banknotes/Add")]
        public async Task<IActionResult> AddBanknotesToMachine([FromBody] AddSubstrBanknoteToMachineReq substrBanknoteToMachineReq)
        {
            var banknotesReq = substrBanknoteToMachineReq.Banknotes.Select(b => _mapper.Map<Banknote>(b)).ToList();
            var machineReq = _mapper.Map<Machine>(substrBanknoteToMachineReq.Machine);
            var resp = await _adminService.AddBanknotesToMachineAsync(banknotesReq, machineReq);
            var machineResp = _mapper.Map<MachineResp>(resp);
            return Ok(machineResp);
        }
        
        [HttpPost("Machines/Banknotes/Substract")]
        public async Task<IActionResult> SubtractBanknotesToMachine([FromBody] AddSubstrBanknoteToMachineReq substrBanknoteToMachineReq)
        {
            var banknotesReq = substrBanknoteToMachineReq.Banknotes.Select(b => _mapper.Map<Banknote>(b)).ToList();
            var machineReq = _mapper.Map<Machine>(substrBanknoteToMachineReq.Machine);
            var resp = await _adminService.SubtractBanknotesFromMachineAsync(banknotesReq, machineReq);
            var machineResp = _mapper.Map<MachineResp>(resp);
            return Ok(machineResp);
        }
        
        [HttpGet("Coffees")]
        public async Task<IActionResult> GetAllCoffees()
        {
            var coffees = await _adminService.GetAllCoffeesAsync();
            var coffeesResp = coffees.Select(c => _mapper.Map<CoffeeRespForAdmin>(c));
            return Ok(coffeesResp);
        }
        
        [HttpPost("Coffees/Add")]
        public async Task<IActionResult> AddCoffee([FromBody] CoffeeAddReq coffeeAddReq)
        {
            var coffee = _mapper.Map<Coffee>(coffeeAddReq);
            var resp = await _adminService.CreateNewCoffeeAsync(coffee);
            var coffeeResp = _mapper.Map<CoffeeRespForAdmin>(resp);
            return Ok(coffeeResp);
        }
        [HttpPost("Coffees/Delete")]
        public async Task<IActionResult> DeleteCoffee([FromBody] CoffeeReq coffeeReq)
        {
            var coffee = _mapper.Map<Coffee>(coffeeReq);
            var resp = await _adminService.DeleteCoffeeAsync(coffee);
            
            return Ok(resp);
        }

        [HttpPost("Machines/Coffee/Add")]
        public async Task<IActionResult> AddCoffeeToMachine([FromBody] AddDelCoffeeToMachine req)
        {
            var coffee = _mapper.Map<Coffee>(req.Coffee);
            var machine = _mapper.Map<Machine>(req.Machine);
            var resp = await _adminService.AddCoffeeToMachineAsync(coffee, machine);
            var machineResp = _mapper.Map<MachineResp>(resp);
            return Ok(machineResp);
        }
        
        [HttpPost("Machines/Coffee/Substract")]
        public async Task<IActionResult> SubstractCoffeeToMachine([FromBody] AddDelCoffeeToMachine req)
        {
            var coffee = _mapper.Map<Coffee>(req.Coffee);
            var machine = _mapper.Map<Machine>(req.Machine);
            var resp = await _adminService.DeleteCoffeeFromMachineAsync(coffee, machine);
            var machineResp = _mapper.Map<MachineResp>(resp);
            return Ok(machineResp);
        }
    }
}
