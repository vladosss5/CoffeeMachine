using CoffeeMachine.API.DTOs.Banknote;

namespace CoffeeMachine.API.DTOs.Transaction;

public class TransactionForOrderDto
{
    public bool IsPayment { get; set; }
    public BanknoteDto Banknote { get; set; }
}