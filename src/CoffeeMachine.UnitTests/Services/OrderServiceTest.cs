using CoffeeMachine.Application.Interfaces;
using CoffeeMachine.Application.Interfaces.IServices;
using CoffeeMachine.Core.Models;
using CoffeeMachine.Persistence.Services;
using Moq;
using NUnit.Framework.Legacy;

namespace CoffeeMachine.UnitTests.Services;

[TestFixture]
public class OrderServiceTest
{
    private Coffee _coffee;
    private Machine _machine;
    private List<Banknote> _banknotes;
    private List<BanknoteToMachine> _banknotesToMachines;
    private List<CoffeeToMachine> _coffeeToMachines;
    private List<Transaction> _transactions;
    private Order _order;

    public OrderServiceTest()
    {
        FillingData();
    }
    
    [Test]
    public async Task TestCreateOrderAsync()
    {
        var moqUnitOfWork = new Mock<IUnitOfWork>();
        var moqAdminService = new Mock<IAdminService>();

        moqUnitOfWork.Setup(x => x.Machine.GetByIdAsync(It.IsAny<long>())).ReturnsAsync(_machine);
        moqUnitOfWork.Setup(x => x.Coffee.GetByNameAsync(It.IsAny<string>())).ReturnsAsync(_coffee);
        moqUnitOfWork.Setup(x => x.Machine.CheckCoffeeInMachineAsync(It.IsAny<Machine>(), It.IsAny<Coffee>()))
            .ReturnsAsync(true);
        moqUnitOfWork.Setup(x => x.Order.AddAsync(It.IsAny<Order>())).ReturnsAsync(_order);
        moqUnitOfWork.Setup(x => x.Transaction.AddAsync(It.IsAny<Transaction>()))
            .ReturnsAsync(new Transaction{Id = 2, Banknote = _banknotes[2], Order = _order, IsPayment = true});
        moqUnitOfWork.Setup(x => x.Banknote.GetByNominalAsync(It.IsAny<int>())).ReturnsAsync(_banknotes[2]);
        moqUnitOfWork.Setup(x => x.Banknote.GetBanknotesByMachineAsync(It.IsAny<Machine>()))
            .ReturnsAsync(_banknotesToMachines.Where(x => x.Machine == _machine));
        moqUnitOfWork.Setup(x => x.Order.UpdateAsync(It.IsAny<Order>()))
            .ReturnsAsync(new Order()
            {
                Id = 1, 
                Machine = _machine, 
                Coffee = _coffee, 
                Status = "Готово",
                DateTimeCreate = DateTime.UtcNow,
                Transactions = new List<Transaction>
                {
                    new Transaction{Id = 2, Banknote = _banknotes[2], Order = _order, IsPayment = true}
                }
            });
        
        var orderService = new OrderService(moqUnitOfWork.Object, moqAdminService.Object);
        
        var result = await orderService.CreateOrderAsync(new Order
        {
            Coffee = _coffee,
            Machine = _machine,
            Transactions = _transactions.Where(x => x.IsPayment == true && x.Order == _order)
        });
        
        var delivery = _transactions
            .Where(x => x.IsPayment == false && x.Order == _order)
            .Select(x => x.Banknote)
            .ToList();
        
        ClassicAssert.AreEqual(result.Status, "Готово");

        for (int i = 0; i < result.Transactions.Count(); i++)
        {
            ClassicAssert.AreEqual(result.Transactions.ToList()[i].Banknote.Nominal, delivery[i].Nominal);
        }
        
        ClassicAssert.AreEqual(result.Coffee, _coffee);
    }
    
    private void FillingData()
    {
        _coffee = new Coffee
        {
            Id = 1,
            Name = "Cappuccino",
            Price = 836
        };

        _machine = new Machine
        {
            Id = 1,
            SerialNumber = "11",
            Description = "wdw",
            Balance = 0
        };

        _banknotes = new List<Banknote>
        {
            new Banknote{Id = 1, Nominal = 5000},
            new Banknote{Id = 2, Nominal = 2000},
            new Banknote{Id = 3, Nominal = 1000},
            new Banknote{Id = 4, Nominal = 500},
            new Banknote{Id = 5, Nominal = 100},
            new Banknote{Id = 6, Nominal = 50},
            new Banknote{Id = 7, Nominal = 10},
            new Banknote{Id = 8, Nominal = 5},
            new Banknote{Id = 9, Nominal = 2},
            new Banknote{Id = 10, Nominal = 1}
        };

        _banknotesToMachines = new List<BanknoteToMachine>
        {
            new BanknoteToMachine{Id = 1,  Machine = _machine, Banknote = _banknotes[0], CountBanknote = 10},
            new BanknoteToMachine{Id = 2,  Machine = _machine, Banknote = _banknotes[1], CountBanknote = 10},
            new BanknoteToMachine{Id = 3,  Machine = _machine, Banknote = _banknotes[2], CountBanknote = 10},
            new BanknoteToMachine{Id = 4,  Machine = _machine, Banknote = _banknotes[3], CountBanknote = 10},
            new BanknoteToMachine{Id = 5,  Machine = _machine, Banknote = _banknotes[4], CountBanknote = 10},
            new BanknoteToMachine{Id = 6,  Machine = _machine, Banknote = _banknotes[5], CountBanknote = 10},
            new BanknoteToMachine{Id = 7,  Machine = _machine, Banknote = _banknotes[6], CountBanknote = 10},
            new BanknoteToMachine{Id = 8,  Machine = _machine, Banknote = _banknotes[7], CountBanknote = 10},
            new BanknoteToMachine{Id = 9,  Machine = _machine, Banknote = _banknotes[8], CountBanknote = 10},
            new BanknoteToMachine{Id = 10, Machine = _machine, Banknote = _banknotes[9], CountBanknote = 10}
        };

        _coffeeToMachines = new List<CoffeeToMachine>
        {
            new CoffeeToMachine{Id = 1,  Machine = _machine, Coffee = _coffee}
        };

        _order = new Order
        {
            Id = 1, Machine = _machine, Coffee = _coffee, DateTimeCreate = DateTime.UtcNow, Status = "Принято"
        };

        _transactions = new List<Transaction>
        {
            new Transaction{Id = 2, Banknote = _banknotes[2], Order = _order, IsPayment = true},
            new Transaction{Id = 3, Banknote = _banknotes[4], Order = _order, IsPayment = false},
            new Transaction{Id = 4, Banknote = _banknotes[5], Order = _order, IsPayment = false},
            new Transaction{Id = 5, Banknote = _banknotes[6], Order = _order, IsPayment = false},
            new Transaction{Id = 6, Banknote = _banknotes[8], Order = _order, IsPayment = false},
            new Transaction{Id = 7, Banknote = _banknotes[8], Order = _order, IsPayment = false}
        };
    }
}