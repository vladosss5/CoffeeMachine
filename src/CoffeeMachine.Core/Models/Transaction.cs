namespace CoffeeMachine.Core.Models;

public class Transaction : BaseModel
{
    public bool IsPayment { get; set; }
    public Order Order { get; set; }
    public Banknote Banknote { get; set; }
}