using CoffeeMachine.API.DTOs.Coffee;
using CoffeeMachine.API.DTOs.Machine;

namespace CoffeeMachine.API.DTOs.CoffeesInMachine;

public class CoffeesInMachineResponseDto
{
    public MachineFullResponseDto Machine { get; set; }
    public IEnumerable<CoffeeFullResponseDto> Coffees { get; set; } = new List<CoffeeFullResponseDto>();
}