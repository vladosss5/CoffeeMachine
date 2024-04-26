using CoffeeMachine.API.DTOs.Coffee;
using CoffeeMachine.API.DTOs.Machine;
using CoffeeMachine.API.DTOs.Transaction;

namespace CoffeeMachine.API.DTOs.Order;

/// <summary>
/// Передаваемый объект "заказ".
/// Используется для ответа.
/// </summary>
public class OrderAddResponseDto
{
    /// <summary>
    /// Ссылка на передаваемый объект "машина".
    /// </summary>
    public MachineForOrderDto Machine { get; set; }
    
    /// <summary>
    /// Ссылка на передаваемый объект "кофе".
    /// </summary>
    public CoffeeForOrderResponseDto Coffee { get; set; }
    
    /// <summary>
    /// Статус заказа
    /// </summary>
    public string Status { get; set; }
    
    /// <summary>
    /// Коллекция ссылок на передаваемые объекты "транзакции".
    /// </summary>
    public List<TransactionForOrderDto> Transactions { get; set; } = new List<TransactionForOrderDto>();
}