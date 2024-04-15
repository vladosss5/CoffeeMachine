using CoffeeMachine.Domain.Models;
using CoffeeMachine.Infrastructure.Exceptions;
using CoffeeMachine.Infrastructure.Interfaces.IRepositories;
using CoffeeMachine.Persistence.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace CoffeeMachine.Persistence.Repositories;

public class BanknoteRepository : IBaseRepository<Banknote>, IBanknoteRepository
{
    private readonly DataContext _dbContext;

    
    public BanknoteRepository(DataContext dbContext, IMachineRepository machineRepository)
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

    public async Task<Banknote> GetByNominalAsync(int par)
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

    public async Task<IEnumerable<BanknoteMachine>> SubtractBanknotesFromMachineAsync(IEnumerable<Banknote> banknotes, Machine machine)
    {
        var banknoteMachines = await _dbContext.BanknotesMachines.Include(bm => bm.Machine).ToListAsync();

        foreach (var bm in banknoteMachines)
        {
            foreach (var banknote in banknotes)
            {
                if (bm.Machine.SerialNumber == machine.SerialNumber && bm.Banknote.Nominal == banknote.Nominal)
                {
                    if (bm.CountBanknote > 1)
                    {
                        bm.CountBanknote--;
                        _dbContext.BanknotesMachines.Update(bm);
                    }
                    else
                    {
                        _dbContext.Remove(bm);
                    }
                }   
            }
        }
        
        await _dbContext.SaveChangesAsync();
        
        return banknoteMachines;
    }

    public async Task<IEnumerable<Banknote>> AddBanknotesToMachineAsync(IEnumerable<Transaction> transactions, Machine machine)
    {
        var banknoteMachines = await _dbContext.BanknotesMachines
            .Include(bm => bm.Machine)
            .Where(bm => bm.Machine.SerialNumber == machine.SerialNumber)
            .ToListAsync();
        
        var banknotes = new List<Banknote>();
        
        foreach (var transaction in transactions)
        {
            bool found = false;
        
            foreach (var bm in banknoteMachines)
            {
                if (bm.Banknote == transaction.Banknote && bm.Machine == machine)
                {
                    bm.CountBanknote++;
                    _dbContext.BanknotesMachines.Update(bm);
                    found = true;
                    break;
                }
            }
        
            if (!found)
            {
                BanknoteMachine newBM = new BanknoteMachine
                {
                    Banknote = transaction.Banknote,
                    CountBanknote = 1,
                    Machine = machine
                };
                await _dbContext.BanknotesMachines.AddAsync(newBM);
            }
        }
        return banknotes;
    }
}