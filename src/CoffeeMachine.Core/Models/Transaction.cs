namespace CoffeeMachine.Core.Models;

/// <summary>
/// Транзакция.
/// </summary>
public class Transaction : BaseModel
{
    /// <summary>
    /// Тип транзакции. true - оплата, false - сдача.
    /// </summary>
    public bool IsPayment { get; set; }
    
    /// <summary>
    /// Заказ.
    /// </summary>
    public Order Order { get; set; }
    
    /// <summary>
    /// Банкнота.
    /// </summary>
    public Banknote Banknote { get; set; }
}