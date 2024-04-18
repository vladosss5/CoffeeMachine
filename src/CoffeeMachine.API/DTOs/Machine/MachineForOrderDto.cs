using Microsoft.EntityFrameworkCore.Scaffolding.Metadata;

namespace CoffeeMachine.API.DTOs.Machine;

public class MachineForOrderDto
{
    public string SerialNumber { get; set; }
}