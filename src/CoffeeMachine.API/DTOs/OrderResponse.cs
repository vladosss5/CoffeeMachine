namespace CoffeeMachine.API.DTOs;

public class OrderResponse
{
    public MachineDto Machine { get; set; }
    public CoffeeDto Coffee { get; set; }
    public string Status { get; set; }
    
    public IEnumerable<TransactionDto> Transactions { get; set; } = new List<TransactionDto>();
}