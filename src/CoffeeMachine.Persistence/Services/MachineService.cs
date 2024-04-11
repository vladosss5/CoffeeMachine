using CoffeeMachine.Domain.Models;
using CoffeeMachine.Infrastructure.Interfaces.IRepositories;
using CoffeeMachine.Infrastructure.Interfaces.IServices;

namespace CoffeeMachine.Persistence.Services;

public class MachineService: IBaseService<Machine>, IMachineService //Убрать IbaseService
{
    private readonly IMachineRepository _machineRepository;

    public MachineService(IMachineRepository machineRepository)
    {
        _machineRepository = machineRepository;
    }
    
    public Task<Machine> GetByIdAsync(long id)
    {
        throw new NotImplementedException();
    }

    public Task<List<Machine>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<Machine> AddAsync(Machine entity)
    {
        throw new NotImplementedException();
    }

    public Task<Machine> UpdateAsync(Machine entity)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteAsync(Machine entity)
    {
        throw new NotImplementedException();
    }

    public Task<Machine> GetBySerialNumberAsync(string serialNumber)
    {
        throw new NotImplementedException();
    }
}