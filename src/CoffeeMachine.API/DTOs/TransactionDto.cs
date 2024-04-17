namespace CoffeeMachine.API.DTOs;

public class TransactionDto
{
    public bool IsPayment { get; set; }
    public BanknoteDto Banknote { get; set; }
}