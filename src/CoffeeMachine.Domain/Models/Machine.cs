namespace CoffeeMachine.Domain.Models;

public class Machine
{
    public int Id { get; set; }
    public string? SerialNumber { get; set; }
    public string? Description { get; set; }

    public List<Purchase> Purchases { get; set; } = new List<Purchase>();
    public List<BanknotesMachine> BanknotesMachines { get; set; } = new List<BanknotesMachine>();
}