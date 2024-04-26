using Microsoft.EntityFrameworkCore.Scaffolding.Metadata;

namespace CoffeeMachine.API.DTOs.Machine;

/// <summary>
/// Передаваемы объект "машина для заказа".
/// Используется для создания заказа.
/// </summary>
public class MachineForOrderDto
{
    /// <summary>
    /// Серийный номер.
    /// </summary>
    public string SerialNumber { get; set; }
}