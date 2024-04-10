using CoffeeMachine.Domain.Models;

namespace CoffeeMachine.Infrastructure.Interfaces.IRepositories;

public interface IPurechaseRepository : IBaseRepository<Purchase>
{
    public Task<List<Purchase>> GetByCoffee(Coffee coffee);
    public Task<List<Purchase>> GetByMachine(Machine machine);
    public Task<List<Purchase>> GetByStatus(string status);
    public Task<List<Purchase>> GetByDate(DateTime dateStart, DateTime dateEnd);
}