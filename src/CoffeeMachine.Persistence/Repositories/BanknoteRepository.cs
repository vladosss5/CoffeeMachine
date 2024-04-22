using CoffeeMachine.Application.Exceptions;
using CoffeeMachine.Application.Interfaces.IRepositories;
using CoffeeMachine.Core.Models;
using CoffeeMachine.Persistence.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace CoffeeMachine.Persistence.Repositories;

public class BanknoteRepository : IBanknoteRepository
{
    private readonly DataContext _dbContext;

    public BanknoteRepository(DataContext dataContext)
    {
        _dbContext = dataContext;
    }
    
    /// <summary>
    /// Получить банкноту по Id
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
    /// Получить список банкнот
    /// </summary>
    /// <returns></returns>
    public async Task<IEnumerable<Banknote>> GetAllAsync()
    {
        return await _dbContext.Banknotes.ToListAsync();
    }

    /// <summary>
    /// Добавить банкноту
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    /// <exception cref="AlreadyExistsException"></exception>
    public async Task<Banknote> AddAsync(Banknote entity)
    {
        if (!await _dbContext.Banknotes.AnyAsync(x => x.Nominal == entity.Nominal))
            throw new AlreadyExistsException(nameof(Banknote), entity.Nominal);
        
        Banknote newBanknote = new Banknote()
        {
            Nominal = entity.Nominal
        };
        
        await _dbContext.Banknotes.AddAsync(newBanknote);
        await _dbContext.SaveChangesAsync();
        
        return newBanknote;
    }
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public Task<Banknote> UpdateAsync(Banknote entity)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Удалить банкноту с идентичным номиналом
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    /// <exception cref="NotFoundException"></exception>
    public async Task<bool> DeleteAsync(Banknote entity)
    {
        var deletingBanknote = _dbContext.Banknotes.FirstOrDefault(x => x.Nominal == entity.Nominal);
        
        if (deletingBanknote == null)
            throw new NotFoundException(nameof(Banknote), entity.Nominal);
        
        _dbContext.Banknotes.Remove(deletingBanknote);
        await _dbContext.SaveChangesAsync();
        
        return true;
    }

    /// <summary>
    /// Получить банкноту по номиналу
    /// </summary>
    /// <param name="par"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public async Task<Banknote> GetByNominalAsync(int par)
    {
        var banknote = await _dbContext.Banknotes.FirstOrDefaultAsync(x => x.Nominal == par);
        
        if (banknote == null)
            throw new NotFoundException(nameof(Banknote), par);

        return banknote;
    }

    /// <summary>
    /// Получить список банкнот в машине
    /// </summary>
    /// <param name="machine"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public async Task<IEnumerable<Banknote>> GetBanknotesByMachineAsync(Machine machine) 
    {
        var banknotes = await _dbContext.BanknotesToMachines
            .Where(bm => bm.Machine.SerialNumber == machine.SerialNumber && bm.CountBanknote != 0)
            .Select(bm => bm.Banknote)
            .OrderByDescending(b => b.Nominal)
            .ToListAsync();

        return banknotes;
    }

    /// <summary>
    /// Добавить банкноты в машину
    /// </summary>
    /// <param name="banknotes"></param>
    /// <param name="machine"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public async Task<IEnumerable<BanknoteToMachine>> AddBanknotesToMachineAsync(IEnumerable<Banknote> banknotes, Machine machine)
    {
        var banknoteMachines = await _dbContext.BanknotesToMachines.Include(bm => bm.Banknote)
            .Where(bm => bm.Machine.SerialNumber == machine.SerialNumber).ToListAsync();

        foreach (var banknote in banknotes)
        {
            bool presence = false;
            foreach (var bm in banknoteMachines)
            {
                if (bm.Banknote == banknote)
                {
                    presence = true;
                    bm.CountBanknote++;
                    _dbContext.BanknotesToMachines.Update(bm);
                }
            }

            if (presence == false)
            {
                var bm = new BanknoteToMachine()
                {
                    Banknote = banknote,
                    CountBanknote = 1,
                    Machine = machine
                };
                await _dbContext.BanknotesToMachines.AddAsync(bm);
            }
        }
        
        await _dbContext.SaveChangesAsync();
        
        return banknoteMachines;
    }

    /// <summary>
    /// Вычесть банкноты из машины
    /// </summary>
    /// <param name="banknotes"></param>
    /// <param name="machine"></param>
    /// <returns></returns>
    public async Task<IEnumerable<BanknoteToMachine>> SubtractBanknotesFromMachineAsync(IEnumerable<Banknote> banknotes, Machine machine)
    {
        var banknoteMachines = await _dbContext.BanknotesToMachines
            .Include(bm => bm.Machine)
            .Include(bm => bm.Banknote)
            .Where(bm => bm.Machine == machine)
            .ToListAsync();

        foreach (var bm in banknoteMachines)
        {
            foreach (var banknote in banknotes)
            {
                if (bm.Machine.SerialNumber == machine.SerialNumber && 
                    bm.Banknote.Nominal == banknote.Nominal && bm.CountBanknote >= 1)
                {
                    bm.CountBanknote--;
                    _dbContext.BanknotesToMachines.Update(bm);
                }   
            }
        }
        
        await _dbContext.SaveChangesAsync();
        
        return await _dbContext.BanknotesToMachines.Include(bm => bm.Machine)
            .Where(bm => bm.Machine == machine).ToListAsync();
    }
}