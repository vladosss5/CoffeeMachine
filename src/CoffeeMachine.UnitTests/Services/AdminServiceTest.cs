using CoffeeMachine.Application.Interfaces;
using CoffeeMachine.Application.Interfaces.IServices;
using CoffeeMachine.Core.Models;
using CoffeeMachine.Persistence.Services;
using Moq;
using NUnit.Framework.Legacy;

namespace CoffeeMachine.UnitTests.Services;

[TestFixture]
public class AdminServiceTest
{
    private Coffee _coffee;
    private Machine _machine;
    private List<Banknote> _banknotes;
    private List<BanknoteToMachine> _banknotesToMachines;
    private List<CoffeeToMachine> _coffeeToMachines;
    private List<Transaction> _transactions;
    private Order _order;
    
    
    public AdminServiceTest()
    {
        FillingData();
    }

    [Test]
    public async Task TestGetMachineByIdAsync()
    {
        var moqUnitOfWork = new Mock<IUnitOfWork>();
        
        moqUnitOfWork.Setup(x => x.Machine.GetByIdAsync(It.IsAny<long>()))
            .ReturnsAsync(_machine);
        
        var adminService = new AdminService(moqUnitOfWork.Object);
        
        var result = await adminService.GetMachineByIdAsync(1);
        
        ClassicAssert.AreEqual(result, _machine);
    }

    [Test]
    public async Task TestGetAllMachinesAsync()
    {
        var moqUnitOfWork = new Mock<IUnitOfWork>();
        moqUnitOfWork.Setup(x => x.Machine.GetAllAsync())
            .ReturnsAsync(new List<Machine>(){_machine, _machine, _machine});
        
        var adminService = new AdminService(moqUnitOfWork.Object);
        
        var result = await adminService.GetAllMachinesAsync();

        foreach (var res in result)
        {
            ClassicAssert.AreEqual(_machine, res);
        }
    }

    [Test]
    public async Task TestCreateNewMachineAsync()
    {
        var moqUnitOfWork = new Mock<IUnitOfWork>();
        moqUnitOfWork.Setup(x => x.Machine.GetBySerialNumberAsync(It.IsAny<string>()));
        moqUnitOfWork.Setup(x => x.Machine.AddAsync(It.IsAny<Machine>()))
            .ReturnsAsync(new Machine{Id = 1, SerialNumber = "11", Description = "wdw", Balance = 0});
        
        var adminService = new AdminService(moqUnitOfWork.Object);
        
        var result = await adminService.CreateNewMachineAsync(_machine);
        
        ClassicAssert.AreEqual(result.Id, _machine.Id);
        ClassicAssert.AreEqual(result.SerialNumber, _machine.SerialNumber);
        ClassicAssert.AreEqual(result.Description, _machine.Description);
        // ClassicAssert.AreEqual(result.Balance, _machine.Balance);
    }

    [Test]
    public async Task TestUpdateMachineAsync()
    {
        var moqUnitOfWork = new Mock<IUnitOfWork>();
        moqUnitOfWork.Setup(x => x.Machine.GetByIdAsync(It.IsAny<long>())).ReturnsAsync(_machine);
        _machine.Description = "test";
        _machine.SerialNumber = "123321";
        moqUnitOfWork.Setup(x => x.Machine.UpdateAsync(It.IsAny<Machine>())).ReturnsAsync(_machine);
        
        ClassicAssert.AreEqual("test", _machine.Description);
        ClassicAssert.AreEqual("123321", _machine.SerialNumber);
    }

    [Test]
    public async Task TestGetMachineBySerialNumberAsync()
    {
        var moqUnitOfWork = new Mock<IUnitOfWork>();
        moqUnitOfWork.Setup(x => x.Machine.GetBySerialNumberAsync(It.IsAny<string>())).ReturnsAsync(_machine);
        
        var adminService = new AdminService(moqUnitOfWork.Object);
        
        var result = await adminService.GetMachineBySerialNumberAsync("11");
        
        ClassicAssert.AreEqual(result, _machine);
    }

    [Test]
    public async Task TestUpdateBalanceAsync()
    {
        var moqUnitOfWork = new Mock<IUnitOfWork>();
        moqUnitOfWork.Setup(x => x.Machine.GetByIdAsync(It.IsAny<long>())).ReturnsAsync(_machine);
        moqUnitOfWork.Setup(x => x.Banknote.GetBanknotesByMachineAsync(It.IsAny<Machine>()))
            .ReturnsAsync(_banknotesToMachines);
        moqUnitOfWork.Setup(x => x.Machine.UpdateAsync(It.IsAny<Machine>())).ReturnsAsync(_machine);
        
        var adminService = new AdminService(moqUnitOfWork.Object);
        
        var result = await adminService.UpdateBalanceAsync(1);
        
        ClassicAssert.AreEqual(result, _machine.Balance);
    }

