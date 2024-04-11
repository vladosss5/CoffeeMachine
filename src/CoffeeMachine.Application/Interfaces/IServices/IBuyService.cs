using CoffeeMachine.Domain.Models;

namespace CoffeeMachine.Infrastructure.Interfaces.IServices;

public interface IBuyService
{
    /// <summary>
    /// Покупка - хз пока
    /// </summary>
    /// <param name="purchase"></param>
    /// <returns>Покупку</returns>
    public Task<Purchase> BuyAsync(Purchase purchase);
}