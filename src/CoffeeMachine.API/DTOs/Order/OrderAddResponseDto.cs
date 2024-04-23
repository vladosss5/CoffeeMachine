using CoffeeMachine.API.DTOs.Coffee;
using CoffeeMachine.API.DTOs.Machine;
using CoffeeMachine.API.DTOs.Transaction;

namespace CoffeeMachine.API.DTOs.Order;

public class OrderAddResponseDto
{
    public MachineForOrderDto Machine { get; set; }
    public CoffeeForOrderResponseDto Coffee { get; set; }
    public string Status { get; set; }
    public IEnumerable<TransactionForOrderDto> Transactions { get; set; } = new List<TransactionForOrderDto>();
}