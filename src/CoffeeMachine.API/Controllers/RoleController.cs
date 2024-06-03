using AutoMapper;
using CoffeeMachine.API.DTOs.Role;
using CoffeeMachine.Application.Interfaces.IServices;
using CoffeeMachine.Core.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CoffeeMachine.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly IRoleService _roleService;
        private readonly IMapper _mapper;
        
        public RoleController(IRoleService roleService, IMapper mapper)
        {
            _roleService = roleService;
            _mapper = mapper;
        }
        
        [HttpGet]
        public async Task<IActionResult> GetAllRolesAsync()
        {
            var roles = await _roleService.GetAllRolesAsync();
            var response = _mapper.Map<List<RoleResponseDto>>(roles);
            return Ok(response);
        }
        
        [HttpGet("GetById/{id}")]
        public async Task<IActionResult> GetRoleByIdAsync(long id)
        {
            var role = await _roleService.GetRoleByIdAsync(id);
            var response = _mapper.Map<RoleResponseDto>(role);
            return Ok(response);
        }
        
        [HttpGet("GetByName/{name}")]
        public async Task<IActionResult> GetRoleByNameAsync(string name)
        {
            var role = await _roleService.GetRoleByNameAsync(name);
            var response = _mapper.Map<RoleResponseDto>(role);
            return Ok(response);
        }
        
        [HttpPost("Create")]
        public async Task<IActionResult> CreateRoleAsync(RoleCreateRequestDto role)
        {
            var newRole = _mapper.Map<Role>(role);
            var createdRole = await _roleService.CreateRoleAsync(newRole);
            var response = _mapper.Map<RoleResponseDto>(createdRole);
            return Ok(response);
        }

        [HttpPut("Update")]
        public async Task<IActionResult> UpdateRoleAsync(RoleUpdateRequestDto role)
        {
            var updatingRole = _mapper.Map<Role>(role);
            var updatedRole = await _roleService.UpdateRoleAsync(updatingRole);
            var response = _mapper.Map<RoleResponseDto>(updatedRole);
            return Ok(response);
        }

        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> DeleteRoleAsync(long id)
        {
            var deletingRole = new Role { Id = id };
            await _roleService.DeleteRoleAsync(deletingRole);
            return NoContent();
        }
    }
}
