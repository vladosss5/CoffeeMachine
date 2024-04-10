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


    public Task<Banknote> GetById(int id)
    {
        throw new NotImplementedException();
    }

    public Task<List<Banknote>> GetAll()
    {
        throw new NotImplementedException();
    }

    public Task<Banknote> Add(Banknote entity)
    {
        throw new NotImplementedException();
    }

    public Task<Banknote> Update(Banknote entity)
    {
        throw new NotImplementedException();
    }

    public Task<bool> Delete(Banknote entity)
    {
        throw new NotImplementedException();
    }

    public Task<Banknote> GetByPar(int par)
    {
        throw new NotImplementedException();
    }
}