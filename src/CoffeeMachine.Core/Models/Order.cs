namespace CoffeeMachine.Core.Models;

/// <summary>
/// Заказ.
/// </summary>
public class Order : BaseModel
{
    /// <summary>
    /// Дата создания.
    /// </summary>
    public DateTime DateTimeCreate { get; set; }
    
    /// <summary>
    /// Статус.
    /// </summary>
    public string Status { get; set; }
    
    /// <summary>
    /// Кофе.
    /// </summary>
    public Coffee Coffee { get; set; }
    
    /// <summary>
    /// Кофемашина.
    /// </summary>
    public Machine Machine { get; set; }

    /// <summary>
    /// Список транзакций.
    /// </summary>
    public IEnumerable<Transaction> Transactions { get; set; } = new List<Transaction>();
}