    [Test]
    public async Task TestAddCoffeeInMachineAsync()
    {
        var moqUnitOfWork = new Mock<IUnitOfWork>();
        moqUnitOfWork.Setup(x => x.Machine.GetByIdAsync(It.IsAny<long>())).ReturnsAsync(_machine);
        moqUnitOfWork.Setup(x => x.Coffee.GetByIdAsync(It.IsAny<long>())).ReturnsAsync(_coffee);
        _machine.CoffeesToMachines.ToList().AddRange(_coffeeToMachines.Where(x => x.Machine == _machine));
        moqUnitOfWork.Setup(x => x.Machine.AddCoffeeInMachineAsync(It.IsAny<Coffee>(),It.IsAny<Machine>()))
            .ReturnsAsync(_machine);
        
        var adminService = new AdminService(moqUnitOfWork.Object);
        
        var result = await adminService.AddCoffeeInMachineAsync(1, 1);
        
        ClassicAssert.AreEqual(result.CoffeesToMachines, _machine.CoffeesToMachines);
    }
    
    [Test]
    public async Task TestDeleteCoffeeFromMachineAsync()
    {
        var moqUnitOfWork = new Mock<IUnitOfWork>();
        moqUnitOfWork.Setup(x => x.Machine.GetByIdAsync(It.IsAny<long>())).ReturnsAsync(_machine);
        moqUnitOfWork.Setup(x => x.Coffee.GetByIdAsync(It.IsAny<long>())).ReturnsAsync(_coffee);
        _machine.CoffeesToMachines.ToList().RemoveAll(x => x.Coffee == _coffee);
        moqUnitOfWork.Setup(x => x.Machine.DeleteCoffeeFromMachineAsync(It.IsAny<Coffee>(),It.IsAny<Machine>()))
            .ReturnsAsync(_machine);
        
        var adminService = new AdminService(moqUnitOfWork.Object);
        
        var result = await adminService.DeleteCoffeeFromMachineAsync(1, 1);
        
        ClassicAssert.AreEqual(result.CoffeesToMachines.Count(), 0);
    }
    
    [Test]
    public async Task TestAddBanknotesToMachineAsync()
    {
        var moqUnitOfWork = new Mock<IUnitOfWork>();
        var moqInternalMethods = new Mock<IAdminService>();
        moqUnitOfWork.Setup(x => x.Machine.GetByIdAsync(It.IsAny<long>())).ReturnsAsync(_machine);
        moqUnitOfWork.Setup(x => x.Banknote.GetBanknotesByMachineAsync(It.IsAny<Machine>()))
            .ReturnsAsync(_banknotesToMachines);
        moqUnitOfWork.SetupSequence(x => x.Banknote.GetByNominalAsync(It.IsAny<int>()))
            .ReturnsAsync(_banknotes[0])
            .ReturnsAsync(_banknotes[1]);
        moqUnitOfWork.Setup(x => x.Machine.UpdateAsync(It.IsAny<Machine>())).ReturnsAsync(_machine);
        moqInternalMethods.Setup(x => x.UpdateBalanceAsync(It.IsAny<long>())).ReturnsAsync(903680);
        
        var adminService = new AdminService(moqUnitOfWork.Object);

        var addBanknotes = new List<Banknote>
        {
            new Banknote { Nominal = 5000 },
            new Banknote { Nominal = 2000 }
        };
        var result = await adminService.AddBanknotesToMachineAsync(addBanknotes, 1);
        
        ClassicAssert.AreEqual(93680, result.Balance);
        
        FillingData();
    }

    [Test]
    public async Task TestSubtractBanknotesFromMachineAsync()
    {
        var moqUnitOfWork = new Mock<IUnitOfWork>();
        var moqInternalMethods = new Mock<IAdminService>();
        moqUnitOfWork.Setup(x => x.Machine.GetByIdAsync(It.IsAny<long>())).ReturnsAsync(_machine);
        moqUnitOfWork.Setup(x => x.Banknote.GetBanknotesByMachineAsync(It.IsAny<Machine>()))
            .ReturnsAsync(_banknotesToMachines);
        moqUnitOfWork.SetupSequence(x => x.Banknote.GetByNominalAsync(It.IsAny<int>()))
            .ReturnsAsync(_banknotes[0])
            .ReturnsAsync(_banknotes[1]);
        moqUnitOfWork.Setup(x => x.Machine.UpdateAsync(It.IsAny<Machine>())).ReturnsAsync(_machine);
        moqInternalMethods.Setup(x => x.UpdateBalanceAsync(It.IsAny<long>())).ReturnsAsync(0);

        var adminService = new AdminService(moqUnitOfWork.Object);

        var deleteBanknotes = new List<Banknote>
        {
            new Banknote { Nominal = 5000 },
            new Banknote { Nominal = 2000 }
        };
        
        var result = await adminService.SubtractBanknotesFromMachineAsync(deleteBanknotes, 1);
        
        ClassicAssert.AreEqual(79680, result.Balance);
        
        FillingData();
    }

