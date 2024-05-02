namespace CoffeeMachine.Core.Models;

/// <summary>
/// Банкнота.
/// </summary>
public class Banknote : BaseModel
{
    /// <summary>
    /// Номинал.
    /// </summary>
    public int Nominal { get; set; }
    
    /// <summary>
    /// Список транзакций.
    /// </summary>
    public IEnumerable<Transaction> Transactions { get; set; } = new List<Transaction>();
    /// <summary>
    /// Список банкнот в кофемашине.
    /// </summary>
    public IEnumerable<BanknoteToMachine> BanknotesToMachines { get; set; } = new List<BanknoteToMachine>();
}