using CoffeeMachine.Domain.Models;

namespace CoffeeMachine.Infrastructure.Interfaces.IServices;

public interface IBanknoteService : IBaseService<Banknote>
{
    public Task<Banknote> GetByParAsync(int par);
}