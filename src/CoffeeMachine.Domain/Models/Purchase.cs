namespace CoffeeMachine.Domain.Models;

public class Purchase : BaseEntity
{
    public string Status { get; set; }
    public DateTime Date { get; set; }
    public Coffee Coffee {get; set;}
    public Machine Machine { get; set; }

    public IEnumerable<Transaction> Transactions { get; set; } = new List<Transaction>();
}