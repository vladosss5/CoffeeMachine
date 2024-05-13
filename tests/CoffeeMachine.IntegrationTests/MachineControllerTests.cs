﻿using System.Net;
using System.Net.Http.Json;
using CoffeeMachine.API;
using CoffeeMachine.API.DTOs.CoffeesInMachine;
using CoffeeMachine.Core.Models;
using CoffeeMachine.Persistence.Data.Context;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

namespace CoffeeMachine.IntegrationTests;

/// <summary>
/// Тестирование MachineController.
/// </summary>
[TestFixture]
public class MachineControllerTests
{
    /// <summary>
    /// Кофе.
    /// </summary>
    private Coffee _coffee;
    
    /// <summary>
    /// Кофемашина.
    /// </summary>
    private Machine _machine;
    
    /// <summary>
    /// Банкноты.
    /// </summary>
    private List<Banknote> _banknotes;
    
    /// <summary>
    /// Банкноты в кофемашине.
    /// </summary>
    private List<BanknoteToMachine> _banknotesToMachines;
    
    /// <summary>
    /// Кофе в кофемашине.
    /// </summary>
    private List<CoffeeToMachine> _coffeeToMachines;
    
    /// <summary>
    /// Транзакции.
    /// </summary>
    private List<Transaction> _transactions;
    
    /// <summary>
    /// Заказ.
    /// </summary>
    private Order _order;

    /// <summary>
    /// Конструктор класса.
    /// </summary>
    public MachineControllerTests()
    {
        FillingData();
    }

    /// <summary>
    /// Тест получения всех кофемашин.
    /// </summary>
    [Test]
    public async Task GetAllMachines_SendRequest_StatusCodeOk()
    {
        //Arrange
        WebApplicationFactory<Startup> webHost = new WebApplicationFactory<Startup>().WithWebHostBuilder(builder =>
        {
            builder.ConfigureTestServices(services =>
            {
                var dbContextDescriptor = services.FirstOrDefault(d =>
                    d.ServiceType == typeof(DbContextOptions<DataContext>));

                services.Remove(dbContextDescriptor);
                services.AddDbContext<DataContext>(options =>
                {
                    options.UseInMemoryDatabase("CoffeeMachine");
                });
            });
        });

        var verifyMachine = new List<Machine>
        {
            new Machine { Id = 1, SerialNumber = "11", Description = "wdw", Balance = 0 }
        };
        
        var context = webHost.Services.CreateScope().ServiceProvider.GetService<DataContext>();
        
        await context.AddRangeAsync(_machine);
        await context.SaveChangesAsync();
        
        var var = webHost.CreateClient();
        
        //Act
        var response = await var.GetAsync("/api/Machine");
        var machinesString = await response.Content.ReadAsStringAsync();
        var machines = JsonConvert.DeserializeObject<List<Machine>>(machinesString);
        
        //Assert
        Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);

        for (int i = 0; i < verifyMachine.Count; i++)
        {
            Assert.AreEqual(verifyMachine[i].Id, machines[i].Id);
            Assert.AreEqual(verifyMachine[i].SerialNumber, machines[i].SerialNumber);
            Assert.AreEqual(verifyMachine[i].Description, machines[i].Description);
            Assert.AreEqual(verifyMachine[i].Balance, machines[i].Balance);
        }
        
