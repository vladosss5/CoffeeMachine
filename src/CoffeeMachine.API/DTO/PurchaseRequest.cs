namespace CoffeeMachine.API.DTO;

public class PurchaseRequest
{
    public List<BanknoteDto> Banknotes { get; set; } = new List<BanknoteDto>();
    public CoffeeDto Coffee {get; set;}
    public MachineDto Machine { get; set; }
}