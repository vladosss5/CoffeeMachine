using CoffeeMachine.API.DTOs.Coffee;
using CoffeeMachine.API.DTOs.Machine;
using CoffeeMachine.API.DTOs.Transaction;

namespace CoffeeMachine.API.DTOs.Order;

public class OrderRespForAdmin
{
    public long Id { get; set; }
    public string Status { get; set; }
    public DateTime DateTimeCreate { get; set; }
    public MachineRespForAdmin Machine { get; set; }
    public CoffeeRespForAdminDto Coffee { get; set; }

    public IEnumerable<TransactionForOrderDto> Transactions { get; set; } = new List<TransactionForOrderDto>();
}