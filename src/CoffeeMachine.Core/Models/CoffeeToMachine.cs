namespace CoffeeMachine.Core.Models;

public class CoffeeToMachine : BaseModel
{
    public Coffee Coffee { get; set; }
    public Machine Machine { get; set; }
}