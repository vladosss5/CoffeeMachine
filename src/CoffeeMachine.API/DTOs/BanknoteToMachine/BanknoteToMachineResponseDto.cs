using CoffeeMachine.API.DTOs.Banknote;

namespace CoffeeMachine.API.DTOs.BanknoteToMachine;

public class BanknoteToMachineResponseDto
{
    public BanknoteDto Banknote { get; set; }
    public int CountBanknote { get; set; }
}