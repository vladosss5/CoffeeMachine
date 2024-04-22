using CoffeeMachine.Application.Interfaces.IRepositories;

namespace CoffeeMachine.Application.Interfaces;

public interface IUnitOfWork
{
    public IBanknoteRepository Banknote { get; }
    public ICoffeeRepository Coffee { get; }
    public IMachineRepository Machine { get; }
    public IOrderRepository Order { get; }
    public ITransactionRepository Transaction { get; }
}