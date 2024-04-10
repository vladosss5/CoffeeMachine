using CoffeeMachine.Domain.Models;
using CoffeeMachine.Infrastructure.Exceptions;
using CoffeeMachine.Infrastructure.Interfaces.IRepositories;
using CoffeeMachine.Persistence.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace CoffeeMachine.Persistence.Repositories;

public class BanknoteRepository : IBaseRepository<Banknote>, IBanknoteRepository
{
    private readonly MyDbContext _dbContext;

    
    public BanknoteRepository(MyDbContext dbContext)
    {
        _dbContext = dbContext;
    }


    public async Task<Banknote> GetByIdAsynk(long id)
    {
        var banknote = await _dbContext.Banknotes.FirstOrDefaultAsync(x => x.Id == id);

        if (banknote == null)
            throw new NotFoundException(nameof(Banknote), id);

        return banknote;
    }

    public async Task<IEnumerable<Banknote>> GetAllAsynk()
    {
        return await _dbContext.Banknotes.ToListAsync();
    }

    public async Task<Banknote> AddAsynk(Banknote entity)
    {
        var identity = await _dbContext.Banknotes.AnyAsync(x => x.Par == entity.Par);

        if (identity != false)
            throw new AlreadyExistsException(nameof(Banknote), entity.Par);
        
        Banknote newBanknote = new Banknote()
        {
            Par = entity.Par
        };
        
        await _dbContext.Banknotes.AddAsync(newBanknote);
        await _dbContext.SaveChangesAsync();
        
        return newBanknote;
    }

    public Task<Banknote> UpdateAsync(Banknote entity)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> DeleteAsync(Banknote entity)
    {
        var deletingBanknote = _dbContext.Banknotes.FirstOrDefault(x => x.Id == entity.Id);
        
        if (deletingBanknote == null)
            throw new NotFoundException(nameof(Banknote), entity.Id);
        
        _dbContext.Banknotes.Remove(deletingBanknote);
        await _dbContext.SaveChangesAsync();
        
        return true;
    }

    public async Task<Banknote> GetByParAsynk(int par)
    {
        var banknote = await _dbContext.Banknotes.FirstOrDefaultAsync(x => x.Par == par);
        
        if (banknote == null)
            throw new NotFoundException(nameof(Banknote), par);

        return banknote;
    }
}