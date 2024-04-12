namespace CoffeeMachine.Domain.Models;

public class Transaction : BaseEntity
{
    public bool Type { get; set; } // true - оплата, false - сдача
    public int CountBanknotes { get; set; } // кол-во одинаковых банкнот
    public Banknote Banknote { get; set; }
    public Purchase Purchase { get; set; }
}