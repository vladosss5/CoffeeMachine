using CoffeeMachine.Core.Models;

namespace CoffeeMachine.Application.Interfaces.IServices;

/// <summary>
/// Сервис ролей.
/// </summary>
public interface IRoleService
{
    /// <summary>
    /// Получить список всех ролей.
    /// </summary>
    /// <returns>Список ролей.</returns>
    public Task<IEnumerable<Role>> GetAllRolesAsync();
    
    /// <summary>
    /// Получить роль по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор.</param>
    /// <returns>Роль.</returns>
    public Task<Role> GetRoleByIdAsync(long id);
    
    /// <summary>
    /// Получить роль по наименованию.
    /// </summary>
    /// <param name="name">Наименование.</param>
    /// <returns>Роль.</returns>
    public Task<Role> GetRoleByNameAsync(string name);
    
    /// <summary>
    /// Создать роль.
    /// </summary>
    /// <param name="role">Роль.</param>
    /// <returns>Созданная роль.</returns>
    public Task<Role> CreateRoleAsync(Role role);
    
    /// <summary>
    /// Изменить роль.
    /// </summary>
    /// <param name="role">Роль.</param>
    /// <returns>Изменённая роль.</returns>
    public Task<Role> UpdateRoleAsync(Role role);
    
    /// <summary>
    /// Удалить роль.
    /// </summary>
    /// <param name="role">Роль.</param>
    public Task DeleteRoleAsync(Role role);
}