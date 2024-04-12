namespace CoffeeMachine.Domain.Models;

public class Machine: BaseEntity
{
    public string? SerialNumber { get; set; }
    public string? Description { get; set; }
    public int? Balance { get; set; }

    public IEnumerable<Purchase> Purchases { get; set; } = new List<Purchase>();
    public IEnumerable<BanknoteMachine> BanknotesMachines { get; set; } = new List<BanknoteMachine>();
}