using CoffeeMachine.Application.Exceptions;
using CoffeeMachine.Application.Interfaces.IRepositories;
using CoffeeMachine.Core.Models;
using CoffeeMachine.Persistence.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace CoffeeMachine.Persistence.Repositories;

/// <summary>
/// Репозиторий кофе.
/// </summary>
public class CoffeeRepository : GenericRepository<Coffee>, ICoffeeRepository
{
    /// <summary>
    /// <inheritdoc cref="DataContext"/>
    /// </summary>
    private readonly DataContext _dataContext;

    /// <summary>
    /// Конструктор класса.
    /// </summary>
    /// <param name="dataContext">Контекст для работы с базой данных.</param>
    public CoffeeRepository(DataContext dataContext) : base(dataContext)
    {
        _dataContext = dataContext;
    }

    /// <summary>
    /// Получить кофе по названию
    /// </summary>
    /// <param name="nameCoffe">Название кофе.</param>
    /// <returns>Кофе с указанным названием.</returns>
    public async Task<Coffee> GetByNameAsync(string nameCoffe)
    {
        return await _dataContext.Coffees.FirstOrDefaultAsync(x => x.Name == nameCoffe);
    }

    /// <summary>
    /// Получить кофе доступные кофемашине.
    /// </summary>
    /// <param name="machine">Кофемашина.</param>
    /// <returns>Список кофе.</returns>
    public async Task<IEnumerable<Coffee>> GetCoffeesFromMachineAsync(Machine machine)
    {
        return await _dataContext.CoffeesToMachines
            .Where(cm => cm.Machine == machine)
            .Select(cm => cm.Coffee)
            .ToListAsync();
    }
}