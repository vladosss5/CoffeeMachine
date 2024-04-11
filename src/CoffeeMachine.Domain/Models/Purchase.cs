namespace CoffeeMachine.Domain.Models;

public class Purchase
{
    public long Id { get; set; }
    
    public string Status { get; set; }
    public DateTime Date { get; set; }
    
    public long IdCoffee { get; set; }
    public Coffee Coffee {get; set;}
    
    public long IdMachine { get; set; }
    public Machine Machine { get; set; }
    

    public List<Transaction> Transactions { get; set; } = new List<Transaction>();
}