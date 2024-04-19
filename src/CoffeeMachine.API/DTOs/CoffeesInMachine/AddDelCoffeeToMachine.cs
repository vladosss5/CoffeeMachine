using CoffeeMachine.API.DTOs.Coffee;
using CoffeeMachine.API.DTOs.Machine;

namespace CoffeeMachine.API.DTOs.CoffeesInMachine;

public class AddDelCoffeeToMachine
{
    public MachineReq Machine { get; set; }
    public CoffeeReq Coffee { get; set; }
}