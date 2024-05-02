using CoffeeMachine.Core.Models;

namespace CoffeeMachine.Application.Interfaces.IServices;

/// <summary>
/// Сервис заказов.
/// </summary>
public interface IOrderService
{
    /// <summary>
    /// Создать заказ.
    /// </summary>
    /// <param name="order">Заказ.</param>
    /// <returns>Покупка.</returns>
    public Task<Order> CreateOrderAsync(Order order);
}