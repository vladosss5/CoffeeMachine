namespace CoffeeMachine.Core.Models;

public class Order : BaseModel
{
    public DateTime DateTimeCreate { get; set; }
    public string Status { get; set; }
    
    public Coffee Coffee { get; set; }
    public Machine Machine { get; set; }
    
    public List<Transaction> Transactions { get; set; }
}