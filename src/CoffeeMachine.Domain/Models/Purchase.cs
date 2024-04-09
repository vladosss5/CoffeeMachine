namespace CoffeeMachine.Domain.Models;

public class Purchase
{
    public Guid Id { get; set; }
    
    public string Status { get; set; }
    public DateTime Date { get; set; }
    
    public Guid IdCoffee { get; set; }
    public Coffee Coffee {get; set;}
    
    public Guid IdPayment { get; set; }
    public Payment Payment { get; set; }
}