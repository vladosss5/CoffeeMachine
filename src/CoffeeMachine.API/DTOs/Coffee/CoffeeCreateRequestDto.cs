namespace CoffeeMachine.API.DTOs.Coffee;

/// <summary>
/// Кофе.
/// </summary>
public class CoffeeCreateRequestDto
{
    /// <summary>
    /// Название
    /// </summary>
    public string Name { get; set; }
    
    /// <summary>
    /// Цена.
    /// </summary>
    public int Price { get; set; }
}