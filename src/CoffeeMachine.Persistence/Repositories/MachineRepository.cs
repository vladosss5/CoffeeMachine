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
        var identity = await _dbContext.Machines.AnyAsync(x => x.SerialNumber == entity.SerialNumber);
        
        if (identity != false)
            throw new AlreadyExistsException(nameof(Machine), entity.SerialNumber);

        Machine newMachine = new Machine()
        {
            SerialNumber = entity.SerialNumber,
            Description = entity.Description
        };
        
        await _dbContext.Machines.AddAsync(newMachine);
        await _dbContext.SaveChangesAsync();
        
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
        await _dbContext.SaveChangesAsync();
        
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
            x.Id == entity.Id || 
            x.SerialNumber == entity.SerialNumber);
        
        if (machine == null)
            throw new NotFoundException(nameof(Machine), entity.SerialNumber);
        
        _dbContext.Machines.Remove(machine);
        await _dbContext.SaveChangesAsync();
        
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
        var machine = GetBySerialNumberAsync(entity.SerialNumber).Result;
        
        var banknoteMachines = await _dbContext.BanknotesToMachines.Where(bm =>
            bm.Machine.SerialNumber == machine.SerialNumber).Include(bm => bm.Banknote).ToListAsync();
        
        int balance = 0;

        foreach (var bm in banknoteMachines)
        {
            balance += bm.Banknote.Nominal * bm.CountBanknote;
        }
        
        machine.Balance = balance;
        
        _dbContext.Machines.Update(machine);
        await _dbContext.SaveChangesAsync();
        
        return balance;
    }
}