using CoffeeMachine.Domain.Models;
using CoffeeMachine.Infrastructure.Interfaces.IRepositories;
using CoffeeMachine.Infrastructure.Interfaces.IServices;

namespace CoffeeMachine.Persistence.Services;

public class BuyService : IBuyService
{
    private readonly IBanknoteRepository _banknoteRepository;
    public BuyService(IBanknoteRepository banknoteRepository)
    {
        _banknoteRepository = banknoteRepository;
    }
    
    public async Task<Purchase> BuyAsync(Purchase purchase)
    {
        List<Banknote> banknotes = purchase.Transactions.Select(t => t.Banknote).ToList();
        var priceCoffee = purchase.Coffee.Price;
        List<Banknote> delivery = CalculationDeliveryBanknotes(banknotes, priceCoffee);

        CheckingDelivery(delivery, purchase.Machine);
        
        return purchase;
    }

    private bool CheckingDelivery(List<Banknote> delivery, Machine machine)
    {
        var banknotesInMachine = _banknoteRepository.GetBanknotesByMachineAsync(machine).Result.ToList();

        int counter = 0;

        
        foreach (var bim in banknotesInMachine)
        {
            foreach (var deliv in delivery)
            {
                if (bim == deliv)
                {
                    counter++;
                    break;
                }
            }
        }

        if (counter == delivery.Count)
        {
            return true;
        }

        return false;
    }

    public List<Banknote> CalculationDeliveryBanknotes(List<Banknote> banknotesPay, int price)
    {
        List<Banknote> banknotes = _banknoteRepository.GetAllAsync().Result.ToList();
        List<Banknote> deliveryBanknotes = new List<Banknote>();

        int sumBanknotes = 0;
        foreach (var banknote in banknotesPay)
        {
            sumBanknotes += banknote.Nominal;
        }

        price = sumBanknotes - price;

        List<int> priceDischarge = SplitPrice(price);

        foreach (var del in priceDischarge)
        {
            int temp = del;
            foreach (var banknote in banknotes)
            {
                if (banknote.Nominal <= temp && 
                    temp.ToString().Length == banknote.Nominal.ToString().Length)
                {
                    deliveryBanknotes.Add(banknote);
                    temp -= banknote.Nominal;
                }
            }
        }

        return deliveryBanknotes;
    }
    
    private static List<int> SplitPrice(int price)
    {
        List<int> split = new List<int>();
        int multiplier = 1;
        
        while (price > 0)
        {
            int digit = price % 10;
            digit *= multiplier;
            split.Add(digit);
            price /= 10;
            multiplier *= 10;
        }

        split.Reverse();
        
        return split;
    }
}