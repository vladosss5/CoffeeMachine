using CoffeeMachine.API.DTOs.Coffee;
using CoffeeMachine.API.DTOs.Machine;

namespace CoffeeMachine.API.DTOs.CoffeesInMachine;

/// <summary>
/// Передаваемы объект "кофе в машине".
/// Используется для ответа.
/// </summary>
public class CoffeesInMachineResponseDto
{
    /// <summary>
    /// Ссылка на передаваемы объект "машина".
    /// </summary>
    public MachineDto Machine { get; set; }
    
    /// <summary>
    /// Коллекция передаваемых объектов "кофе".
    /// </summary>
    public List<CoffeeDto> Coffees { get; set; } = new List<CoffeeDto>();
}