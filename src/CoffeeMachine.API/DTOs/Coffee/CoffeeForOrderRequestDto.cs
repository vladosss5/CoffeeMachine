namespace CoffeeMachine.API.DTOs.Coffee;

/// <summary>
/// Передаваемый объект "кофе для объекта запрос заказа".
/// </summary>
public class CoffeeForOrderRequestDto
{
    /// <summary>
    /// Название кофе.
    /// </summary>
    public string Name { get; set; }
}