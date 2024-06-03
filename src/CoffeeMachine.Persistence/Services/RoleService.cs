using CoffeeMachine.Application.Exceptions;
using CoffeeMachine.Application.Interfaces;
using CoffeeMachine.Application.Interfaces.IRepositories;
using CoffeeMachine.Application.Interfaces.IServices;
using CoffeeMachine.Core.Models;

namespace CoffeeMachine.Persistence.Services;

/// <summary>
/// <inheritdoc cref="IRoleService"/>
/// </summary>
public class RoleService : IRoleService
{
    /// <summary>
    /// <inheritdoc cref="IUnitOfWork"/>
    /// </summary>
    private readonly IUnitOfWork _unitOfWork;

    /// <summary>
    /// Конструктор класса.
    /// </summary>
    /// <param name="unitOfWork">Unit of work.</param>
    public RoleService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    
    /// <summary>
    /// <inheritdoc cref="IRoleService.GetAllRolesAsync"/>
    /// </summary>
    public async Task<IEnumerable<Role>> GetAllRolesAsync()
    {
        return await _unitOfWork.Role.GetAllAsync();
    }

    /// <summary>
    /// <inheritdoc cref="IRoleService.GetRoleByIdAsync"/>
    /// </summary>
    public async Task<Role> GetRoleByIdAsync(long id)
    {
        return await _unitOfWork.Role.GetByIdAsync(id);
    }

    /// <summary>
    /// <inheritdoc cref="IRoleService.GetRoleByNameAsync"/>
    /// </summary>
    public async Task<Role> GetRoleByNameAsync(string name)
    {
        return await _unitOfWork.Role.GetByNameAsync(name);
    }

    /// <summary>
    /// <inheritdoc cref="IRoleService.CreateRoleAsync"/>
    /// </summary>
    public async Task<Role> CreateRoleAsync(Role role)
    {
        var identity = await _unitOfWork.Role.GetByNameAsync(role.Name);
        if (identity != null)
            throw new AlreadyExistsException(nameof(Role), role.Name);
        
        return await _unitOfWork.Role.AddAsync(role);
    }

    /// <summary>
    /// <inheritdoc cref="IRoleService.UpdateRoleAsync"/>
    /// </summary>
    public async Task<Role> UpdateRoleAsync(Role role)
    {
        var oldRole = await _unitOfWork.Role.GetByIdAsync(role.Id);
        if (oldRole == null)
            throw new NotFoundException(nameof(Role), role.Name);
        
        oldRole.Name = role.Name;
        
        return await _unitOfWork.Role.UpdateAsync(oldRole);
    }

    /// <summary>
    /// <inheritdoc cref="IRoleService.DeleteRoleAsync"/>
    /// </summary>
    public async Task DeleteRoleAsync(Role role)
    {
        var deletingRole = await _unitOfWork.Role.GetByIdAsync(role.Id);
        if (deletingRole == null)
            throw new NotFoundException(nameof(Role), role.Name);
        
        await _unitOfWork.Role.DeleteAsync(deletingRole);
    }
}