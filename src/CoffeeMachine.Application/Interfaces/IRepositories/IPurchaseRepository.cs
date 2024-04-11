using CoffeeMachine.Domain.Models;

namespace CoffeeMachine.Infrastructure.Interfaces.IRepositories;

public interface IPurchaseRepository : IBaseRepository<Purchase>
{
    public Task<List<Purchase>> GetByCoffeeAsync(Coffee coffee);
    public Task<List<Purchase>> GetByMachineAsync(Machine machine);
    public Task<List<Purchase>> GetByStatusAsync(string status);
    public Task<List<Purchase>> GetByDateAsync(DateTime dateStart, DateTime dateEnd);
}