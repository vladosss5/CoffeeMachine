using CoffeeMachine.Application.Exceptions;
using CoffeeMachine.Application.Interfaces.IRepositories;
using CoffeeMachine.Core.Models;
using CoffeeMachine.Persistence.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace CoffeeMachine.Persistence.Repositories;

/// <summary>
/// Репозиторий банкнот.
/// </summary>
public class BanknoteRepository : GenericRepository<Banknote>, IBanknoteRepository
{
    /// <summary>
    /// <inheritdoc cref="DataContext"/>
    /// </summary>
    private readonly DataContext _dataContext;

    
    /// <summary>
    /// Конструктор класса.
    /// </summary>
    /// <param name="dataContext">Контекст для работы с базой данных.</param>
    public BanknoteRepository(DataContext dataContext) : base(dataContext)
    {
        _dataContext = dataContext;
    }

    /// <summary>
    /// Получить банкноту по номиналу.
    /// </summary>
    /// <param name="nominal"> Номинал банкноты. </param>
    /// <returns> Банкнота с указанным номиналом. </returns>
    public async Task<Banknote> GetByNominalAsync(int nominal)
    {
        return await _dataContext.Banknotes.FirstOrDefaultAsync(x => x.Nominal == nominal);
    }

    /// <summary>
    /// Получить список банкнот в машине.
    /// </summary>
    /// <param name="machine"> Кофемашина. </param>
    /// <returns> Список банкнот в кофемашине. </returns>
    public async Task<IEnumerable<BanknoteToMachine>> GetBanknotesByMachineAsync(Machine machine) 
    {
        var banknotesToMachine = await _dataContext.BanknotesToMachines
            .Where(bm => bm.Machine.SerialNumber == machine.SerialNumber)
            .OrderByDescending(bm => bm.Banknote.Nominal)
            .Include(bm => bm.Banknote)
            .ToListAsync();

        return banknotesToMachine;
    }
}