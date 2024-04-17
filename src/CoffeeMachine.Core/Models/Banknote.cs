namespace CoffeeMachine.Core.Models;

public class Banknote : BaseModel
{
    public int Nominal { get; set; }
    
    public IEnumerable<Transaction> Transactions { get; set; } = new List<Transaction>();
    public IEnumerable<BanknoteToMachine> BanknotesToMachines { get; set; } = new List<BanknoteToMachine>();
}