namespace CoffeeMachine.Domain.Models;

public class Machine: BaseEntity
{
    public string? SerialNumber { get; set; }
    public string? Description { get; set; }
    public int? Balance { get; set; }

    public IEnumerable<Order> Purchases { get; set; } = new List<Order>();
    public IEnumerable<BanknoteMachine> BanknotesMachines { get; set; } = new List<BanknoteMachine>();
    public IEnumerable<CoffeeInMachine> CoffeesInMachines { get; set; } = new List<CoffeeInMachine>();
}