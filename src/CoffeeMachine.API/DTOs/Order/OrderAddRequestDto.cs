using CoffeeMachine.API.DTOs.Coffee;
using CoffeeMachine.API.DTOs.Machine;
using CoffeeMachine.API.DTOs.Transaction;

namespace CoffeeMachine.API.DTOs.Order;

/// <summary>
/// Передаваемый объект "заказ".
/// Используется для запроса на создание.
/// </summary>
public class OrderAddRequestDto
{
    /// <summary>
    /// Ссылка на передаваемый объект "машина".
    /// </summary>
    public MachineForOrderDto Machine { get; set; }
    
    /// <summary>
    /// Ссылка на передаваемый объект "кофе".
    /// </summary>
    public CoffeeForOrderRequestDto Coffee { get; set; }
    
    /// <summary>
    /// Коллекция ссылок на передаваемые объекты "транзакции".
    /// </summary>
    public List<TransactionForOrderDto> Transactions { get; set; } = new List<TransactionForOrderDto>();
}