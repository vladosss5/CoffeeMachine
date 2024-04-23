namespace CoffeeMachine.API.DTOs.Coffee;

public class CoffeeUpdateRequestDto
{
    public long Id { get; set; }
    public string Name { get; set; }
    public int Price { get; set; }
}