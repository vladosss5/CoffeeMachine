using CoffeeMachine.API.DTOs.Coffee;
using CoffeeMachine.API.DTOs.Machine;
using CoffeeMachine.API.DTOs.Transaction;

namespace CoffeeMachine.API.DTOs.Order;

public class OrderAddReq
{
    public MachineForOrderDto Machine { get; set; }
    public CoffeeForOrderReq Coffee { get; set; }
    public IEnumerable<TransactionForOrderDto> Transactions { get; set; } = new List<TransactionForOrderDto>();
}