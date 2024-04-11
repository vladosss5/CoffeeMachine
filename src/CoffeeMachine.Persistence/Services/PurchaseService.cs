using CoffeeMachine.Domain.Models;
using CoffeeMachine.Infrastructure.Interfaces.IRepositories;
using CoffeeMachine.Infrastructure.Interfaces.IServices;

namespace CoffeeMachine.Persistence.Services;

public class PurchaseService : IBaseService<Purchase>, IPurchaseService
{
    private readonly IPurchaseRepository _purchaseRepository;

    public PurchaseService(IPurchaseRepository purchaseRepository)
    {
        _purchaseRepository = purchaseRepository;
    }
    
    public Task<Purchase> GetByIdAsync(long id)
    {
        throw new NotImplementedException();
    }

    public Task<List<Purchase>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<Purchase> AddAsync(Purchase entity)
    {
        return _purchaseRepository.AddAsync(entity);
    }

    public Task<Purchase> UpdateAsync(Purchase entity)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteAsync(Purchase entity)
    {
        throw new NotImplementedException();
    }

    public Task<List<Purchase>> GetByCoffeeAsync(Coffee coffee)
    {
        throw new NotImplementedException();
    }

    public Task<List<Purchase>> GetByMachineAsync(Machine machine)
    {
        throw new NotImplementedException();
    }

    public Task<List<Purchase>> GetByStatusAsync(string status)
    {
        throw new NotImplementedException();
    }

    public Task<List<Purchase>> GetByDateAsync(DateTime dateStart, DateTime dateEnd)
    {
        throw new NotImplementedException();
    }
}