namespace CoffeeMachine.API.DTOs.Coffee;

/// <summary>
/// Передаваемый объект "кофе".
/// Используется для запроса.
/// </summary>
public class CoffeeReqestDto
{
    public string Name { get; set; }
    public int Price { get; set; }
}