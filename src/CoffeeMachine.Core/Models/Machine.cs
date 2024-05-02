namespace CoffeeMachine.Core.Models;

/// <summary>
/// Кофемашина.
/// </summary>
public class Machine : BaseModel
{
    /// <summary>
    /// Серийный номер.
    /// </summary>
    public string SerialNumber { get; set; }
    
    /// <summary>
    /// Описание.
    /// </summary>
    public string Description { get; set; }
    
    /// <summary>
    /// Баланс.
    /// </summary>
    public int Balance { get; set; } = 0;

    /// <summary>
    /// Список заказов.
    /// </summary>
    public IEnumerable<Order> Orders { get; set; } = new List<Order>();
    
    /// <summary>
    /// Список банкнот в кофемашине.
    /// </summary>
    public IEnumerable<BanknoteToMachine> BanknotesToMachines { get; set; } = new List<BanknoteToMachine>();
    
    /// <summary>
    /// Список кофе в кофемашине.
    /// </summary>
    public IEnumerable<CoffeeToMachine> CoffeesToMachines { get; set; } = new List<CoffeeToMachine>();
}