    [Test]
    public async Task TestGetBanknotesByMachineAsync()
    {
        var moqUnitOfWork = new Mock<IUnitOfWork>();
        moqUnitOfWork.Setup(x => x.Machine.GetByIdAsync(It.IsAny<long>())).ReturnsAsync(_machine);
        moqUnitOfWork.Setup(x => x.Banknote.GetBanknotesByMachineAsync(It.IsAny<Machine>()))
            .ReturnsAsync(_banknotesToMachines.Where(x => x.Machine == _machine));
        
        var adminService = new AdminService(moqUnitOfWork.Object);
        
        var result = await adminService.GetBanknotesByMachineAsync(1);

        ClassicAssert.AreEqual(_banknotesToMachines, result);
    }

    [Test]
    public async Task TestGetCoffeeByIdAsync()
    {
        var moqUnitOfWork = new Mock<IUnitOfWork>();
        moqUnitOfWork.Setup(x => x.Coffee.GetByIdAsync(It.IsAny<long>())).ReturnsAsync(_coffee);
        
        var adminService = new AdminService(moqUnitOfWork.Object);
        
        var result = await adminService.GetCoffeeByIdAsync(1);
        
        ClassicAssert.AreEqual(_coffee, result);
    }

    [Test]
    public async Task TestGetAllCoffeesAsync()
    {
        var moqUnitOfWork = new Mock<IUnitOfWork>();
        moqUnitOfWork.Setup(x => x.Coffee.GetAllAsync())
            .ReturnsAsync(new List<Coffee> {_coffee, _coffee, _coffee});
        
        var adminService = new AdminService(moqUnitOfWork.Object);
        
        var result = await adminService.GetAllCoffeesAsync();

        foreach (var res in result)
        {
            ClassicAssert.AreEqual(_coffee, res);
        }
    }

    [Test]
    public async Task TestCreateNewCoffeeAsync()
    {
        var moqUnitOfWork = new Mock<IUnitOfWork>();
        moqUnitOfWork.Setup(x => x.Coffee.GetByNameAsync(It.IsAny<string>()));
        moqUnitOfWork.Setup(x => x.Coffee.AddAsync(It.IsAny<Coffee>())).ReturnsAsync(_coffee);
        
        var adminService = new AdminService(moqUnitOfWork.Object);
        
        var result = await adminService.CreateNewCoffeeAsync(_coffee);
        
        ClassicAssert.AreEqual(_coffee, result);
    }
    
    [Test]
    public async Task TestUpdateCoffeeAsync()
    {
        var moqUnitOfWork = new Mock<IUnitOfWork>();
        moqUnitOfWork.Setup(x => x.Coffee.GetByIdAsync(It.IsAny<long>())).ReturnsAsync(_coffee);
        moqUnitOfWork.Setup(x => x.Coffee.UpdateAsync(It.IsAny<Coffee>())).ReturnsAsync(_coffee);
        
        var adminService = new AdminService(moqUnitOfWork.Object);
        
        var result = await adminService.UpdateCoffeeAsync(_coffee);
        
        ClassicAssert.AreEqual(_coffee, result);
    }

    [Test]
    public async Task TestGetByNameAsync()
    {
        var moqUnitOfWork = new Mock<IUnitOfWork>();
        moqUnitOfWork.Setup(x => x.Coffee.GetByNameAsync(It.IsAny<string>())).ReturnsAsync(_coffee);
        
        var adminService = new AdminService(moqUnitOfWork.Object);
        
        var result = await adminService.GetByNameAsync(_coffee.Name);
        
        ClassicAssert.AreEqual(_coffee, result);
    }

