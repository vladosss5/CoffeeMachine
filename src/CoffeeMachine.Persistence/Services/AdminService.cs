using CoffeeMachine.Application.Exceptions;
using CoffeeMachine.Application.Interfaces;
using CoffeeMachine.Application.Interfaces.IRepositories;
using CoffeeMachine.Application.Interfaces.IServices;
using CoffeeMachine.Core.Models;

namespace CoffeeMachine.Persistence.Services;

public class AdminService : IAdminService
{
    private readonly IUnitOfWork _unitOfWork;

    public AdminService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    
    /// <summary>
    /// Получить кофемашину по Id.
    /// </summary>
    /// <param name="machineId"></param>
    /// <returns></returns>
    public async Task<Machine> GetMachineByIdAsync(long machineId)
    {
        return await _unitOfWork.Machine.GetByIdAsync(machineId);
    }

    /// <summary>
    /// Получить список всех кофемашин.
    /// </summary>
    /// <returns></returns>
    public async Task<IEnumerable<Machine>> GetAllMachinesAsync()
    {
        return await _unitOfWork.Machine.GetAllAsync();
    }

    /// <summary>
    /// Создать новую кофемашину.
    /// </summary>
    /// <param name="machine"></param>
    /// <returns></returns>
    /// <exception cref="AlreadyExistsException"></exception>
    public async Task<Machine> CreateNewMachineAsync(Machine machine)
    {
        var identity = await GetMachineBySerialNumberAsync(machine.SerialNumber);
        if (identity != null)
            throw new AlreadyExistsException(nameof(Machine), machine.SerialNumber);
        
        return await _unitOfWork.Machine.AddAsync(machine);
    }

    /// <summary>
    /// Изменить кофемашину.
    /// </summary>
    /// <param name="machine"></param>
    /// <returns></returns>
    /// <exception cref="NotFoundException"></exception>
    public async Task<Machine> UpdateMachineAsync(Machine machine)
    {
        var identity = await GetMachineByIdAsync(machine.Id);
        if (identity == null)
            throw new NotFoundException(nameof(Machine), machine.Id);
        
        return await _unitOfWork.Machine.UpdateAsync(machine);
    }

    /// <summary>
    /// Удалить кофемашину.
    /// </summary>
    /// <param name="machineId"></param>
    /// <returns></returns>
    /// <exception cref="NotFoundException"></exception>
    public async Task<bool> DeleteMachineAsync(long machineId)
    {
        var identity = await _unitOfWork.Machine.GetByIdAsync(machineId);
        if (identity == null)
            throw new NotFoundException(nameof(Machine), machineId);
        
        return await _unitOfWork.Machine.DeleteAsync(identity);
    }

    /// <summary>
    /// Получить кофемашину по серийному номеру.
    /// </summary>
    /// <param name="serialNumber"></param>
    /// <returns></returns>
    /// <exception cref="NotFoundException"></exception>
    public async Task<Machine> GetMachineBySerialNumberAsync(string serialNumber)
    {
        var identity = await _unitOfWork.Machine.GetBySerialNumberAsync(serialNumber);
        if (identity == null)
            throw new NotFoundException(nameof(Machine), serialNumber);
        
        return identity;
    }

    /// <summary>
    /// Обновить баланс кофемашины.
    /// </summary>
    /// <param name="machineId"></param>
    /// <returns></returns>
    /// <exception cref="NotFoundException"></exception>
    public async Task<int> UpdateBalanceAsync(long machineId)
    {
        var machine = await _unitOfWork.Machine.GetByIdAsync(machineId);
        if (machine == null)
            throw new NotFoundException(nameof(Machine), machineId);
        
        return await _unitOfWork.Machine.UpdateBalanceAsync(machine);
    }

