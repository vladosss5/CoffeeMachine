using CoffeeMachine.Domain.Models;
using CoffeeMachine.Infrastructure.Interfaces.IRepositories;
using CoffeeMachine.Infrastructure.Interfaces.IServices;

namespace CoffeeMachine.Persistence.Services;

public class BanknoteService : IBaseService<Banknote>, IBanknoteService
{
    private readonly IBanknoteRepository _banknoteRepository;

    public BanknoteService(IBanknoteRepository banknoteRepository)
    {
        _banknoteRepository = banknoteRepository;
    }


    public Task<Banknote> GetByIdAsync(long id)
    {
        throw new NotImplementedException();
    }

    public Task<List<Banknote>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<Banknote> AddAsync(Banknote entity)
    {
        throw new NotImplementedException();
    }

    public Task<Banknote> UpdateAsync(Banknote entity)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteAsync(Banknote entity)
    {
        throw new NotImplementedException();
    }

    public Task<Banknote> GetByParAsync(int par)
    {
        throw new NotImplementedException();
    }
}