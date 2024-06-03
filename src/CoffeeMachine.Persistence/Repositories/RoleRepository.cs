using CoffeeMachine.Application.Interfaces.IRepositories;
using CoffeeMachine.Core.Models;
using CoffeeMachine.Persistence.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace CoffeeMachine.Persistence.Repositories;

/// <summary>
/// <inheritdoc cref="IRoleRepository"/>
/// </summary>
public class RoleRepository : GenericRepository<Role>, IRoleRepository
{
    /// <summary>
    /// <inheritdoc cref="DataContext"/>
    /// </summary>
    private readonly DataContext _dataContext;

    /// <summary>
    /// Конструктор класса.
    /// </summary>
    /// <param name="dataContext">Контекст работы с базой данных.</param>
    public RoleRepository(DataContext dataContext) : base(dataContext)
    {
        _dataContext = dataContext;
    }
    
    /// <summary>
    /// <inheritdoc cref="IRoleRepository.GetByNameAsync"/>
    /// </summary>
    public async Task<Role> GetByNameAsync(string name)
    {
        return await _dataContext.Roles.FirstOrDefaultAsync(x => x.Name == name);
    }
}