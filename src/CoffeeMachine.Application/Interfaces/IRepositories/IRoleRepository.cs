using CoffeeMachine.Core.Models;

namespace CoffeeMachine.Application.Interfaces.IRepositories;

/// <summary>
/// Репозиторий ролей.
/// </summary>
public interface IRoleRepository : IBaseRepository<Role>
{
    /// <summary>
    /// Получить роль по наименованию.
    /// </summary>
    /// <param name="name">Наименование.</param>
    /// <returns>Роль</returns>
    public Task<Role> GetByNameAsync(string name);
}