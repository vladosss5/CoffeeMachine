using CoffeeMachine.Domain.Models;
using CoffeeMachine.Infrastructure.Interfaces.IRepositories;
using CoffeeMachine.Infrastructure.Interfaces.IServices;

namespace CoffeeMachine.Persistence.Services;

public class BuyService : IBuyService
{
    private readonly IPurchaseService _purchaseService;
    private readonly IBanknoteRepository _banknoteRepository;
    public BuyService(IBanknoteRepository banknoteRepository)
    {
        _banknoteRepository = banknoteRepository;
    }
    
    public async Task<Purchase> BuyAsync(Purchase purchase)
    {
        return purchase;
    }

    public async Task<IEnumerable<Banknote>> CalculationDeliveryBanknotes(List<Banknote> banknotesPay, int price)
    {
        List<Banknote> banknotes = _banknoteRepository.GetAllAsync().Result.ToList();
        List<Banknote> deliveryBanknotes = new List<Banknote>();

        int delivery = 0;
        
        foreach (var banknote in banknotesPay)
        {
            delivery -= banknote.Par;
        }

        List<int> priceDischarge = SplitPrice(delivery);

        foreach (var del in priceDischarge)
        {
            int temp = del;
            foreach (var banknote in banknotes)
            {
                if (banknote.Par <= temp && 
                    temp.ToString().Length == banknote.Par.ToString().Length)
                {
                    deliveryBanknotes.Add(banknote);
                    temp -= banknote.Par;
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