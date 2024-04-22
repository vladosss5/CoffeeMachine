using System.Transactions;
using CoffeeMachine.Application.Interfaces;
using CoffeeMachine.Application.Interfaces.IRepositories;
using CoffeeMachine.Persistence.Data.Context;

namespace CoffeeMachine.Persistence;

public class UnitOfWork : IUnitOfWork
{
    private readonly DataContext _dbContext;
    private readonly IBanknoteRepository _banknoteRepository;
    private readonly ICoffeeRepository _coffeeRepository;
    private readonly IMachineRepository _machineRepository;
    private readonly IOrderRepository _orderRepository;
    private readonly ITransactionRepository _transactionRepository;

    public UnitOfWork(DataContext dbContext,
        IBanknoteRepository banknoteRepository,
        ICoffeeRepository coffeeRepository,
        IMachineRepository machineRepository,
        IOrderRepository orderRepository,
        ITransactionRepository transactionRepository)
    {
        _dbContext = dbContext;
        _banknoteRepository = banknoteRepository;
        _coffeeRepository = coffeeRepository;
        _machineRepository = machineRepository;
        _orderRepository = orderRepository;
        _transactionRepository = transactionRepository;
    }

    public IBanknoteRepository Banknote
    {
        get => _banknoteRepository;
    }
   
    public ICoffeeRepository Coffee
    {
        get => _coffeeRepository;
    }
   
    public IOrderRepository Order
    {
        get => _orderRepository;
    }
   
    public ITransactionRepository Transaction
    {
        get => _transactionRepository;
    }

    public IMachineRepository Machine
    {
        get => _machineRepository;
    }
}