namespace CoffeeMachine.Domain.Models;

public class Transaction
{
    public int Id { get; set; }
    public bool Type { get; set; } // true - оплата, false - сдача
    public int CountBanknotes { get; set; } // кол-во одинаковых банкнот
    
    public int IdBanknote { get; set; }
    public Banknote Banknote { get; set; }
    
    public int IdPurchase { get; set; }
    public Purchase Purchase { get; set; }

}