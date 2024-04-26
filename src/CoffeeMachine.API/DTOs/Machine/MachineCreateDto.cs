namespace CoffeeMachine.API.DTOs.Machine;

/// <summary>
/// Передаваемый объект "кофемашина".
/// Используется для создания.
/// </summary>
public class MachineCreateDto
{
    /// <summary>
    /// Серийный номер.
    /// </summary>
    public string SerialNumber { get; set; }
    
    /// <summary>
    /// Описание.
    /// </summary>
    public string Description { get; set; }
}