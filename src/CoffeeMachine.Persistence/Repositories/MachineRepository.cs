using CoffeeMachine.Application.Exceptions;
using CoffeeMachine.Application.Interfaces.IRepositories;
using CoffeeMachine.Core.Models;
using CoffeeMachine.Persistence.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace CoffeeMachine.Persistence.Repositories;

/// <summary>
/// Репозиторий кофемашины.
/// </summary>
public class MachineRepository : GenericRepository<Machine>, IMachineRepository
{
    /// <summary>
    /// <inheritdoc cref="DataContext"/>
    /// </summary>
    private readonly DataContext _dataContext;

    /// <summary>
    /// Конструктор класса.
    /// </summary>
    /// <param name="dataContext">Контекст для работы с базой данных.</param>
    public MachineRepository(DataContext dataContext) : base(dataContext)
    {
        _dataContext = dataContext;
    }

    /// <summary>
    /// Получить кофкмашину по серийному номеру
    /// </summary>
    /// <param name="serialNumber">Серийный номер</param>
    /// <returns>Кофемашина.</returns>
    public async Task<Machine> GetBySerialNumberAsync(string serialNumber)
    {
        return await _dataContext.Machines.FirstOrDefaultAsync(x => x.SerialNumber == serialNumber);
    }

    /// <summary>
    /// Добавить кофе в кофемашину.
    /// </summary>
    /// <param name="coffee">Кофе.</param>
    /// <param name="machine">Кофемашина.</param>
    /// <returns>Кофемашина.</returns>
    public async Task<Machine> AddCoffeeInMachineAsync(Coffee coffee, Machine machine)
    {
        var coffeeMachine = new CoffeeToMachine()
        {
            Coffee = coffee,
            Machine = machine
        };
        
        await _dataContext.CoffeesToMachines.AddAsync(coffeeMachine);
        await _dataContext.TrySaveChangesToDbAsync();
        
        return machine;
    }

    /// <summary>
    /// Удалить кофе из кофемашины.
    /// </summary>
    /// <param name="coffee">Кофе.</param>
    /// <param name="machine">Кофемашина.</param>
    /// <returns>Кофемашина.</returns>
    public async Task<Machine> DeleteCoffeeFromMachineAsync(Coffee coffee, Machine machine)
    {
        var coffeeMachine = _dataContext.CoffeesToMachines.Where(cm => cm.Coffee == coffee && cm.Machine == machine);
        
        _dataContext.CoffeesToMachines.RemoveRange(coffeeMachine);
        await _dataContext.TrySaveChangesToDbAsync();

        return machine;
    }

    /// <summary>
    /// Добавить банкноты в кофемашину.
    /// </summary>
    /// <param name="banknotes">Список банкнот.</param>
    /// <param name="machine">Кофемашина.</param>
    /// <returns>Кофемашина.</returns>
    public async Task<Machine> AddBanknotesToMachineAsync(IEnumerable<Banknote> banknotes, Machine machine)
    {
        var banknoteMachines = await _dataContext.BanknotesToMachines.Include(bm => bm.Banknote)
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
                    _dataContext.BanknotesToMachines.Update(bm);
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
                await _dataContext.BanknotesToMachines.AddAsync(bm);
            }
        }
        
        await _dataContext.TrySaveChangesToDbAsync();
        
        return machine;
    }

    /// <summary>
    /// Вычесть банкноты из машины.
    /// </summary>
    /// <param name="banknotes">Список банкнот.</param>
    /// <param name="machine">Кофемашина.</param>
    /// <returns>Кофемашина.</returns>
    public async Task<Machine> SubtractBanknotesFromMachineAsync(IEnumerable<Banknote> banknotes, Machine machine)
    {
        var banknoteMachines = await _dataContext.BanknotesToMachines
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
                    _dataContext.BanknotesToMachines.Update(bm);
                }   
            }
        }
        
        await _dataContext.TrySaveChangesToDbAsync();
        
        return machine;
    }

    /// <summary>
    /// Получить список кофе из кофемашины.
    /// </summary>
    /// <param name="machine">Кофемашина.</param>
    /// <returns></returns>
    public async Task<IEnumerable<Coffee>> GetCoffeesFromMachineAsync(Machine machine)
    {
        var coffeesToMachines = _dataContext.CoffeesToMachines.Where(cm => cm.Machine == machine);
        return await coffeesToMachines.Select(cm => cm.Coffee).ToListAsync();
    }
    
    /// <summary>
    /// Проверить есть ли кофе в кофемашине. True - есть, False - нет.
    /// </summary>
    /// <param name="machine">Кофемашина.</param>
    /// <param name="coffee">Кофе.</param>
    /// <returns>bool</returns>
    public async Task<bool> CheckCoffeeInMachineAsync(Machine machine, Coffee coffee)
    {
        return await _dataContext.CoffeesToMachines.AnyAsync(cm => cm.Coffee == coffee && cm.Machine == machine);
    }
}