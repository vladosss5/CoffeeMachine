using AutoMapper;
using CoffeeMachine.API.DTOs.Banknote;
using CoffeeMachine.API.DTOs.BanknoteToMachine;
using CoffeeMachine.API.DTOs.Machine;
using CoffeeMachine.API.DTOs.Order;
using CoffeeMachine.Application.Interfaces.IServices;
using CoffeeMachine.Core.Models;
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
        public async Task<IActionResult> AddBanknotesToMachine([FromBody] AddBanknoteToMachineReq banknoteToMachineReq)
        {
            var banknotesReq = banknoteToMachineReq.Banknotes.Select(b => _mapper.Map<Banknote>(b)).ToList();
            var machineReq = _mapper.Map<Machine>(banknoteToMachineReq.Machine);
            var resp = await _adminService.AddBanknotesToMachineAsync(banknotesReq, machineReq);
            var machineResp = _mapper.Map<MachineResp>(resp);
            return Ok(machineResp);
        }
    }
}
