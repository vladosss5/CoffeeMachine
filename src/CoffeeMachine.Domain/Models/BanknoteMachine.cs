namespace CoffeeMachine.Domain.Models;

public class BanknoteMachine : BaseEntity
{
    public Machine Machine { get; set; }
    public Banknote Banknote { get; set; }
    public int CountBanknote { get; set; }
}