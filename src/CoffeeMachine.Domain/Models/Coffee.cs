namespace CoffeeMachine.Domain.Models;

public class Coffee : BaseEntity
{
    public string Name { get; set; }
    public int Price { get; set; }

    public IEnumerable<Order> Purchases { get; set; } = new List<Order>();
    public IEnumerable<CoffeeInMachine> CoffeesInMachines { get; set; } = new List<CoffeeInMachine>();
}