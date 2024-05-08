﻿using System.Net;
using System.Net.Http.Json;
using CoffeeMachine.API;
using CoffeeMachine.Core.Models;
using CoffeeMachine.Persistence.Data.Context;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Text.Json;

namespace CoffeeMachine.IntegrationTests;

[TestFixture]
public class CoffeeControllerTests
{
    private Coffee _coffee;
    private Machine _machine;
    private List<Banknote> _banknotes;
    private List<BanknoteToMachine> _banknotesToMachines;
    private List<CoffeeToMachine> _coffeeToMachines;
    private List<Transaction> _transactions;
    private Order _order;

    public CoffeeControllerTests()
    {
        FillingData();
    }
    
    [Test]
    public async Task GetCoffeeList_SendRequest_StatusCodeOk()
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
        
        await context.AddRangeAsync(_coffee);
        await context.SaveChangesAsync();
        
        HttpClient httpClient = webHost.CreateClient();
        
        //Act
        HttpResponseMessage response = await httpClient.GetAsync("/api/coffee");
        
        //Assert
        Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        
        context.Database.EnsureDeleted();
    }
    
    [Test]
    public async Task GetCoffeeById_SendRequest_StatusCodeOk()
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
        
        await context.AddRangeAsync(_coffee);
        await context.SaveChangesAsync();
        
        HttpClient httpClient = webHost.CreateClient();
        
        //Act
        HttpResponseMessage response = await httpClient.GetAsync("/api/coffee/1");
        
        //Assert
        Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        
        context.Database.EnsureDeleted();
    }

    [Test]
    public async Task CreateCoffee_SendRequest_StatusCodeOk()
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
        
        await context.AddRangeAsync(_coffee);
        await context.SaveChangesAsync();
        
        var httpClient = webHost.CreateClient();
        
        //Act
        var responseAlreadyExist = await httpClient.PostAsJsonAsync("/api/coffee", _coffee);
        var responseOk = await httpClient.PostAsJsonAsync("/api/coffee", new Coffee{Id = 2, Name = "Latte", Price = 900});
        
        //Assert
        Assert.AreEqual(HttpStatusCode.OK, responseOk.StatusCode);
        Assert.AreEqual(HttpStatusCode.BadRequest, responseAlreadyExist.StatusCode);
        
        context.Database.EnsureDeleted();
    }

    [Test]
    public async Task UpdateCoffee_SendRequest_StatusCodeOk()
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
        
        await context.AddRangeAsync(_coffee);
        await context.SaveChangesAsync();
        
        var httpClient = webHost.CreateClient();
        
        //Act
        var response = await httpClient.PutAsJsonAsync("/api/coffee/1", new Coffee{Id = 1, Name = "Latte", Price = 800});
        var responseNotFound = await httpClient.PutAsJsonAsync("/api/coffee/1", new Coffee{Id = 2, Name = "Latte", Price = 800});
        
        //Assert
        Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
        Assert.AreEqual(responseNotFound.StatusCode, HttpStatusCode.NotFound);
        
        context.Database.EnsureDeleted();
    }
    
    [Test]
    public async Task DeleteCoffee_SendRequest_StatusCodeOk()
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
        
        await context.AddRangeAsync(_coffee);
        await context.SaveChangesAsync();
        
        var httpClient = webHost.CreateClient();
        
        //Act
        var response = await httpClient.DeleteAsync("/api/coffee/1");
        var responseNotFound = await httpClient.DeleteAsync("/api/coffee/2");
        
        //Assert
        Assert.AreEqual(response.StatusCode, HttpStatusCode.NoContent);
        Assert.AreEqual(responseNotFound.StatusCode, HttpStatusCode.NotFound);
        
        context.Database.EnsureDeleted();
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