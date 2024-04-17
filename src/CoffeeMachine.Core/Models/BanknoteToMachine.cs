namespace CoffeeMachine.Core.Models;

public class BanknoteToMachine : BaseModel
{
    public Banknote Banknote { get; set; }
    public Machine Machine { get; set; }
    public int CountBanknote { get; set; }
}