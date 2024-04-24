using CoffeeMachine.Application.Interfaces.IRepositories;

namespace CoffeeMachine.Application.Interfaces;

public interface IUnitOfWork
{
    /// <summary>
    /// Репозиторий для работы с банкнотами.
    /// </summary>
    public IBanknoteRepository Banknote { get; }
    
    /// <summary>
    /// Репозиторий для работы с кофе.
    /// </summary>
    public ICoffeeRepository Coffee { get; }
    
    /// <summary>
    /// Репозиторий для работы с кофемашинами.
    /// </summary>
    public IMachineRepository Machine { get; }
    
    /// <summary>
    /// Репозиторий для работы с заказами.
    /// </summary>
    public IOrderRepository Order { get; }
    
    /// <summary>
    /// Репозиторий для работы с транзакциями.
    /// </summary>
    public ITransactionRepository Transaction { get; }
}