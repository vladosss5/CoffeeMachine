namespace CoffeeMachine.Domain.Models;

public class CoffeeInMachine : BaseEntity
{
    public Machine Machine { get; set; }
    public Coffee Coffee { get; set; }
}