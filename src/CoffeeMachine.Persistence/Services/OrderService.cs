using CoffeeMachine.Domain.Models;
using CoffeeMachine.Infrastructure.Interfaces.IRepositories;
using CoffeeMachine.Infrastructure.Interfaces.IServices;

namespace CoffeeMachine.Persistence.Services;

public class OrderService : IOrderService
{
    private readonly IBanknoteRepository _banknoteRepository;
    private readonly IMachineRepository _machineRepository;
    private readonly IOrderRepository _orderRepository;
    private readonly ITransactionRepository _transactionRepository;
    public OrderService(IBanknoteRepository banknoteRepository, IMachineRepository machineRepository, 
        IOrderRepository orderRepository, ITransactionRepository transactionRepository)
    {
        _banknoteRepository = banknoteRepository;
        _machineRepository = machineRepository;
        _orderRepository = orderRepository;
        _transactionRepository = transactionRepository;
    }
    
    public async Task<Order> CreateOrderAsync(Order order)
    {
        // await _banknoteRepository.AddBanknotesToMachineAsync(order.Transactions, order.Machine); //Надо фиксить
        
        foreach (var transaction in order.Transactions) // Добавляем транзакции оплаты
        {
            transaction.Type = true;
            _transactionRepository.AddAsync(transaction);
        }
        
        List<Banknote> deliveredBanknotes = CalculateChange(order.Coffee.Price, order.Transactions, order.Machine);
        await _banknoteRepository.SubtractBanknotesFromMachineAsync(deliveredBanknotes, order.Machine);
        
        await _machineRepository.UpdateBalanceAsync(order.Machine);

        foreach (var transaction in order.Transactions) //Добавляем транзакции сдачи
        {
            transaction.Type = false;
            await _transactionRepository.AddAsync(transaction);
        }
        
        await _orderRepository.AddAsync(order);
        
        return order;
    }
    
    
    public List<Banknote> CalculateChange(int coffeePrice, IEnumerable<Transaction> transactions, Machine machine)
    {
        List<Banknote> banknotes = _banknoteRepository.GetBanknotesByMachineAsync(machine).Result.ToList();
        List<Banknote> change = new List<Banknote>();
        int moneyInserted = transactions.Where(t => t.Type == true).Sum(t => t.Banknote.Nominal);
        int remainingChange = moneyInserted - coffeePrice;

        foreach (Banknote banknote in banknotes)
        {
            int count = remainingChange / banknote.Nominal;
            for (int i = 0; i < count; i++)
            {
                change.Add(banknote);
                remainingChange -= banknote.Nominal;
            }
        }

        return change;
    }
}