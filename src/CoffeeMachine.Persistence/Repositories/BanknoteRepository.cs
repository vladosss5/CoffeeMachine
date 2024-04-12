using CoffeeMachine.Domain.Models;
using CoffeeMachine.Infrastructure.Exceptions;
using CoffeeMachine.Infrastructure.Interfaces.IRepositories;
using CoffeeMachine.Persistence.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace CoffeeMachine.Persistence.Repositories;

public class BanknoteRepository : IBaseRepository<Banknote>, IBanknoteRepository
{
    private readonly DataContext _dbContext;

    
    public BanknoteRepository(DataContext dbContext)
    {
        _dbContext = dbContext;
    }


    /// <summary>
    /// 
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    /// <exception cref="NotFoundException"></exception>
    public async Task<Banknote> GetByIdAsync(long id)
    {
        var banknote = await _dbContext.Banknotes.FirstOrDefaultAsync(x => x.Id == id);

        if (banknote == null)
            throw new NotFoundException(nameof(Banknote), id);

        return banknote;
    }
    
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>

    public async Task<IEnumerable<Banknote>> GetAllAsync()
    {
        return await _dbContext.Banknotes.ToListAsync();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    /// <exception cref="AlreadyExistsException"></exception>
    public async Task<Banknote> AddAsync(Banknote entity)
    {
        var identity = await _dbContext.Banknotes.AnyAsync(x => x.Nominal == entity.Nominal);

        if (identity != false)
            throw new AlreadyExistsException(nameof(Banknote), entity.Nominal);
        
        Banknote newBanknote = new Banknote()
        {
            Nominal = entity.Nominal
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

    public async Task<Banknote> GetByParAsync(int par)
    {
        var banknote = await _dbContext.Banknotes.FirstOrDefaultAsync(x => x.Nominal == par);
        
        if (banknote == null)
            throw new NotFoundException(nameof(Banknote), par);

        return banknote;
    }

    public async Task<IEnumerable<Banknote>> GetBanknotesByMachineAsync(Machine machine)
    {
        var banknoteMachine = _dbContext.BanknotesMachines.Where(bm => machine.SerialNumber == bm.Machine.SerialNumber).ToList();
        var banknotes = new List<Banknote>();

        foreach (var b in _dbContext.Banknotes)
        {
            foreach (var bm in banknoteMachine)
            {
                if (b == bm.Banknote)
                {
                    banknotes.Add(b);
                }
            }
        }
        
        return banknotes;
    }

    // public async Task<List<Banknote>> GetByPurchase(Purchase entity)
    // {
    //      // var purchase = _dbContext.Purchases.F
    //     // return await _dbContext.Banknotes.Where(b => b.).ToListAsync();
    // }
}