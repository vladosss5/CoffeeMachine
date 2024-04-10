namespace CoffeeMachine.Domain.Models;

public class Banknote
{
    public long Id { get; set; }
    public int Par { get; set; }

    public List<Transaction> Transactions { get; set; } = new List<Transaction>();
    public List<BanknotesMachine> BanknotesMachines { get; set; } = new List<BanknotesMachine>();
}