namespace CoffeeMachine.API.DTO;

public class OrderRequest
{
    public List<BanknoteDto> Banknotes { get; set; } = new List<BanknoteDto>();
    public CoffeeDto Coffee {get; set;}
    public MachineDto Machine { get; set; }
}