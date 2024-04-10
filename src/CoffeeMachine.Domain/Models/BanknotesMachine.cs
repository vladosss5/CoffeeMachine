namespace CoffeeMachine.Domain.Models;

public class BanknotesMachine
{
    public long Id { get; set; }
    
    public long IdMachine { get; set; }
    public Machine Machine { get; set; }
    
    public long IdBanknote { get; set; }
    public Banknote Banknote { get; set; }
    public int CountBanknotes { get; set; }
}