namespace CoffeeMachine.Core.Models;

/// <summary>
/// Кофе.
/// </summary>
public class Coffee : BaseModel
{
    /// <summary>
    /// Название.
    /// </summary>
    public string Name { get; set; }
    
    /// <summary>
    /// Цена.
    /// </summary>
    public int Price { get; set; }
    
    /// <summary>
    /// Список заказов.
    /// </summary>
    public IEnumerable<Order> Orders { get; set; } = new List<Order>();
    
    /// <summary>
    /// Список кофе в кофемашине.
    /// </summary>
    public IEnumerable<CoffeeToMachine> CoffeesToMachines { get; set; } = new List<CoffeeToMachine>();
}