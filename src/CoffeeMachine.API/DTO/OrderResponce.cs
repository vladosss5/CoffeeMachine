namespace CoffeeMachine.API.DTO;

public class OrderResponce
{
    public string Status { get; set; }
    public DateTime Date { get; set; }
    public CoffeeDto Coffee {get; set;}
    public MachineDto Machine { get; set; }
    public List<BanknoteDto> Banknotes { get; set; } = new List<BanknoteDto>();
}