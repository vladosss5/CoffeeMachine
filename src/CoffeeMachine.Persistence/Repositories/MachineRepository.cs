using CoffeeMachine.Application.Exceptions;
using CoffeeMachine.Application.Interfaces.IRepositories;
using CoffeeMachine.Core.Models;
using CoffeeMachine.Persistence.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace CoffeeMachine.Persistence.Repositories;

public class MachineRepository : IMachineRepository
{
    private readonly DataContext _dbContext;

    public MachineRepository(DataContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    /// <summary>
    /// Получить машину по Id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public async Task<Machine> GetByIdAsync(long id)
    {
        var machine = await _dbContext.Machines.FirstOrDefaultAsync(x => x.Id == id);
        
        if (machine == null)
            throw new NotFoundException(nameof(Machine), id);
        
        return machine;
    }

    /// <summary>
    /// Получить список машин
    /// </summary>
    /// <returns></returns>
    public async Task<IEnumerable<Machine>> GetAllAsync()
    {
        return await _dbContext.Machines.ToListAsync();
    }

    /// <summary>
    /// Добавить кофемашину
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public async Task<Machine> AddAsync(Machine entity)
    {
        if (!await _dbContext.Machines.AnyAsync(x => x.SerialNumber == entity.SerialNumber))
            throw new AlreadyExistsException(nameof(Machine), entity.SerialNumber);

        Machine newMachine = new Machine()
        {
            SerialNumber = entity.SerialNumber,
            Description = entity.Description
        };
        
        await _dbContext.Machines.AddAsync(newMachine);
        await _dbContext.TrySaveChangesToDbAsync();
        
        return newMachine;
    }
    
    /// <summary>
    /// Изменить кофемашину
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    /// <exception cref="NotFoundException"></exception>
    public async Task<Machine> UpdateAsync(Machine entity)
    {
        var machine = await _dbContext.Machines.FirstOrDefaultAsync(x => x.Id == entity.Id);
        
        if (machine == null)
            throw new NotFoundException(nameof(Machine), entity.Id);
        
        machine.SerialNumber = entity.SerialNumber;
        machine.Description = entity.Description;
        machine.Balance = entity.Balance;
        
        _dbContext.Machines.Update(machine);
        await _dbContext.TrySaveChangesToDbAsync();
        
        return machine;
    }

    /// <summary>
    /// Удалить кофемашину
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public async Task<bool> DeleteAsync(Machine entity)
    {
        var machine = await _dbContext.Machines.FirstOrDefaultAsync(x => 
            x.Id == entity.Id && 
            x.SerialNumber == entity.SerialNumber);
        
        if (machine == null)
            throw new NotFoundException(nameof(Machine), entity.SerialNumber);
        
        _dbContext.Machines.Remove(machine);
        await _dbContext.TrySaveChangesToDbAsync();
        
        return true;
    }

    /// <summary>
    /// Получить кофкмашину по серийному номеру
    /// </summary>
    /// <param name="serialNumber"></param>
    /// <returns></returns>
    /// <exception cref="NotFoundException"></exception>
    public async Task<Machine> GetBySerialNumberAsync(string serialNumber)
    {
        var machine = await _dbContext.Machines.FirstOrDefaultAsync(x => x.SerialNumber == serialNumber);
        
        if (machine == null)
            throw new NotFoundException(nameof(Machine), serialNumber);
        
        return machine;
    }

    /// <summary>
    /// Пересчитать баланс машины
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    public async Task<int> UpdateBalanceAsync(Machine entity)
    {
        var machine = await GetBySerialNumberAsync(entity.SerialNumber);
        
        var banknoteMachines = await _dbContext.BanknotesToMachines.Where(bm =>
            bm.Machine.SerialNumber == machine.SerialNumber).Include(bm => bm.Banknote).ToListAsync();
        
        int balance = 0;

        foreach (var bm in banknoteMachines)
        {
            balance += bm.Banknote.Nominal * bm.CountBanknote;
        }
        
        machine.Balance = balance;
        
        _dbContext.Machines.Update(machine);
        await _dbContext.TrySaveChangesToDbAsync();
        
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
        if (!await _dbContext.Coffees.AnyAsync(c => coffee.Id == c.Id))
            throw new NotFoundException(nameof(Coffee), coffee.Id);

        if (!await _dbContext.Machines.AnyAsync(m => machine.Id == m.Id))
            throw new NotFoundException(nameof(Machine), machine.Id);

        if (!await _dbContext.CoffeesToMachines.AnyAsync(ctm => ctm.Coffee == coffee && ctm.Machine == machine))
            throw new AlreadyExistsException(nameof(Coffee), coffee.Name);
        
        var coffeeMachine = new CoffeeToMachine()
        {
            Coffee = coffee,
            Machine = machine
        };
        
        await _dbContext.CoffeesToMachines.AddAsync(coffeeMachine);
        await _dbContext.TrySaveChangesToDbAsync();
        
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
        if (!await _dbContext.Coffees.AnyAsync(c => coffee.Id == c.Id))
            throw new NotFoundException(nameof(Coffee), coffee.Id);

        if (!await _dbContext.Machines.AnyAsync(m => machine.Id == m.Id))
            throw new NotFoundException(nameof(Machine), machine.Id);
        
        var coffeeMachine = _dbContext.CoffeesToMachines.Where(cm => cm.Coffee == coffee && cm.Machine == machine);
        
        _dbContext.CoffeesToMachines.RemoveRange(coffeeMachine);
        await _dbContext.TrySaveChangesToDbAsync();

        return machine;
    }

    /// <summary>
    /// Получить список кофе из кофемашины
    /// </summary>
    /// <param name="machine"></param>
    /// <returns></returns>
    public async Task<List<Coffee>> GetCoffeesFromMachineAsync(Machine machine)
    {
        var coffeesToMachines = _dbContext.CoffeesToMachines.Where(cm => cm.Machine == machine);
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
        return await _dbContext.CoffeesToMachines.AnyAsync(cm => cm.Coffee == coffee && cm.Machine == machine);
    }
}