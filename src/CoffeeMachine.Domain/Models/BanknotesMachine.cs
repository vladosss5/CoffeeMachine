namespace CoffeeMachine.Domain.Models;

public class BanknotesMachine
{
    public int Id { get; set; }
    
    public int IdMachine { get; set; }
    public Machine Machine { get; set; }
    
    public int IdBanknote { get; set; }
    public Banknote Banknote { get; set; }
    public int CountBanknotes { get; set; }
}