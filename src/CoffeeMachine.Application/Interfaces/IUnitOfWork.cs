using CoffeeMachine.Application.Interfaces.IRepositories;

namespace CoffeeMachine.Application.Interfaces;

/// <summary>
/// Unit of work.
/// </summary>
public interface IUnitOfWork
{
    /// <summary>
    /// <inheritdoc cref="IBanknoteRepository"/>
    /// </summary>
    public IBanknoteRepository Banknote { get; }
    
    /// <summary>
    /// <inheritdoc cref="ICoffeeRepository"/>
    /// </summary>
    public ICoffeeRepository Coffee { get; }
    
    /// <summary>
    /// <inheritdoc cref="IMachineRepository"/>
    /// </summary>
    public IMachineRepository Machine { get; }
    
    /// <summary>
    /// <inheritdoc cref="IOrderRepository"/>
    /// </summary>
    public IOrderRepository Order { get; }
    
    /// <summary>
    /// <inheritdoc cref="ITransactionRepository"/>
    /// </summary>
    public ITransactionRepository Transaction { get; }
    
    /// <summary>
    /// <inheritdoc cref="IUserRepository"/>
    /// </summary>
    public IUserRepository User { get; }
}