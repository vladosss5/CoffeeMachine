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
    /// Получить список всех кофемашин
    /// </summary>
    /// <returns></returns>
    public async Task<IEnumerable<Machine>> GetAllMachinesAsync()
    {
        return await _unitOfWork.Machine.GetAllAsync();
    }

    /// <summary>
    /// Добавить кофемашину
    /// </summary>
    /// <param name="machine"></param>
    /// <returns></returns>
    public async Task<Machine> CreateNewMachineAsync(Machine machine)
    {
        return await _unitOfWork.Machine.AddAsync(machine);
    }

    /// <summary>
    /// Изменить кофемашину
    /// </summary>
    /// <param name="machine"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public async Task<Machine> UpdateMachineAsync(Machine machine)
    {
        return await _unitOfWork.Machine.UpdateAsync(machine);
    }

    /// <summary>
    /// Удалить кофемашину
    /// </summary>
    /// <param name="machine"></param>
    /// <returns></returns>
    public async Task<bool> DeleteMachineAsync(Machine machine)
    {
        return await _unitOfWork.Machine.DeleteAsync(machine);
    }

    /// <summary>
    /// Получить банкноты в кофемашине
    /// </summary>
    /// <param name="machine"></param>
    /// <returns></returns>
    public async Task<IEnumerable<Banknote>> GetBanknotesByMachineAsync(Machine machine)
    {
        return await _unitOfWork.Banknote.GetBanknotesByMachineAsync(machine);
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
            banknotes.Add(await _unitOfWork.Banknote.GetByNominalAsync(banknote.Nominal));
        }
        
        var machine = await _unitOfWork.Machine.GetByIdAsync(machineReq.Id);
        
        await _unitOfWork.Banknote.AddBanknotesToMachineAsync(banknotes, machine);
        await _unitOfWork.Machine.UpdateBalanceAsync(machine);

        var machineResp = await _unitOfWork.Machine.GetByIdAsync(machine.Id);
        
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
            banknotes.Add(await _unitOfWork.Banknote.GetByNominalAsync(banknote.Nominal));
        }
        
        var machine = await _unitOfWork.Machine.GetByIdAsync(machineReq.Id);
        
        await _unitOfWork.Banknote.SubtractBanknotesFromMachineAsync(banknotes, machine);
        await _unitOfWork.Machine.UpdateBalanceAsync(machine);
        
        var machineResp = await _unitOfWork.Machine.GetByIdAsync(machine.Id);
        
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
        var nCoffee = await _unitOfWork.Coffee.GetByNameAsync(coffee.Name);
        var nMachine = await _unitOfWork.Machine.GetBySerialNumberAsync(machine.SerialNumber);
        
        return await _unitOfWork.Machine.AddCoffeeInMachineAsync(nCoffee, nMachine);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="coffee"></param>
    /// <param name="machine"></param>
    /// <returns></returns>
    public async Task<Machine> DeleteCoffeeFromMachineAsync(Coffee coffee, Machine machine)
    {
        var nCoffee = await _unitOfWork.Coffee.GetByNameAsync(coffee.Name);
        var nMachine = await _unitOfWork.Machine.GetBySerialNumberAsync(machine.SerialNumber);
        
        return await _unitOfWork.Machine.DeleteCoffeeFromMachineAsync(nCoffee, nMachine);
    }

    /// <summary>
    /// Получить список кофе
    /// </summary>
    /// <returns></returns>
    public async Task<IEnumerable<Coffee>> GetAllCoffeesAsync()
    {
        return await _unitOfWork.Coffee.GetAllAsync();
    }

    /// <summary>
    /// Создать новый кофе
    /// </summary>
    /// <param name="coffee"></param>
    /// <returns></returns>
    public async Task<Coffee> CreateNewCoffeeAsync(Coffee coffee)
    {
        return await _unitOfWork.Coffee.AddAsync(coffee);
    }

    /// <summary>
    /// Удалить кофе
    /// </summary>
    /// <param name="coffee"></param>
    /// <returns></returns>
    public async Task<bool> DeleteCoffeeAsync(Coffee coffee)
    {
        return await _unitOfWork.Coffee.DeleteAsync(coffee);
    }

    /// <summary>
    /// Обновить кофе
    /// </summary>
    /// <param name="coffee"></param>
    /// <returns></returns>
    public async Task<Coffee> UpdateCoffeeAsync(Coffee coffee)
    {
        return await _unitOfWork.Coffee.UpdateAsync(coffee);
    }

    /// <summary>
    /// Получить список всех заказов
    /// </summary>
    /// <returns></returns>
    public async Task<IEnumerable<Order>> GetAllOrdersAsync()
    {
        return await _unitOfWork.Order.GetAllAsync();
    }
}