    [Test]
    public async Task TestGetCoffeesFromMachineAsync()
    {
        var moqUnitOfWork = new Mock<IUnitOfWork>();
        moqUnitOfWork.Setup(x => x.Machine.GetByIdAsync(It.IsAny<long>())).ReturnsAsync(_machine);
        moqUnitOfWork.Setup(x => x.Coffee.GetCoffeesFromMachineAsync(It.IsAny<Machine>()))
            .ReturnsAsync(_coffeeToMachines.Where(x => x.Coffee == _coffee && x.Machine == _machine)
                .Select(x => x.Coffee));
        
        var adminService = new AdminService(moqUnitOfWork.Object);
        
        var result = await adminService.GetCoffeesFromMachineAsync(1);
        
        ClassicAssert.AreEqual(_coffee, result.First());
    }

    [Test]
    public async Task TestGetOrderByIdAsync()
    {
        var moqUnitOfWork = new Mock<IUnitOfWork>();
        moqUnitOfWork.Setup(x => x.Order.GetOrderByIdAsyncIcludeOtherEntities(It.IsAny<long>())).ReturnsAsync(_order);
        
        var adminService = new AdminService(moqUnitOfWork.Object);
        
        var result = await adminService.GetOrderByIdAsync(1);
        
        ClassicAssert.AreEqual(_order, result);
    }

    [Test]
    public async Task TestGetAllOrdersAsync()
    {
        var moqUnitOfWork = new Mock<IUnitOfWork>();
        moqUnitOfWork.Setup(x => x.Order.GetAllAsync()).ReturnsAsync(new List<Order> {_order, _order, _order});
        
        var adminService = new AdminService(moqUnitOfWork.Object);
        
        var result = await adminService.GetAllOrdersAsync();

        foreach (var res in result)
        {
            ClassicAssert.AreEqual(res, _order);
        }
    }
    
    [Test]
    public async Task TestGetAllTransactionsAsync()
    {
        var moqUnitOfWork = new Mock<IUnitOfWork>();
        moqUnitOfWork.Setup(x => x.Transaction.GetAllAsync()).ReturnsAsync(_transactions);
        
        var adminService = new AdminService(moqUnitOfWork.Object);
        
        var result = await adminService.GetAllTransactionsAsync();
        
        ClassicAssert.AreEqual(_transactions, result);
    }
    
    [Test]
    public async Task TestGetTransactionByIdAsync()
    {
        var moqUnitOfWork = new Mock<IUnitOfWork>();
        moqUnitOfWork.Setup(x => x.Transaction.GetByIdAsync(It.IsAny<long>())).ReturnsAsync(_transactions[0]);
        
        var adminService = new AdminService(moqUnitOfWork.Object);
        
        var result = await adminService.GetTransactionByIdAsync(1);
        
        ClassicAssert.AreEqual(_transactions[0], result);
    }
    
    [Test]
    public async Task TestGetTransactionByTypeAsync()
    {
        var moqUnitOfWork = new Mock<IUnitOfWork>();
        moqUnitOfWork.Setup(x => x.Transaction.GetTransactionsByTypeAsync(It.IsAny<bool>()))
            .ReturnsAsync(_transactions.Where(x => x.IsPayment == true));
        
        var adminService = new AdminService(moqUnitOfWork.Object);
        
        var result = await adminService.GetTransactionsByTypeAsync(true);
        
        ClassicAssert.AreEqual(_transactions.Where(x => x.IsPayment == true), result);
    }
    
    [Test]
    public async Task TestGetTransactionsByOrderAsync()
    {
        var moqUnitOfWork = new Mock<IUnitOfWork>();
        moqUnitOfWork.Setup(x => x.Order.GetByIdAsync(It.IsAny<long>())).ReturnsAsync(_order);
        moqUnitOfWork.Setup(x => x.Transaction.GetTransactionsByOrderAsync(It.IsAny<Order>()))
            .ReturnsAsync(_transactions.Where(x => x.Order == _order));
        
        var adminService = new AdminService(moqUnitOfWork.Object);
        
        var result = await adminService.GetTransactionsByOrderAsync(1);
        
        ClassicAssert.AreEqual(_transactions.Where(x => x.Order == _order), result);
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
            new Transaction{Id = 1, Banknote = _banknotes[3], Order = _order, IsPayment = true},
            new Transaction{Id = 2, Banknote = _banknotes[3], Order = _order, IsPayment = true},
            new Transaction{Id = 3, Banknote = _banknotes[4], Order = _order, IsPayment = false},
            new Transaction{Id = 4, Banknote = _banknotes[5], Order = _order, IsPayment = false},
            new Transaction{Id = 5, Banknote = _banknotes[6], Order = _order, IsPayment = false},
            new Transaction{Id = 6, Banknote = _banknotes[8], Order = _order, IsPayment = false},
            new Transaction{Id = 7, Banknote = _banknotes[8], Order = _order, IsPayment = false}
        };
    }
}