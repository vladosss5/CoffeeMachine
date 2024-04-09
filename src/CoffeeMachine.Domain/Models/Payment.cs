namespace CoffeeMachine.Domain.Models;

public class Payment
{
    public Guid Id { get; set; }
    public List<Money> Rect { get; set; } = new List<Money>();
    public List<Money> Change { get; set; } = new List<Money>();
    public string Status { get; set; }
}