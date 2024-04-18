using CoffeeMachine.Application.Exceptions;
using CoffeeMachine.Application.Interfaces.IRepositories;
using CoffeeMachine.Application.Interfaces.IServices;
using CoffeeMachine.Core.Models;

namespace CoffeeMachine.Persistence.Services;

public class OrderService : IOrderService
{
    private readonly IBanknoteRepository _banknoteRepository;
    private readonly IMachineRepository _machineRepository;
    private readonly IOrderRepository _orderRepository;
    private readonly ITransactionRepository _transactionRepository;
    private readonly ICoffeeRepository _coffeeRepository;
    
    public OrderService(IBanknoteRepository banknoteRepository, IMachineRepository machineRepository, 
        IOrderRepository orderRepository, ITransactionRepository transactionRepository, ICoffeeRepository coffeeRepository)
    {
        _banknoteRepository = banknoteRepository;
        _machineRepository = machineRepository;
        _orderRepository = orderRepository;
        _transactionRepository = transactionRepository;
        _coffeeRepository = coffeeRepository;
    }
    
    /// <summary>
    /// Метод для создания заказа
    /// </summary>
    /// <param name="orderRequest"></param>
    /// <returns></returns>
    public async Task<Order> CreateOrderAsync(Order orderRequest)
    {
        var order = new Order()
        {
            Machine = await _machineRepository.GetBySerialNumberAsync(orderRequest.Machine.SerialNumber),
            Coffee = await _coffeeRepository.GetByNameAsync(orderRequest.Coffee.Name),
            Status = "Принято",
        };

        if (!await _machineRepository.CheckCoffeeInMachineAsync(order.Machine, order.Coffee))
            throw new Exception("Данная машина не умеет готовить выбранный кофе");
        
        
        order = await _orderRepository.AddAsync(order);
        
        foreach (var transaction in orderRequest.Transactions)
        {
            transaction.Order = order;
            await _transactionRepository.AddAsync(transaction);
        }
        
        var banknotesPay = order.Transactions.Select(t => t.Banknote).ToList();
        await _banknoteRepository.AddBanknotesToMachineAsync(banknotesPay, order.Machine);
        
        order.Status = "Внесены деньги";
        await _orderRepository.UpdateAsync(order);
        
        var delivery = new List<Banknote>();
        
        try
        {
            delivery = await CalculateChange(order.Coffee.Price, order.Transactions, order.Machine);
        }
        catch (Exception e)
        {
            return await ErrorChange(order, banknotesPay);
        }
        
        await _banknoteRepository.SubtractBanknotesFromMachineAsync(delivery, order.Machine);
        
        foreach (var banknote in delivery)
        {
            var newTransaction = new Transaction()
            {
                IsPayment = false,
                Banknote = banknote,
                Order = order
            };
            await _transactionRepository.AddAsync(newTransaction);
        }
        
        order.Transactions = await _transactionRepository.GetByOrderAsync(order);
        order.Status = "Готово";
        order = await _orderRepository.UpdateAsync(order);
        order.Transactions = order.Transactions.Where(t => t.IsPayment == false).ToList();
        return order;
    }
    
    
    /// <summary>
    /// Метод для расчета сдачи
    /// </summary>
    /// <param name="coffeePrice"></param>
    /// <param name="transactions"></param>
    /// <param name="machine"></param>
    /// <returns></returns>
    private async Task<List<Banknote>> CalculateChange(int coffeePrice, IEnumerable<Transaction> transactions, Machine machine)
    {
        var banknotes = await _banknoteRepository
            .GetBanknotesByMachineAsync(machine);
        
        var change = new List<Banknote>();
        
        var moneyInserted = transactions.Sum(t => t.Banknote.Nominal);
        var remainingChange = moneyInserted - coffeePrice;

        if (remainingChange <= 0)
            return change;

        foreach (Banknote banknote in banknotes)
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
    /// Метод возвращающий ошибку при сдаче
    /// </summary>
    /// <param name="order"></param>
    /// <param name="banknotes"></param>
    /// <returns></returns>
    private async Task<Order> ErrorChange(Order order, List<Banknote> banknotes)
    {
        await _banknoteRepository.SubtractBanknotesFromMachineAsync(banknotes, order.Machine);
        order.Status = "Нет сдачи";
        await _orderRepository.UpdateAsync(order);
        order.Transactions = order.Transactions.Where(t => t.IsPayment == false).ToList();
        
        foreach (var banknote in banknotes)
        {
            var newTransaction = new Transaction()
            {
                IsPayment = false,
                Banknote = banknote,
                Order = order
            };
            await _transactionRepository.AddAsync(newTransaction);
        }
        
        return order;
    }
}