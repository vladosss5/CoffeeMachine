using CoffeeMachine.Application.Exceptions;
using CoffeeMachine.Application.Interfaces;
using CoffeeMachine.Application.Interfaces.IRepositories;
using CoffeeMachine.Application.Interfaces.IServices;
using CoffeeMachine.Core.Models;

namespace CoffeeMachine.Persistence.Services;

/// <summary>
/// Сервис заказов.
/// </summary>
public class OrderService : IOrderService
{
    /// <summary>
    /// Unit Of Work.
    /// </summary>
    private readonly IUnitOfWork _unitOfWork;
    
    /// <summary>
    /// Сервис администратора.
    /// </summary>
    private readonly IAdminService _adminService;

    /// <summary>
    /// Конструктор класса.
    /// </summary>
    /// <param name="unitOfWork">Unit Of Work.</param>
    /// <param name="adminService">Сервис администратора.</param>
    public OrderService(IUnitOfWork unitOfWork, IAdminService adminService)
    {
        _unitOfWork = unitOfWork;
        _adminService = adminService;
    }
    
    /// <summary>
    /// Метод для создания заказа.
    /// </summary>
    /// <param name="orderRequest">Запрашиваемый заказ.</param>
    /// <returns>Заказ.</returns>
    public async Task<Order> CreateOrderAsync(Order orderRequest)
    {
        var order = new Order()
        {
            Machine = await _unitOfWork.Machine.GetByIdAsync(orderRequest.Machine.Id),
            Coffee = await _unitOfWork.Coffee.GetByNameAsync(orderRequest.Coffee.Name),
            DateTimeCreate = DateTime.UtcNow,
            Status = "Принято",
        };

        if (!await _unitOfWork.Machine.CheckCoffeeInMachineAsync(order.Machine, order.Coffee))
            throw new Exception("Данная машина не умеет готовить выбранный кофе");
        
        order = await _unitOfWork.Order.AddAsync(order);
        
        foreach (var transaction in orderRequest.Transactions)
        {
            transaction.Banknote = await _unitOfWork.Banknote.GetByNominalAsync(transaction.Banknote.Nominal);
            transaction.Order = order;
            await _unitOfWork.Transaction.AddAsync(transaction);
        }
        
        var banknotesPay = order.Transactions.Select(t => t.Banknote).ToList();
        await _adminService.AddBanknotesToMachineAsync(banknotesPay, order.Machine.Id);
        
        order.Status = "Внесены деньги";
        await _unitOfWork.Order.UpdateAsync(order);
        
        var delivery = new List<Banknote>();
        
        try
        {
            delivery = await CalculateChange(order.Coffee.Price, order.Transactions, order.Machine);
        }
        catch (Exception e)
        {
            return await ErrorChange(order, banknotesPay);
        }
        
        await _adminService.SubtractBanknotesFromMachineAsync(delivery, order.Machine.Id);
        
        foreach (var banknote in delivery)
        {
            var newTransaction = new Transaction()
            {
                IsPayment = false,
                Banknote = banknote,
                Order = order
            };
            await _unitOfWork.Transaction.AddAsync(newTransaction);
        }
        
        order.Status = "Готово";
        order = await _unitOfWork.Order.UpdateAsync(order);
        order.Transactions = order.Transactions.Where(t => t.IsPayment == false).ToList();
        await _adminService.UpdateBalanceAsync(order.Machine.Id);
        return order;
    }
    
    
    /// <summary>
    /// Расчет сдачи.
    /// </summary>
    /// <param name="coffeePrice">Цена кофе.</param>
    /// <param name="transactions">Список транзакций.</param>
    /// <param name="machine">Кофемашина.</param>
    /// <returns>Список банкнот для сдачи.</returns>
    private async Task<List<Banknote>> CalculateChange(int coffeePrice, IEnumerable<Transaction> transactions, Machine machine)
    {
        var banknotesToMachine = await _unitOfWork.Banknote
            .GetBanknotesByMachineAsync(machine);
        
        var change = new List<Banknote>();
        
        var moneyInserted = transactions.Sum(t => t.Banknote.Nominal);
        var remainingChange = moneyInserted - coffeePrice;

        if (remainingChange <= 0)
            return change;

        foreach (var banknote in banknotesToMachine.Where(x => x.CountBanknote > 0).Select(bm => bm.Banknote))
        {
            int count = remainingChange / banknote.Nominal;
            for (int i = 0; i < count; i++)
            {
                change.Add(banknote);
                remainingChange -= banknote.Nominal;
            }
            
            if (remainingChange == 0)
                break;
        }

        if (remainingChange > 0)
            throw new Exception();

        return change;
    }

    /// <summary>
    /// Ошибка при сдаче.
    /// </summary>
    /// <param name="order">Заказ.</param>
    /// <param name="banknotes">Список банкнот.</param>
    /// <returns>Заказ.</returns>
    private async Task<Order> ErrorChange(Order order, List<Banknote> banknotes)
    {
        await _adminService.SubtractBanknotesFromMachineAsync(banknotes, order.Machine.Id);
        order.Status = "Нет сдачи";
        await _unitOfWork.Order.UpdateAsync(order);
        order.Transactions = order.Transactions.Where(t => t.IsPayment == false).ToList();
        
        foreach (var banknote in banknotes)
        {
            var newTransaction = new Transaction()
            {
                IsPayment = false,
                Banknote = banknote,
                Order = order
            };
            await _unitOfWork.Transaction.AddAsync(newTransaction);
        }
        
        await _adminService.UpdateBalanceAsync(order.Machine.Id);
        
        return order;
    }
}