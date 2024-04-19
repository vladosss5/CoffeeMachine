using CoffeeMachine.Application.Exceptions;
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
    public async Task<Machine> AddBanknotesToMachineAsync(List<Banknote> banknotesReq, Machine machineReq)
    {
        var banknotes = new List<Banknote>();

        foreach (var banknote in banknotesReq)
        {
            banknotes.Add(await _banknoteRepository.GetByNominalAsync(banknote.Nominal));
        }
        
        var machine = await _machineRepository.GetByIdAsync(machineReq.Id);
        
        await _banknoteRepository.AddBanknotesToMachineAsync(banknotes, machine);
        await _machineRepository.UpdateBalanceAsync(machine);

        var machineResp = await _machineRepository.GetByIdAsync(machine.Id);
        
        return machineResp;
    }

    /// <summary>
    /// Вычесть банкноты из кофемашины
    /// </summary>
    /// <param name="banknotes"></param>
    /// <param name="machine"></param>
    /// <returns></returns>
    public async Task<Machine> SubtractBanknotesFromMachineAsync(List<Banknote> banknotesReq, Machine machineReq)
    {
        var banknotes = new List<Banknote>();
        
        foreach (var banknote in banknotesReq)
        {
            banknotes.Add(await _banknoteRepository.GetByNominalAsync(banknote.Nominal));
        }
        
        var machine = await _machineRepository.GetByIdAsync(machineReq.Id);
        
        await _banknoteRepository.SubtractBanknotesFromMachineAsync(banknotes, machine);
        await _machineRepository.UpdateBalanceAsync(machine);
        
        var machineResp = await _machineRepository.GetByIdAsync(machine.Id);
        
        return machineResp;
    }

    /// <summary>
    /// Добавить кофе в кофемашину
    /// </summary>
    /// <param name="coffee"></param>
    /// <param name="machine"></param>
    /// <returns></returns>
    public async Task<Machine> AddCoffeeToMachineAsync(Coffee coffee, Machine machine)
    {
        var nCoffee = await _coffeeRepository.GetByNameAsync(coffee.Name);
        var nMachine = await _machineRepository.GetBySerialNumberAsync(machine.SerialNumber);
        
        return await _machineRepository.AddCoffeeInMachineAsync(nCoffee, nMachine);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="coffee"></param>
    /// <param name="machine"></param>
    /// <returns></returns>
    public async Task<Machine> DeleteCoffeeFromMachineAsync(Coffee coffee, Machine machine)
    {
        var nCoffee = await _coffeeRepository.GetByNameAsync(coffee.Name);
        var nMachine = await _machineRepository.GetBySerialNumberAsync(machine.SerialNumber);
        
        return await _machineRepository.DeleteCoffeeFromMachineAsync(nCoffee, nMachine);
    }

    /// <summary>
    /// Получить список кофе
    /// </summary>
    /// <returns></returns>
    public async Task<IEnumerable<Coffee>> GetAllCoffeesAsync()
    {
        return await _coffeeRepository.GetAllAsync();
    }

    /// <summary>
    /// Создать новый кофе
    /// </summary>
    /// <param name="coffee"></param>
    /// <returns></returns>
    public async Task<Coffee> CreateNewCoffeeAsync(Coffee coffee)
    {
        return await _coffeeRepository.AddAsync(coffee);
    }

    /// <summary>
    /// Удалить кофе
    /// </summary>
    /// <param name="coffee"></param>
    /// <returns></returns>
    public async Task<bool> DeleteCoffeeAsync(Coffee coffee)
    {
        return await _coffeeRepository.DeleteAsync(coffee);
    }

    /// <summary>
    /// Получить список всех заказов
    /// </summary>
    /// <returns></returns>
    public async Task<IEnumerable<Order>> GetAllOrdersAsync()
    {
        return await _orderRepository.GetAllAsync();
    }
}