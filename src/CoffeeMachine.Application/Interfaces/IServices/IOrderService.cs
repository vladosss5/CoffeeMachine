using CoffeeMachine.Domain.Models;

namespace CoffeeMachine.Infrastructure.Interfaces.IServices;

public interface IOrderService
{
    /// <summary>
    /// Покупка - хз пока
    /// </summary>
    /// <param name="order"></param>
    /// <returns>Покупку</returns>
    public Task<Order> CreateOrderAsync(Order order);
}