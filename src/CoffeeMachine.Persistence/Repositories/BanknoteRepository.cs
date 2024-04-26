using CoffeeMachine.Application.Exceptions;
using CoffeeMachine.Application.Interfaces.IRepositories;
using CoffeeMachine.Core.Models;
using CoffeeMachine.Persistence.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace CoffeeMachine.Persistence.Repositories;

public class BanknoteRepository : GenericRepository<Banknote>, IBanknoteRepository
{
    private readonly DataContext _dbContext;

    public BanknoteRepository(DataContext dataContext) : base(dataContext)
    {
        _dbContext = dataContext;
    }

    /// <summary>
    /// Получить банкноту по номиналу.
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
    /// Получить список банкнот в машине.
    /// </summary>
    /// <param name="machine"></param>
    /// <returns></returns>
    public async Task<IEnumerable<BanknoteToMachine>> GetBanknotesByMachineAsync(Machine machine) 
    {
        var banknotesToMachine = await _dbContext.BanknotesToMachines
            .Where(bm => bm.Machine.SerialNumber == machine.SerialNumber && bm.CountBanknote != 0)
            .OrderByDescending(bm => bm.Banknote.Nominal)
            .Include(bm => bm.Banknote)
            .ToListAsync();

        return banknotesToMachine;
    }
}