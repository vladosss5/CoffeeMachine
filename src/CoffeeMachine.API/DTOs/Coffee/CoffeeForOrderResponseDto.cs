namespace CoffeeMachine.API.DTOs.Coffee;

/// <summary>
/// Передаваемый объект "кофе для объекта ответ заказа".
/// </summary>
public class CoffeeForOrderResponseDto
{
    /// <summary>
    /// Название кофе.
    /// </summary>
    public string Name { get; set; }
    
    /// <summary>
    /// Цена кофе.
    /// </summary>
    public int Price { get; set; }
}