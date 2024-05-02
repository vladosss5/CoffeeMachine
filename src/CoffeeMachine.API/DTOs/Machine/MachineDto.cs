namespace CoffeeMachine.API.DTOs.Machine;

/// <summary>
/// Передаваемый объект "кофемашина".
/// Используется для ответа.
/// Хранит все поля сущности "Machine", кроме ссылок.
/// </summary>
public class MachineDto
{
    public long Id { get; set; }
    public string SerialNumber { get; set; }
    public string Description { get; set; }
    public int Balance { get; set; }
}