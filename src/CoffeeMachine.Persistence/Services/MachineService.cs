using CoffeeMachine.Domain.Models;
using CoffeeMachine.Infrastructure.Interfaces.IRepositories;
using CoffeeMachine.Infrastructure.Interfaces.IServices;

namespace CoffeeMachine.Persistence.Services;

public class MachineService: IBaseService<Machine>, IMachineService
{
    private readonly IMachineRepository _machineRepository;

    public MachineService(IMachineRepository machineRepository)
    {
        _machineRepository = machineRepository;
    }
    
    public Task<Machine> GetById(int id)
    {
        throw new NotImplementedException();
    }

    public Task<List<Machine>> GetAll()
    {
        throw new NotImplementedException();
    }

    public Task<Machine> Add(Machine entity)
    {
        throw new NotImplementedException();
    }

    public Task<Machine> Update(Machine entity)
    {
        throw new NotImplementedException();
    }

    public Task<bool> Delete(Machine entity)
    {
        throw new NotImplementedException();
    }

    public Task<Machine> GetBySerialNumber(string serialNumber)
    {
        throw new NotImplementedException();
    }
}