        context.Database.EnsureDeleted();
    }
    
    /// <summary>
    /// Тест получения кофемашины по Id.
    /// </summary>
    [Test]
    public async Task GetMachineById_SendRequest_StatusCodeOk()
    {
        //Arrange
        WebApplicationFactory<Startup> webHost = new WebApplicationFactory<Startup>().WithWebHostBuilder(builder =>
        {
            builder.ConfigureTestServices(services =>
            {
                var dbContextDescriptor = services.FirstOrDefault(d =>
                    d.ServiceType == typeof(DbContextOptions<DataContext>));

                services.Remove(dbContextDescriptor);
                services.AddDbContext<DataContext>(options =>
                {
                    options.UseInMemoryDatabase("CoffeeMachine");
                });
            });
        });

        var verifyMachine = new Machine { Id = 1, SerialNumber = "11", Description = "wdw", Balance = 0 };
        var context = webHost.Services.CreateScope().ServiceProvider.GetService<DataContext>();
        
        await context.AddRangeAsync(_machine);
        await context.SaveChangesAsync();
        
        var var = webHost.CreateClient();
        
        //Act
        var response = await var.GetAsync($"/api/Machine/{_machine.Id}");
        var machineString = await response.Content.ReadAsStringAsync();
        var machine = JsonConvert.DeserializeObject<Machine>(machineString);
        
        //Assert
        Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
        Assert.AreEqual(verifyMachine.Id, machine.Id);
        Assert.AreEqual(verifyMachine.SerialNumber, machine.SerialNumber);
        Assert.AreEqual(verifyMachine.Description, machine.Description);
        Assert.AreEqual(verifyMachine.Balance, machine.Balance);
        
        context.Database.EnsureDeleted();
    }
    
    /// <summary>
    /// Тест создания кофемашины.
    /// </summary>
    [Test]
    public async Task CreateMachine_SendRequest_StatusCodeOk()
    {
        //Arrange
        WebApplicationFactory<Startup> webHost = new WebApplicationFactory<Startup>().WithWebHostBuilder(builder =>
        {
            builder.ConfigureTestServices(services =>
            {
                var dbContextDescriptor = services.FirstOrDefault(d =>
                    d.ServiceType == typeof(DbContextOptions<DataContext>));

                services.Remove(dbContextDescriptor);
                services.AddDbContext<DataContext>(options =>
                {
                    options.UseInMemoryDatabase("CoffeeMachine");
                });
            });
        });
        
        var verifyMachine = new Machine { Id = 2, SerialNumber = "22", Description = "dfd", Balance = 0 };
        var context = webHost.Services.CreateScope().ServiceProvider.GetService<DataContext>();
        
        await context.AddRangeAsync(_machine);
        await context.SaveChangesAsync();
        
        var var = webHost.CreateClient();
        
        //Act
        var response = await var.PostAsJsonAsync("/api/Machine", new Machine { SerialNumber = "22", Description = "dfd" });
        var responseAlreadyExist = await var.PostAsJsonAsync("/api/Machine", _machine);
        var machineString = await response.Content.ReadAsStringAsync();
        var machine = JsonConvert.DeserializeObject<Machine>(machineString);
        
        //Assert
        Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
        Assert.AreEqual(responseAlreadyExist.StatusCode, HttpStatusCode.BadRequest);
        Assert.AreEqual(verifyMachine.Id, machine.Id);
        Assert.AreEqual(verifyMachine.SerialNumber, machine.SerialNumber);
        Assert.AreEqual(verifyMachine.Description, machine.Description);
        Assert.AreEqual(verifyMachine.Balance, machine.Balance);
        
        context.Database.EnsureDeleted();
    }
    
    /// <summary>
    /// Тест обновления кофемашины.
    /// </summary>
    [Test]
    public async Task UpdateMachine_SendRequest_StatusCodeOk()
    {
        //Arrange
        WebApplicationFactory<Startup> webHost = new WebApplicationFactory<Startup>().WithWebHostBuilder(builder =>
        {
            builder.ConfigureTestServices(services =>
            {
                var dbContextDescriptor = services.FirstOrDefault(d =>
                    d.ServiceType == typeof(DbContextOptions<DataContext>));

                services.Remove(dbContextDescriptor);
                services.AddDbContext<DataContext>(options =>
                {
                    options.UseInMemoryDatabase("CoffeeMachine");
                });
            });
        });
        
        var verifyMachine = new Machine { Id = 1, SerialNumber = "33", Description = "wdw", Balance = 1000 };
        var context = webHost.Services.CreateScope().ServiceProvider.GetService<DataContext>();
        
        await context.AddRangeAsync(_machine);
        await context.SaveChangesAsync();
        
        var var = webHost.CreateClient();
        
        //Act
        var response = await var.PutAsJsonAsync($"/api/Machine/", new Machine { Id = 1, SerialNumber = "33", Description = "wdw", Balance = 1000 });
        var responseNotFound = await var.PutAsJsonAsync($"/api/Machine/", new Machine { Id = 2, SerialNumber = "33", Description = "wdw" });
        var machineString = await response.Content.ReadAsStringAsync();
        var machine = JsonConvert.DeserializeObject<Machine>(machineString);
        
        //Assert
        Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
        Assert.AreEqual(responseNotFound.StatusCode, HttpStatusCode.NotFound);
        Assert.AreEqual(verifyMachine.Id, machine.Id);
        Assert.AreEqual(verifyMachine.SerialNumber, machine.SerialNumber);
        Assert.AreEqual(verifyMachine.Description, machine.Description);
        
        context.Database.EnsureDeleted();
    }
    
    /// <summary>
    /// Тест удаления кофемашины.
    /// </summary>
    [Test]
    public async Task DeleteMachine_SendRequest_StatusCodeOk()
    {
        //Arrange
        WebApplicationFactory<Startup> webHost = new WebApplicationFactory<Startup>().WithWebHostBuilder(builder =>
        {
            builder.ConfigureTestServices(services =>
            {
                var dbContextDescriptor = services.FirstOrDefault(d =>
                    d.ServiceType == typeof(DbContextOptions<DataContext>));

                services.Remove(dbContextDescriptor);
                services.AddDbContext<DataContext>(options =>
                {
                    options.UseInMemoryDatabase("CoffeeMachine");
                });
            });
        });
        
        var context = webHost.Services.CreateScope().ServiceProvider.GetService<DataContext>();
        
        await context.AddRangeAsync(_machine);
        await context.SaveChangesAsync();
        
        var var = webHost.CreateClient();
        
        //Act
        var response = await var.DeleteAsync($"/api/Machine/{_machine.Id}");
        var responseNotFound = await var.DeleteAsync($"/api/Machine/{_machine.Id}");
        
        //Assert
        Assert.AreEqual(response.StatusCode, HttpStatusCode.NoContent);
        Assert.AreEqual(responseNotFound.StatusCode, HttpStatusCode.NotFound);
        
        context.Database.EnsureDeleted();
    }
    
    /// <summary>
    /// Тест добавления кофе в кофемашину.
    /// </summary>
    [Test]
    public async Task AddCoffeeToMachines_SendRequest_StatusCodeOk()
    {
        //Arrange
        WebApplicationFactory<Startup> webHost = new WebApplicationFactory<Startup>().WithWebHostBuilder(builder =>
        {
            builder.ConfigureTestServices(services =>
            {
                var dbContextDescriptor = services.FirstOrDefault(d =>
                    d.ServiceType == typeof(DbContextOptions<DataContext>));

                services.Remove(dbContextDescriptor);
                services.AddDbContext<DataContext>(options =>
                {
                    options.UseInMemoryDatabase("CoffeeMachine");
                });
            });
        });
        
        var context = webHost.Services.CreateScope().ServiceProvider.GetService<DataContext>();
        
        await context.AddRangeAsync(_machine, _coffee);
        await context.SaveChangesAsync();
        
        var var = webHost.CreateClient();
        
        //Act
        var response = await var.PostAsJsonAsync($"/api/Machine/AddCoffeeToMachines/{_machine.Id}", _coffee.Id);
        var responseString = await response.Content.ReadAsStringAsync();
        var coffeeToMachine = JsonConvert.DeserializeObject<CoffeesInMachineDto>(responseString);
        
        //Assert
        Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
        Assert.AreEqual(_machine.Id, coffeeToMachine.Machine.Id);
        Assert.AreEqual(_coffee.Id, coffeeToMachine.Coffees.FirstOrDefault(x => x.Id == _coffee.Id).Id);
        
        context.Database.EnsureDeleted();
    }
    
    /// <summary>
    /// Тест удаления кофе из кофемашины.
    /// </summary>
    [Test]
    public async Task DeleteCoffeeFromMachines_SendRequest_StatusCodeOk()
    {
        //Arrange
        WebApplicationFactory<Startup> webHost = new WebApplicationFactory<Startup>().WithWebHostBuilder(builder =>
        {
            builder.ConfigureTestServices(services =>
            {
                var dbContextDescriptor = services.FirstOrDefault(d =>
                    d.ServiceType == typeof(DbContextOptions<DataContext>));

                services.Remove(dbContextDescriptor);
                services.AddDbContext<DataContext>(options =>
                {
                    options.UseInMemoryDatabase("CoffeeMachine");
                });
            });
        });
        
        var context = webHost.Services.CreateScope().ServiceProvider.GetService<DataContext>();
        
        await context.AddRangeAsync(_machine, _coffee);
        foreach (var сoffeeToMachine in _coffeeToMachines)
        {
            await context.AddAsync(сoffeeToMachine);
        }
        await context.SaveChangesAsync();
        
        var var = webHost.CreateClient();
        
        //Act
        var response = await var.PostAsJsonAsync($"/api/Machine/DeleteCoffeeFromMachines/{_machine.Id}", _coffee.Id);
        var responseString = await response.Content.ReadAsStringAsync();
        var coffeeToMachine = JsonConvert.DeserializeObject<CoffeesInMachineDto>(responseString);
        
        //Assert
        Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
        Assert.AreEqual(0, coffeeToMachine.Coffees.Count);
        
        context.Database.EnsureDeleted();
    }

    /// <summary>
    /// Тест добавления банкнот в кофемашину.
    /// </summary>
    [Test]
    public async Task AddBanknotesToMachines_SendRequest_StatusCodeOk()
    {
        //Arrange
        WebApplicationFactory<Startup> webHost = new WebApplicationFactory<Startup>().WithWebHostBuilder(builder =>
        {
            builder.ConfigureTestServices(services =>
            {
                var dbContextDescriptor = services.FirstOrDefault(d =>
                    d.ServiceType == typeof(DbContextOptions<DataContext>));

                services.Remove(dbContextDescriptor);
                services.AddDbContext<DataContext>(options =>
                {
                    options.UseInMemoryDatabase("CoffeeMachine");
                });
            });
        });
        
        var context = webHost.Services.CreateScope().ServiceProvider.GetService<DataContext>();
        await context.AddRangeAsync(_banknotes);
        await context.AddRangeAsync(_banknotesToMachines);
        await context.AddRangeAsync(_machine);
        await context.SaveChangesAsync();
        
        var var = webHost.CreateClient();
        
        //Act
        var response = await var.PostAsJsonAsync($"/api/Machine/AddBanknotesToMachines/{_machine.Id}", 
            new List<Banknote>
            {
                new Banknote{Nominal = 500},
                new Banknote{Nominal = 500}
            });
        var responseString = await response.Content.ReadAsStringAsync();
        var machine = JsonConvert.DeserializeObject<Machine>(responseString);
        
        //Assert
        Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        Assert.AreEqual(87680, machine.Balance);
        
        context.Database.EnsureDeleted();
    }

    /// <summary>
    /// Тест вычитания банкнот из кофемашины.
    /// </summary>
    [Test]
    public async Task SubtractBanknotesFromMachines_SendRequest_StatusCodeOk()
    {
        //Arrange
        WebApplicationFactory<Startup> webHost = new WebApplicationFactory<Startup>().WithWebHostBuilder(builder =>
        {
            builder.ConfigureTestServices(services =>
            {
                var dbContextDescriptor = services.FirstOrDefault(d =>
                    d.ServiceType == typeof(DbContextOptions<DataContext>));

                services.Remove(dbContextDescriptor);
                services.AddDbContext<DataContext>(options =>
                {
                    options.UseInMemoryDatabase("CoffeeMachine");
                });
            });
        });
        
        var context = webHost.Services.CreateScope().ServiceProvider.GetService<DataContext>();
        
        await context.AddRangeAsync(_banknotes);
        await context.AddRangeAsync(_banknotesToMachines);
        await context.AddRangeAsync(_machine);
        
        await context.SaveChangesAsync();
        
        var var = webHost.CreateClient();
        
        //Act
        var response = await var.PostAsJsonAsync($"/api/Machine/DeleteBanknotesFromMachines/{_machine.Id}", 
            new List<Banknote>
            {
                new Banknote{Nominal = 500},
                new Banknote{Nominal = 500}
            });
        var responseString = await response.Content.ReadAsStringAsync();
        var machine = JsonConvert.DeserializeObject<Machine>(responseString);
        
        //Assert
        Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        Assert.AreEqual(85680, machine.Balance);
        
        context.Database.EnsureDeleted();
    }

    /// <summary>
    /// Тест получения списка банкнот в кофемашине.
    /// </summary>
    [Test]
    public async Task GetBanknotesByMachine_SendRequest_StatusCodeOk()
    {
        //Arrange
        WebApplicationFactory<Startup> webHost = new WebApplicationFactory<Startup>().WithWebHostBuilder(builder =>
        {
            builder.ConfigureTestServices(services =>
            {
                var dbContextDescriptor = services.FirstOrDefault(d =>
                    d.ServiceType == typeof(DbContextOptions<DataContext>));

                services.Remove(dbContextDescriptor);
                services.AddDbContext<DataContext>(options =>
                {
                    options.UseInMemoryDatabase("CoffeeMachine");
                });
            });
        });
        
        var context = webHost.Services.CreateScope().ServiceProvider.GetService<DataContext>();
        await context.AddRangeAsync(_banknotes);
        await context.AddRangeAsync(_banknotesToMachines);
        await context.AddRangeAsync(_machine);
        await context.SaveChangesAsync();
        
        var var = webHost.CreateClient();
        
        //Act
        var response = await var.GetAsync($"/api/Machine/GetBanknotesByMachine/{_machine.Id}");
        var responseString = await response.Content.ReadAsStringAsync();
        var banknotesToMachines = JsonConvert.DeserializeObject<List<BanknoteToMachine>>(responseString);
        
        //Assert
        Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        foreach (var banknoteToMachine in banknotesToMachines)
        {
            Assert.AreEqual(10, banknoteToMachine.CountBanknote);
        }
        
        context.Database.EnsureDeleted();
    }

    /// <summary>
    /// Получение списка кофе из кофемашины.
    /// </summary>
    [Test]
    public async Task GetCoffeesFromMachine_SendRequest_StatusCodeOk()
    {
        //Arrange
        WebApplicationFactory<Startup> webHost = new WebApplicationFactory<Startup>().WithWebHostBuilder(builder =>
        {
            builder.ConfigureTestServices(services =>
            {
                var dbContextDescriptor = services.FirstOrDefault(d =>
                    d.ServiceType == typeof(DbContextOptions<DataContext>));

                services.Remove(dbContextDescriptor);
                services.AddDbContext<DataContext>(options =>
                {
                    options.UseInMemoryDatabase("CoffeeMachine");
                });
            });
        });
        
        var context = webHost.Services.CreateScope().ServiceProvider.GetService<DataContext>();
        
        await context.AddRangeAsync(_machine, _coffee);
        await context.AddRangeAsync(_coffeeToMachines);
        await context.SaveChangesAsync();
        
        var var = webHost.CreateClient();
        
        //Act
        var response = await var.GetAsync($"/api/Machine/GetCoffeesFromMachine/{_machine.Id}");
        var responseString = await response.Content.ReadAsStringAsync();
        var coffees = JsonConvert.DeserializeObject<List<Coffee>>(responseString);
        
        //Assert
        Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        Assert.AreEqual(_coffee.Id, coffees[0].Id);
        
        context.Database.EnsureDeleted();
    }
    
    /// <summary>
    /// Заполнение данных для тестирования.
    /// </summary>
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