namespace CoffeeMachine.API.DTOs.Coffee;

/// <summary>
/// Передаваемый объект.
/// Имеет все поля сущности "кофе", кроме ссылок.
/// Используется для запроса.
/// </summary>
public class CoffeeFullRequestDto
{
    /// <summary>
    /// Идентификатор.
    /// </summary>
    public long Id { get; set; }
    
    /// <summary>
    /// Название
    /// </summary>
    public string Name { get; set; }
    
    /// <summary>
    /// Цена.
    /// </summary>
    public int Price { get; set; }
}