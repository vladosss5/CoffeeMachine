using CoffeeMachine.Domain.Models;
using CoffeeMachine.Infrastructure.Exceptions;
using CoffeeMachine.Infrastructure.Interfaces.IRepositories;
using CoffeeMachine.Persistence.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace CoffeeMachine.Persistence.Repositories;

public class MachineRepository : IBaseRepository<Machine>, IMachineRepository
{
    private readonly DataContext _dbContext;

    public MachineRepository(DataContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<Machine> GetByIdAsync(long id)
    {
        var machine = await _dbContext.Machines.FirstOrDefaultAsync(x => x.Id == id);
        
        if (machine == null)
            throw new NotFoundException(nameof(Machine), id);
        
        return machine;
    }

    public async Task<IEnumerable<Machine>> GetAllAsync()
    {
        return await _dbContext.Machines.ToListAsync();
    }

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

    public async Task<Machine> UpdateAsync(Machine entity)
    {
        var machine = await _dbContext.Machines
            .FirstOrDefaultAsync(x => x.SerialNumber == entity.SerialNumber);
        
        if (machine == null)
            throw new NotFoundException(nameof(Machine), entity.SerialNumber);
        
        machine.Description = entity.Description;
        machine.Balance = entity.Balance;
        
        _dbContext.Machines.Update(machine);
        await _dbContext.SaveChangesAsync();
        
        return machine;
    }

    public async Task<bool> DeleteAsync(Machine entity)
    {
        var machine = await _dbContext.Machines
            .FirstOrDefaultAsync(x => x.SerialNumber == entity.SerialNumber);
        
        if (machine == null)
            throw new NotFoundException(nameof(Machine), entity.SerialNumber);
        
        _dbContext.Machines.Remove(machine);
        await _dbContext.SaveChangesAsync();
        
        return true;
    }

    public async Task<Machine> GetBySerialNumberAsync(string serialNumber)
    {
        var machine = await _dbContext.Machines
            .FirstOrDefaultAsync(x => x.SerialNumber == serialNumber);
        
        if (machine == null)
            throw new NotFoundException(nameof(Machine), serialNumber);
        
        return machine;
    }

    public async Task<int> UpdateBalanceAsync(Machine entity)
    {
        var machine = GetBySerialNumberAsync(entity.SerialNumber).Result;
        int balance = 0;
        List<BanknoteMachine> banknoteMachines = await _dbContext.BanknotesMachines.Where(bm =>
            bm.Machine.SerialNumber == machine.SerialNumber).ToListAsync();

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