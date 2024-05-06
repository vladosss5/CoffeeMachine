using CoffeeMachine.API.DTOs.Coffee;
using CoffeeMachine.API.DTOs.Machine;

namespace CoffeeMachine.API.DTOs.Order;

/// <summary>
/// Передаваемый объект "заказ".
/// Используется для ответа.
/// </summary>
public class OrderResponseDto
{
    /// <summary>
    /// Идентификатор.
    /// </summary>
    public long Id { get; set; }
    
    /// <summary>
    /// Статус.
    /// </summary>
    public string Status { get; set; }
    
    /// <summary>
    /// Дата создания.
    /// </summary>
    public DateTime DateTimeCreate { get; set; }
    
    /// <summary>
    /// Кофе.
    /// </summary>
    public CoffeeDto Coffee { get; set; }
    
    /// <summary>
    /// Машина.
    /// </summary>
    public MachineDto Machine { get; set; }
}