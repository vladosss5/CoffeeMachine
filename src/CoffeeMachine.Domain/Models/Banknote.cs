namespace CoffeeMachine.Domain.Models;

public class Banknote : BaseEntity
{
    public int Nominal  { get; set; }
    public IEnumerable<Transaction> Transactions { get; set; } = new List<Transaction>();
    public IEnumerable<BanknoteMachine> BanknotesMachines { get; set; } = new List<BanknoteMachine>();
}