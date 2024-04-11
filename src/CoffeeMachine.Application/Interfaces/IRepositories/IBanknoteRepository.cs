using CoffeeMachine.Domain.Models;

namespace CoffeeMachine.Infrastructure.Interfaces.IRepositories;

public interface IBanknoteRepository : IBaseRepository<Banknote>
{
    public Task<Banknote> GetByParAsync(int par);
}