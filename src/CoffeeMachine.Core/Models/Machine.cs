namespace CoffeeMachine.Core.Models;

public class Machine : BaseModel
{
    public string SerialNumber { get; set; }
    public string Description { get; set; }
    public int Balance { get; set; } = 0;

    public IEnumerable<Order> Orders { get; set; } = new List<Order>();
    public IEnumerable<BanknoteToMachine> BanknotesToMachines { get; set; } = new List<BanknoteToMachine>();
    public IEnumerable<CoffeeToMachine> CoffeesToMachines { get; set; } = new List<CoffeeToMachine>();
}