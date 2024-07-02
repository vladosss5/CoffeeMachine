namespace CoffeeMachine.API.DTOs.CoffeesInMachine;

using CoffeeMachine.API.DTOs.Coffee;
using CoffeeMachine.API.DTOs.Machine;

/// <summary>
/// Передаваемый объект "кофе в машине".
/// </summary>
public class CoffeesInMachineDto
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