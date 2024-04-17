namespace CoffeeMachine.API.DTOs;

public class OrderDto
{
    public MachineDto Machine { get; set; }
    public CoffeeDto Coffee { get; set; }
    
    public IEnumerable<TransactionDto> Transactions { get; set; } = new List<TransactionDto>();
}