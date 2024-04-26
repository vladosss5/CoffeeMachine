using CoffeeMachine.Application.Exceptions;
using CoffeeMachine.Application.Interfaces.IRepositories;
using CoffeeMachine.Core.Models;
using CoffeeMachine.Persistence.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace CoffeeMachine.Persistence.Repositories;

public class MachineRepository : GenericRepository<Machine>, IMachineRepository
{
    private readonly DataContext _dataContext;

    public MachineRepository(DataContext dataContext) : base(dataContext)
    {
        _dataContext = dataContext;
    }

    /// <summary>
    /// Получить кофкмашину по серийному номеру
    /// </summary>
    /// <param name="serialNumber"></param>
    /// <returns></returns>
    /// <exception cref="NotFoundException"></exception>
    public async Task<Machine> GetBySerialNumberAsync(string serialNumber)
    {
        return await _dataContext.Machines.FirstOrDefaultAsync(x => x.SerialNumber == serialNumber);
    }

    /// <summary>
    /// Пересчитать баланс машины
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    public async Task<int> UpdateBalanceAsync(Machine entity)
    {
        var machine = await GetBySerialNumberAsync(entity.SerialNumber);
        
        var banknoteMachines = await _dataContext.BanknotesToMachines.Where(bm =>
            bm.Machine.SerialNumber == machine.SerialNumber).Include(bm => bm.Banknote).ToListAsync();
        
        int balance = 0;

        foreach (var bm in banknoteMachines)
        {
            balance += bm.Banknote.Nominal * bm.CountBanknote;
        }
        
        machine.Balance = balance;
        
        _dataContext.Machines.Update(machine);
        await _dataContext.TrySaveChangesToDbAsync();
        
        return balance;
    }

    /// <summary>
    /// Добавить кофе в кофемашину
    /// </summary>
    /// <param name="coffee"></param>
    /// <param name="machine"></param>
    /// <returns></returns>
    public async Task<Machine> AddCoffeeInMachineAsync(Coffee coffee, Machine machine)
    {
        if (await _dataContext.CoffeesToMachines.AnyAsync(ctm => ctm.Coffee == coffee && ctm.Machine == machine))
            throw new AlreadyExistsException(nameof(Coffee), coffee.Name);
        
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
    /// Удалить кофе из кофемашины
    /// </summary>
    /// <param name="coffee"></param>
    /// <param name="machine"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public async Task<Machine> DeleteCoffeeFromMachineAsync(Coffee coffee, Machine machine)
    {
        var coffeeMachine = _dataContext.CoffeesToMachines.Where(cm => cm.Coffee == coffee && cm.Machine == machine);
        
        _dataContext.CoffeesToMachines.RemoveRange(coffeeMachine);
        await _dataContext.TrySaveChangesToDbAsync();

        return machine;
    }

    /// <summary>
    /// Добавить банкноты в машину.
    /// </summary>
    /// <param name="banknotes"></param>
    /// <param name="machine"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
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
    /// <param name="banknotes"></param>
    /// <param name="machine"></param>
    /// <returns></returns>
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
    /// Получить список кофе из кофемашины
    /// </summary>
    /// <param name="machine"></param>
    /// <returns></returns>
    public async Task<IEnumerable<Coffee>> GetCoffeesFromMachineAsync(Machine machine)
    {
        var coffeesToMachines = _dataContext.CoffeesToMachines.Where(cm => cm.Machine == machine);
        return await coffeesToMachines.Select(cm => cm.Coffee).ToListAsync();
    }
    
    /// <summary>
    /// Проверить есть ли кофе в кофемашине
    /// </summary>
    /// <param name="machine"></param>
    /// <param name="coffee"></param>
    /// <returns></returns>
    public async Task<bool> CheckCoffeeInMachineAsync(Machine machine, Coffee coffee)
    {
        return await _dataContext.CoffeesToMachines.AnyAsync(cm => cm.Coffee == coffee && cm.Machine == machine);
    }
}