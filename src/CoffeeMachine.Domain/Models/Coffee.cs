namespace CoffeeMachine.Domain.Models;

public class Coffee
{
    public long Id { get; set; }
    public string Name { get; set; }
    public int Price { get; set; }
    public int Size { get; set; }

    public List<Purchase> Purchases { get; set; } = new List<Purchase>();
}