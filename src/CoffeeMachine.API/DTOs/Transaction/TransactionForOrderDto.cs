using CoffeeMachine.API.DTOs.Banknote;

namespace CoffeeMachine.API.DTOs.Transaction;

/// <summary>
/// Передаваемый объект "транзакция заказа".
/// </summary>
public class TransactionForOrderDto
{
    /// <summary>
    /// Тип транзакции.
    /// true - оплата.
    /// false - сдача.
    /// </summary>
    public bool IsPayment { get; set; }
    
    /// <summary>
    /// Ссылка на передаваемый объект "банкнота".
    /// </summary>
    public BanknoteDto Banknote { get; set; }
}