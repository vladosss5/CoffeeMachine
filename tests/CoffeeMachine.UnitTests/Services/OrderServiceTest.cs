using CoffeeMachine.Application.Interfaces;
using CoffeeMachine.Application.Interfaces.IServices;
using CoffeeMachine.Core.Models;
using CoffeeMachine.Persistence.Services;
using Moq;

namespace CoffeeMachine.UnitTests.Services;

[TestFixture]
public class OrderServiceTest
{
    public Machine machine;
    public Coffee coffee;
    public Order order;
    public List<Transaction> transactions;
    public List<Banknote> banknotes;
    public List<BanknoteToMachine> banknotesToMachines;
    // public 
    
    public OrderServiceTest()
    {
        machine = new Machine{Id = 1, SerialNumber = "11", Description = "wdw"};
        coffee = new Coffee{Id = 1, Name = "Cappuccino", Price = 836};
        banknotes = new List<Banknote>
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
    }
    
    [Test]
    public async Task TestCreateOrderAsync()
    {
        var moqUnitOfWork = new Mock<IUnitOfWork>();
        var moqAdminService = new Mock<IAdminService>();
        
        moqUnitOfWork.Setup(x => x.Machine.GetByIdAsync(It.IsAny<int>()))
            .ReturnsAsync(machine);
        
        moqUnitOfWork.Setup(x => x.Coffee.GetByNameAsync(It.IsAny<string>()))
            .ReturnsAsync(coffee);
        
        moqUnitOfWork.Setup(x => x.Machine.CheckCoffeeInMachineAsync(It.IsAny<Machine>(), It.IsAny<Coffee>()))
            .ReturnsAsync(true);
        
        moqUnitOfWork.Setup(x => x.Order.AddAsync(It.IsAny<Order>()))
            .ReturnsAsync(new Order
            {
                Id = 1, 
                Coffee = coffee,
                Machine = machine,
                DateTimeCreate = DateTime.UtcNow,
                Status = "Принято",
                Transactions = new List<Transaction>
                {
                    new Transaction{Id = 1, Banknote = banknotes[3], Order = new Order{Id = 1}, IsPayment = true},
                    new Transaction{Id = 2, Banknote = banknotes[3], Order = new Order{Id = 1}, IsPayment = true},
                    new Transaction{Id = 3, Banknote = banknotes[4], Order = new Order{Id = 1}, IsPayment = false},
                    new Transaction{Id = 3, Banknote = banknotes[5], Order = new Order{Id = 1}, IsPayment = false},
                    new Transaction{Id = 3, Banknote = banknotes[6], Order = new Order{Id = 1}, IsPayment = false},
                    new Transaction{Id = 3, Banknote = banknotes[8], Order = new Order{Id = 1}, IsPayment = false},
                    new Transaction{Id = 3, Banknote = banknotes[8], Order = new Order{Id = 1}, IsPayment = false}
                }
            });

        moqUnitOfWork.Setup(x => x.Banknote.GetByNominalAsync(It.IsAny<int>()))
            .ReturnsAsync(banknotes[9]);

        moqUnitOfWork.Setup(x => x.Transaction.AddAsync(It.IsAny<Transaction>()));
        
        
    }
}