﻿using System.Net;
using System.Net.Http.Json;
using CoffeeMachine.API;
using CoffeeMachine.API.DTOs.Banknote;
using CoffeeMachine.API.DTOs.Coffee;
using CoffeeMachine.API.DTOs.Machine;
using CoffeeMachine.API.DTOs.Order;
using CoffeeMachine.API.DTOs.Transaction;
using CoffeeMachine.Core.Models;
using CoffeeMachine.Persistence.Data.Context;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using NUnit.Framework.Legacy;

namespace CoffeeMachine.IntegrationTests;

/// <summary>
/// Тестирование OrderController.
/// </summary>
[TestFixture]
public class OrderControllerTests
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
    /// Заказ.
    /// </summary>
    private Order _order;
    
    /// <summary>
    /// Конструктор класса.
    /// </summary>
    public OrderControllerTests()
    {
        FillingData();
    }

    /// <summary>
    /// Тест создания заказа.
    /// </summary>
    [Test]
    public async Task CreateOrder_SendRequest_StatusCodeOk()
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
        
        var sumDelivery = 0;
        
        var context = webHost.Services.CreateScope().ServiceProvider.GetService<DataContext>();
        ClearContext(context);
        
        await context.AddRangeAsync(_coffee, _machine);
        await context.AddRangeAsync(_banknotes);
        await context.AddRangeAsync(_banknotesToMachines);
        await context.AddRangeAsync(_coffeeToMachines);
        await context.SaveChangesAsync();
        
        HttpClient httpClient = webHost.CreateClient();
        
        var requestOrder = new OrderAddRequestDto
        {
            Machine = new MachineForOrderDto{Id = _machine.Id},
            Coffee = new CoffeeForOrderRequestDto{Name = _coffee.Name},
            Transactions = new List<TransactionForOrderDto>
            {
                new TransactionForOrderDto
                {
                    Banknote = new BanknoteDto{Nominal = _banknotes[2].Nominal},
                    IsPayment = true
                }
            }
        };
        
        //Act
        var response = await httpClient.PostAsJsonAsync("/api/order", requestOrder);
        var responseString = await response.Content.ReadAsStringAsync();
        var responseOrder = JsonConvert.DeserializeObject<OrderAddResponseDto>(responseString);
        
        //Assert
        ClassicAssert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        ClassicAssert.AreEqual(responseOrder.Coffee.Name, _coffee.Name);
        foreach (var transaction in responseOrder.Transactions)
        {
            ClassicAssert.AreEqual(false, transaction.IsPayment);
            sumDelivery+= transaction.Banknote.Nominal;   
        }
        ClassicAssert.AreEqual(164, sumDelivery);
        
        context.Database.EnsureDeleted();
    }

    /// <summary>
    /// Тест получения заказа по Id.
    /// </summary>
    [Test]
    public async Task GetOrderById_SendRequest_StatusCodeOk()
    {
        //Arrange
        WebApplicationFactory<Startup> webHost = new WebApplicationFactory<Startup>().WithWebHostBuilder(builder =>
        {
            builder.ConfigureTestServices(services =>
            {
                var dbContextDescriptor = services.FirstOrDefault(d =>
                    d.ServiceType == typeof(DbContextOptions<DataContext>));

                services.Remove(dbContextDescriptor);
                services.AddDbContext<DataContext>(options => { options.UseInMemoryDatabase("CoffeeMachine"); });
            });
        });
        
        var context = webHost.Services.CreateScope().ServiceProvider.GetService<DataContext>();
        ClearContext(context);

        await context.AddRangeAsync(_order);
        await context.SaveChangesAsync();
        
        HttpClient httpClient = webHost.CreateClient();
        
        //Act
        var response = await httpClient.GetAsync($"/api/order/{_order.Id}");
        var responseString = await response.Content.ReadAsStringAsync();
        var responseOrder = JsonConvert.DeserializeObject<OrderResponseDto>(responseString);
        
        //Assert
        ClassicAssert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        ClassicAssert.AreEqual(_order.Id, responseOrder.Id);
        
        context.Database.EnsureDeleted();
    }
    
    private void ClearContext(DataContext context)
    {
        context.Database.EnsureDeleted();
        context.Database.EnsureCreated();
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
    }
}