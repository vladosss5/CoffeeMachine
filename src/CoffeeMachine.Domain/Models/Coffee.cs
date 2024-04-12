namespace CoffeeMachine.Domain.Models;

public class Coffee : BaseEntity
{
    public string Name { get; set; }
    public int Price { get; set; }

    public IEnumerable<Purchase> Purchases { get; set; } = new List<Purchase>();
}