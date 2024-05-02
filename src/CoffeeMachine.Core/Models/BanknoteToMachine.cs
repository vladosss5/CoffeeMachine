namespace CoffeeMachine.Core.Models;

/// <summary>
/// Банкноты в кофемашине.
/// </summary>
public class BanknoteToMachine : BaseModel
{
    /// <summary>
    /// Банкнота.
    /// </summary>
    public Banknote Banknote { get; set; }
    
    /// <summary>
    /// Кофемашина.
    /// </summary>
    public Machine Machine { get; set; }
    
    /// <summary>
    /// Количество одинаковых банкнот в кофемашине.
    /// </summary>
    public int CountBanknote { get; set; }
}