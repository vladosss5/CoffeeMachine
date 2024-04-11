namespace CoffeeMachine.Domain.Models;

public class Transaction
{
    public long Id { get; set; }
    public bool Type { get; set; } // true - оплата, false - сдача
    public int CountBanknotes { get; set; } // кол-во одинаковых банкнот
    
    public long IdBanknote { get; set; }
    public Banknote Banknote { get; set; }
    
    public long IdPurchase { get; set; }
    public Purchase Purchase { get; set; }
}