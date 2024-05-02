namespace CoffeeMachine.Core.Models;

/// <summary>
/// Кофе в кофемашине.
/// </summary>
public class CoffeeToMachine : BaseModel
{
    /// <summary>
    /// Кофе.
    /// </summary>
    public Coffee Coffee { get; set; }
    
    /// <summary>
    /// Кофемашина.
    /// </summary>
    public Machine Machine { get; set; }
}