using System.Transactions;
using CoffeeMachine.Application.Interfaces;
using CoffeeMachine.Application.Interfaces.IRepositories;
using CoffeeMachine.Persistence.Data.Context;

namespace CoffeeMachine.Persistence;

/// <summary>
/// Unit of work.
/// </summary>
public class UnitOfWork : IUnitOfWork
{
    /// <summary>
    /// <inheritdoc cref="DataContext"/>
    /// </summary>
    private readonly DataContext _dbContext;
    
    /// <summary>
    /// <inheritdoc cref="IBanknoteRepository"/>
    /// </summary>
    private readonly IBanknoteRepository _banknoteRepository;
    
    /// <summary>
    /// <inheritdoc cref="ICoffeeRepository"/>
    /// </summary>
    private readonly ICoffeeRepository _coffeeRepository;
    
    /// <summary>
    /// <inheritdoc cref="IMachineRepository"/>
    /// </summary>
    private readonly IMachineRepository _machineRepository;
    
    /// <summary>
    /// <inheritdoc cref="IOrderRepository"/>
    /// </summary>
    private readonly IOrderRepository _orderRepository;
    
    /// <summary>
    /// <inheritdoc cref="ITransactionRepository"/>
    /// </summary>
    private readonly ITransactionRepository _transactionRepository;

    /// <summary>
    /// Конструктор класса.
    /// </summary>
    /// <param name="dbContext">Контекст для работы с базой данных.</param>
    /// <param name="banknoteRepository">Репозиторий банкнот.</param>
    /// <param name="coffeeRepository">Репозиторий кофе.</param>
    /// <param name="machineRepository">Репозиторий кофемашины.</param>
    /// <param name="orderRepository">Репозиторий заказа.</param>
    /// <param name="transactionRepository">Репозиторий транзакции.</param>
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

    /// <summary>
    /// Получение репозитория банкнот.
    /// </summary>
    public IBanknoteRepository Banknote
    {
        get => _banknoteRepository;
    }
   
    /// <summary>
    /// Получение репозитория кофе.
    /// </summary>
    public ICoffeeRepository Coffee
    {
        get => _coffeeRepository;
    }
   
    /// <summary>
    /// Получение репозитория заказа.
    /// </summary>
    public IOrderRepository Order
    {
        get => _orderRepository;
    }
   
    /// <summary>
    /// Получение репозитория транзакции.
    /// </summary>
    public ITransactionRepository Transaction
    {
        get => _transactionRepository;
    }

    /// <summary>
    /// Получение репозитория кофемашины.
    /// </summary>
    public IMachineRepository Machine
    {
        get => _machineRepository;
    }
}