    /// <summary>
    /// Добавить кофе в кофемашину.
    /// </summary>
    /// <param name="coffeeId"></param>
    /// <param name="machineId"></param>
    /// <returns></returns>
    /// <exception cref="NotFoundException"></exception>
    public async Task<Machine> AddCoffeeInMachineAsync(long coffeeId, long machineId)
    {
        var coffee = await _unitOfWork.Coffee.GetByIdAsync(coffeeId);
        if (coffee == null)
            throw new NotFoundException(nameof(Coffee), coffeeId);
        
        var machine = await _unitOfWork.Machine.GetByIdAsync(machineId);
        if (machine == null)
            throw new NotFoundException(nameof(Machine), machineId);
        
        return await _unitOfWork.Machine.AddCoffeeInMachineAsync(coffee, machine);
    }

    /// <summary>
    /// Удалить кофе из кофемашины.
    /// </summary>
    /// <param name="coffeeId"></param>
    /// <param name="machineId"></param>
    /// <returns></returns>
    /// <exception cref="NotFoundException"></exception>
    public async Task<Machine> DeleteCoffeeFromMachineAsync(long coffeeId, long machineId)
    {
        var coffee = await _unitOfWork.Coffee.GetByIdAsync(coffeeId);
        if (coffee == null)
            throw new NotFoundException(nameof(Coffee), coffeeId);
        
        var machine = await _unitOfWork.Machine.GetByIdAsync(machineId);
        if (machine == null)
            throw new NotFoundException(nameof(Machine), machineId);
        
        return await _unitOfWork.Machine.DeleteCoffeeFromMachineAsync(coffee, machine);
    }

    /// <summary>
    /// Добавить банкноты в автомат.
    /// </summary>
    /// <param name="banknotes"></param>
    /// <param name="machineId"></param>
    /// <returns></returns>
    /// <exception cref="NotFoundException"></exception>
    public async Task<Machine> AddBanknotesToMachineAsync(IEnumerable<Banknote> banknotes, long machineId)
    {
        var machine = await _unitOfWork.Machine.GetByIdAsync(machineId);
        if (machine == null)
            throw new NotFoundException(nameof(Machine), machineId);
        
        return await _unitOfWork.Machine.AddBanknotesToMachineAsync(banknotes, machine);
    }

    /// <summary>
    /// Выдать банкноты из автомата.
    /// </summary>
    /// <param name="banknotes"></param>
    /// <param name="machineId"></param>
    /// <returns></returns>
    /// <exception cref="NotFoundException"></exception>
    public async Task<Machine> SubtractBanknotesFromMachineAsync(IEnumerable<Banknote> banknotes, long machineId)
    {
        var machine = await _unitOfWork.Machine.GetByIdAsync(machineId);
        if (machine == null)
            throw new NotFoundException(nameof(Machine), machineId);
        
        return await _unitOfWork.Machine.SubtractBanknotesFromMachineAsync(banknotes, machine);
    }

    /// <summary>
    /// Получить список банкнот в кофемашине.
    /// </summary>
    /// <param name="machineId"></param>
    /// <returns></returns>
    /// <exception cref="NotFoundException"></exception>
    public async Task<IEnumerable<Banknote>> GetBanknotesByMachineAsync(long machineId)
    {
        var machine = await _unitOfWork.Machine.GetByIdAsync(machineId);
        if (machine == null)
            throw new NotFoundException(nameof(Machine), machineId);
        
        return await _unitOfWork.Banknote.GetBanknotesByMachineAsync(machine);
    }

    /// <summary>
    /// Получить кофе по Id.
    /// </summary>
    /// <param name="coffeeId"></param>
    /// <returns></returns>
    public async Task<Coffee> GetCoffeeByIdAsync(long coffeeId)
    {
        return await _unitOfWork.Coffee.GetByIdAsync(coffeeId);
    }

    /// <summary>
    /// Получить список кофе.
    /// </summary>
    /// <returns></returns>
    public async Task<IEnumerable<Coffee>> GetAllCoffeesAsync()
    {
        return await _unitOfWork.Coffee.GetAllAsync();
    }

    /// <summary>
    /// Создать новый кофе.
    /// </summary>
    /// <param name="coffee"></param>
    /// <returns></returns>
    /// <exception cref="AlreadyExistsException"></exception>
    public async Task<Coffee> CreateNewCoffeeAsync(Coffee coffee)
    {
        var identity = await _unitOfWork.Coffee.GetByNameAsync(coffee.Name);
        if (identity != null)
            throw new AlreadyExistsException(nameof(Coffee), coffee.Name);
        
        return await _unitOfWork.Coffee.AddAsync(coffee);
    }

