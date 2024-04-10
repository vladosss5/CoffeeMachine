using CoffeeMachine.Domain.Models;
using CoffeeMachine.Infrastructure.Interfaces.IRepositories;
using CoffeeMachine.Persistence.Data.Context;

namespace CoffeeMachine.Persistence.Repositories;

public class MachineRepository : IBaseRepository<Machine>, IMachineRepository
{
    private readonly MyDbContext _dbContext;

    public MachineRepository(MyDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public Task<Machine> GetByIdAsynk(int id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Machine>> GetAllAsynk()
    {
        throw new NotImplementedException();
    }

    public Task<Machine> AddAsynk(Machine entity)
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