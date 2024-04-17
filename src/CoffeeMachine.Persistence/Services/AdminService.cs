using CoffeeMachine.Application.Interfaces.IRepositories;
using CoffeeMachine.Application.Interfaces.IServices;
using CoffeeMachine.Core.Models;

namespace CoffeeMachine.Persistence.Services;

public class AdminService : IAdminService
{
    private readonly IBanknoteRepository _banknoteRepository;
    private readonly IMachineRepository _machineRepository;
    private readonly IOrderRepository _orderRepository;
    private readonly ITransactionRepository _transactionRepository;
    private readonly ICoffeeRepository _coffeeRepository;
    
    public AdminService(IBanknoteRepository banknoteRepository, IMachineRepository machineRepository, 
        IOrderRepository orderRepository, ITransactionRepository transactionRepository, 
        ICoffeeRepository coffeeRepository)
    {
        _banknoteRepository = banknoteRepository;
        _machineRepository = machineRepository;
        _orderRepository = orderRepository;
        _transactionRepository = transactionRepository;
        _coffeeRepository = coffeeRepository;
    }


    /// <summary>
    /// Получить список всех кофемашин
    /// </summary>
    /// <returns></returns>
    public async Task<IEnumerable<Machine>> GetAllMachinesAsync()
    {
        return await _machineRepository.GetAllAsync();
    }

    /// <summary>
    /// Добавить кофемашину
    /// </summary>
    /// <param name="machine"></param>
    /// <returns></returns>
    public async Task<Machine> CreateNewMachineAsync(Machine machine)
    {
        return await _machineRepository.AddAsync(machine);
    }

    /// <summary>
    /// Удалить кофемашину
    /// </summary>
    /// <param name="machine"></param>
    /// <returns></returns>
    public async Task<bool> DeleteMachineAsync(Machine machine)
    {
        return await _machineRepository.DeleteAsync(machine);
    }

    /// <summary>
    /// Получить банкноты в кофемашине
    /// </summary>
    /// <param name="machine"></param>
    /// <returns></returns>
    public async Task<IEnumerable<Banknote>> GetBanknotesByMachineAsync(Machine machine)
    {
        return await _banknoteRepository.GetBanknotesByMachineAsync(machine);
    }

    /// <summary>
    /// Добавить банкноты в кофемашину
    /// </summary>
    /// <param name="banknotes"></param>
    /// <param name="machine"></param>
    /// <returns></returns>
    public async Task<Machine> AddBanknotesToMachineAsync(List<Banknote> banknotes, Machine machine)
    {
        await _banknoteRepository.AddBanknotesToMachineAsync(banknotes, machine);
        await _machineRepository.UpdateBalanceAsync(machine);
        
        return await _machineRepository.GetByIdAsync(machine.Id);
    }

    /// <summary>
    /// Вычесть банкноты из кофемашины
    /// </summary>
    /// <param name="banknotes"></param>
    /// <param name="machine"></param>
    /// <returns></returns>
    public async Task<Machine> SubtractBanknotesFromMachineAsync(List<Banknote> banknotes, Machine machine)
    {
        await _banknoteRepository.SubtractBanknotesFromMachineAsync(banknotes, machine);
        await _machineRepository.UpdateBalanceAsync(machine);
        
        return await _machineRepository.GetByIdAsync(machine.Id);
    }

    /// <summary>
    /// Добавить кофе в кофемашину
    /// </summary>
    /// <param name="coffee"></param>
    /// <param name="machine"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public async Task<Machine> AddCoffeeToMachineAsync(Coffee coffee, Machine machine)
    {
        // await _machineRepository.AddCoffeeToMachineAsync(coffee, machine);
        throw new NotImplementedException();
    }

    public async Task<Machine> SubtractCoffeeFromMachineAsync(Coffee coffee, Machine machine)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<Coffee>> GetAllCoffeesAsync()
    {
        throw new NotImplementedException();
    }

    public async Task<Coffee> CreateNewCoffeeAsync(Coffee coffee)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> DeleteCoffeeAsync(Coffee coffee)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<Order>> GetAllOrdersAsync()
    {
        throw new NotImplementedException();
    }
}