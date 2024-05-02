using CoffeeMachine.Core.Models;

namespace CoffeeMachine.Application.Interfaces.IServices;

public interface IOrderService
{
    /// <summary>
    /// Создать заказ.
    /// </summary>
    /// <param name="order">Заказ.</param>
    /// <returns>Покупка.</returns>
    public Task<Order> CreateOrderAsync(Order order);
}