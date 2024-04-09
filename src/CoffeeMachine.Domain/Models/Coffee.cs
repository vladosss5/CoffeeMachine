namespace CoffeeMachine.Domain.Models;

public class Coffee
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public int Price { get; set; }
}