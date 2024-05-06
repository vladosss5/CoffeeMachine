namespace CoffeeMachine.API.DTOs.Machine;

/// <summary>
/// Передаваемый объект "кофемашина".
/// </summary>
public class MachineDto
{
    /// <summary>
    /// Идентификатор.
    /// </summary>
    public long Id { get; set; }
    
    /// <summary>
    /// Серийный номер.
    /// </summary>
    public string SerialNumber { get; set; }
    
    /// <summary>
    /// Описание.
    /// </summary>
    public string Description { get; set; }
    
    /// <summary>
    /// Баланс.
    /// </summary>
    public int Balance { get; set; }
}