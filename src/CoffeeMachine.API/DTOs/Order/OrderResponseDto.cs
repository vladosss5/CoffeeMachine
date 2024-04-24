using CoffeeMachine.API.DTOs.Coffee;
using CoffeeMachine.API.DTOs.Machine;

namespace CoffeeMachine.API.DTOs.Order;

public class OrderResponseDto
{
    public long Id { get; set; }
    public string Status { get; set; }
    public DateTime DateTimeCreate { get; set; }
    
    public CoffeeFullResponseDto Coffee { get; set; }
    public MachineFullResponseDto Machine { get; set; }
}