namespace CoffeeMachine.Core.Models;

/// <summary>
/// Роль в системе.
/// </summary>
public class Role : BaseModel
{
    /// <summary>
    /// Название.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Пользователи.
    /// </summary>
    public IEnumerable<User> Users { get; set; } = new List<User>();
}