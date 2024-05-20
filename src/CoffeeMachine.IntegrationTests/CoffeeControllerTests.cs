using System.Net;
using System.Net.Http.Json;
using CoffeeMachine.API;
using CoffeeMachine.Core.Models;
using CoffeeMachine.Persistence.Data.Context;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Text.Json;
using Newtonsoft.Json;
using NUnit.Framework.Legacy;

namespace CoffeeMachine.IntegrationTests;

/// <summary>
/// Тестирование CoffeeController.
/// </summary>
[TestFixture]
public class CoffeeControllerTests
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
    /// Заказ.
    /// </summary>
    private Order _order;

    /// <summary>
    /// Конструктор класса.
    /// </summary>
    public CoffeeControllerTests()
    {
        FillingData();
    }
    /// <summary>
    /// Тест для получения списка кофе.
    /// </summary>
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

        var verifyCoffees = new List<Coffee> { _coffee };
        
        var context = webHost.Services.CreateScope().ServiceProvider.GetService<DataContext>();
        
        await context.AddRangeAsync(_coffee);
        await context.SaveChangesAsync();
        
        HttpClient httpClient = webHost.CreateClient();
        
        //Act
        HttpResponseMessage response = await httpClient.GetAsync("/api/coffee");
        var responseString = await response.Content.ReadAsStringAsync();
        var coffees = JsonConvert.DeserializeObject<List<Coffee>>(responseString);
        
        //Assert
        ClassicAssert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        ClassicAssert.AreEqual(HttpStatusCode.OK, response.StatusCode);

        for (int i = 0; i < verifyCoffees.Count; i++)
        {
            ClassicAssert.AreEqual(verifyCoffees[i].Name, coffees[i].Name);
            ClassicAssert.AreEqual(verifyCoffees[i].Price, coffees[i].Price);
        }
        
        context.Database.EnsureDeleted();
    }
    
    /// <summary>
    /// Тест получения кофе по Id.
    /// </summary>
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
        
        var httpClient = webHost.CreateClient();
        
        //Act
        var response = await httpClient.GetAsync("/api/coffee/1");
        var responseString = await response.Content.ReadAsStringAsync();
        var coffee = JsonConvert.DeserializeObject<Coffee>(responseString);
        
        //Assert
        ClassicAssert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        ClassicAssert.AreEqual(_coffee.Id, coffee.Id);
        
        context.Database.EnsureDeleted();
    }

    /// <summary>
    /// Тест создания кофе.
    /// </summary>
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
        
        var verifyCoffee = new Coffee{Id = 2, Name = "Latte", Price = 900};
        
        var context = webHost.Services.CreateScope().ServiceProvider.GetService<DataContext>();
        
        await context.AddRangeAsync(_coffee);
        await context.SaveChangesAsync();
        
        var httpClient = webHost.CreateClient();
        
        //Act
        var responseOk = await httpClient.PostAsJsonAsync("/api/coffee", verifyCoffee);
        var coffeeString = await responseOk.Content.ReadAsStringAsync();
        var coffee = JsonConvert.DeserializeObject<Coffee>(coffeeString);
        
        //Assert
        ClassicAssert.AreEqual(HttpStatusCode.OK, responseOk.StatusCode);
        ClassicAssert.AreEqual(verifyCoffee.Id, coffee.Id);
        ClassicAssert.AreEqual(verifyCoffee.Name, coffee.Name);
        ClassicAssert.AreEqual(verifyCoffee.Price, coffee.Price);
        
        context.Database.EnsureDeleted();
    }

    /// <summary>
    /// Тест обновления кофе.
    /// </summary>
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
        
        var verifyCoffee = new Coffee{Id = 1, Name = "Latte", Price = 800};
        
        var context = webHost.Services.CreateScope().ServiceProvider.GetService<DataContext>();
        
        await context.AddRangeAsync(_coffee);
        await context.SaveChangesAsync();
        
        var httpClient = webHost.CreateClient();
        
        //Act
        var response = await httpClient.PutAsJsonAsync("/api/coffee/1", verifyCoffee);
        var coffeeString = await response.Content.ReadAsStringAsync();
        var coffee = JsonConvert.DeserializeObject<Coffee>(coffeeString);
        
        //Assert
        ClassicAssert.AreEqual(response.StatusCode, HttpStatusCode.OK);
        ClassicAssert.AreEqual(verifyCoffee.Id, coffee.Id);
        ClassicAssert.AreEqual(verifyCoffee.Name, coffee.Name);
        ClassicAssert.AreEqual(verifyCoffee.Price, coffee.Price);
        
        context.Database.EnsureDeleted();
    }
    
    /// <summary>
    /// Тест удаления кофе.
    /// </summary>
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
        
        //Assert
        ClassicAssert.AreEqual(response.StatusCode, HttpStatusCode.NoContent);
        
        context.Database.EnsureDeleted();
    }

    /// <summary>
    /// Заполнение данных.
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
    }
}