using CoffeeMachine.API.DTOs.Banknote;
using CoffeeMachine.API.DTOs.Machine;

namespace CoffeeMachine.API.DTOs.BanknoteToMachine;

public class AddBanknoteToMachineReq
{
    public MachineReq Machine { get; set; }
    public IEnumerable<BanknoteDto> Banknotes { get; set; } = new List<BanknoteDto>();
}