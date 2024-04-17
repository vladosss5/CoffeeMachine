namespace CoffeeMachine.Core.Models;

public class Coffee : BaseModel
{
    public string Name { get; set; }
    public int Price { get; set; }
    
    public IEnumerable<Order> Orders { get; set; } = new List<Order>();
    public IEnumerable<CoffeeToMachine> CoffeesToMachines { get; set; } = new List<CoffeeToMachine>();
}