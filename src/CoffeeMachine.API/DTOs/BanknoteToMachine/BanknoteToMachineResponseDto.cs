using CoffeeMachine.API.DTOs.Banknote;

namespace CoffeeMachine.API.DTOs.BanknoteToMachine;

/// <summary>
/// Передаваемый объект "банкноты в машине".
/// </summary>
public class BanknoteToMachineResponseDto
{
    /// <summary>
    /// Банкнота.
    /// </summary>
    public BanknoteDto Banknote { get; set; }
    
    /// <summary>
    /// Кол-во одинаковых банкнот.
    /// </summary>
    public int CountBanknote { get; set; }
}