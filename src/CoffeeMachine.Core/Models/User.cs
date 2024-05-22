using System.ComponentModel.DataAnnotations.Schema;

namespace CoffeeMachine.Core.Models;

/// <summary>
/// Пользователь.
/// </summary>
public class User : BaseModel
{
    /// <summary>
    /// Логин.
    /// </summary>
    public string Login { get; set; }
    
    /// <summary>
    /// Пароль.
    /// </summary>
    public string Password { get; set; }
    
    /// <summary>
    /// JWT.
    /// </summary>
    [NotMapped] string Token { get; set; }
}