    /// <summary>
    /// Обновить кофе.
    /// </summary>
    /// <param name="coffee"></param>
    /// <returns></returns>
    /// <exception cref="NotFoundException"></exception>
    public async Task<Coffee> UpdateCoffeeAsync(Coffee coffee)
    {
        if (await _unitOfWork.Coffee.GetByIdAsync(coffee.Id) == null)
            throw new NotFoundException(nameof(Coffee), coffee.Id);
        
        return await _unitOfWork.Coffee.UpdateAsync(coffee);
    }

    /// <summary>
    /// Удалить кофе.
    /// </summary>
    /// <param name="coffeeId"></param>
    /// <returns></returns>
    /// <exception cref="NotFoundException"></exception>
    public async Task<bool> DeleteCoffeeAsync(long coffeeId)
    {
        var coffee = await _unitOfWork.Coffee.GetByIdAsync(coffeeId);
        if (coffee == null)
            throw new NotFoundException(nameof(Coffee), coffeeId);
        
        return await _unitOfWork.Coffee.DeleteAsync(coffee);
    }

    /// <summary>
    /// Получить кофе по названию.
    /// </summary>
    /// <param name="nameCoffe"></param>
    /// <returns></returns>
    /// <exception cref="NotFoundException"></exception>
    public async Task<Coffee> GetByNameAsync(string nameCoffe)
    {
        var coffee = await _unitOfWork.Coffee.GetByNameAsync(nameCoffe);

        if (coffee == null)
            throw new NotFoundException(nameof(Coffee), nameCoffe);
        
        return coffee;
    }

    /// <summary>
    /// Получить список доступных кофе для машины.
    /// </summary>
    /// <param name="machineId"></param>
    /// <returns></returns>
    /// <exception cref="NotFoundException"></exception>
    public async Task<IEnumerable<Coffee>> GetCoffeesFromMachineAsync(long machineId)
    {
        var machine = await _unitOfWork.Machine.GetByIdAsync(machineId);
        if (machine == null)
            throw new NotFoundException(nameof(Machine), machineId);
        
        return await _unitOfWork.Coffee.GetCoffeesFromMachineAsync(machine);
    }

    /// <summary>
    /// Получить заказ по Id.
    /// </summary>
    /// <param name="orderId"></param>
    /// <returns></returns>
    /// <exception cref="NotFoundException"></exception>
    public async Task<Order> GetOrderByIdAsync(long orderId)
    {
        var order = await _unitOfWork.Order.GetByIdAsync(orderId);
        if (order == null)
            throw new NotFoundException(nameof(Order), orderId);

        return order;
    }

    /// <summary>
    /// Получить список всех заказов
    /// </summary>
    /// <returns></returns>
    public async Task<IEnumerable<Order>> GetAllOrdersAsync()
    {
        return await _unitOfWork.Order.GetAllAsync();
    }

    /// <summary>
    /// Удалить заказ.
    /// </summary>
    /// <param name="orderId"></param>
    /// <returns></returns>
    /// <exception cref="NotFoundException"></exception>
    public async Task<bool> DeleteOrderAsync(long orderId)
    {
        var order = await _unitOfWork.Order.GetByIdAsync(orderId);
        if (order == null)
            throw new NotFoundException(nameof(Order), orderId);
        
        return await _unitOfWork.Order.DeleteAsync(order);
    }

    /// <summary>
    /// Получить транзакции по типу.
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    public async Task<IEnumerable<Transaction>> GetTransactionsByTypeAsync(bool type)
    {
        return await _unitOfWork.Transaction.GetTransactionsByTypeAsync(type);
    }

    /// <summary>
    /// Получить транзакции покупки.
    /// </summary>
    /// <param name="order"></param>
    /// <returns></returns>
    public async Task<IEnumerable<Transaction>> GetTransactionsByOrderAsync(Order order)
    {
        return await _unitOfWork.Transaction.GetTransactionsByOrderAsync(order);
    }
}