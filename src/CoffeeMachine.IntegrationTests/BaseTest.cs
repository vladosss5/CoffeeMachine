using CoffeeMachine.API;
using CoffeeMachine.Core.Models;
using CoffeeMachine.Persistence.Data.Context;
using CoffeeMachine.Persistence.Data.Migrations;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Linq;

namespace CoffeeMachine.IntegrationTests;

/// <summary>
/// Базовый тест. Служит для настройки DI тестируемого проета.
/// </summary>
public abstract class BaseTest
{
    /// <summary>
    /// HTTP клиент.
    /// </summary>
    protected HttpClient _client;
    
    /// <summary>
    /// Контекст данных.
    /// </summary>
    protected DataContext _dataContext;

    /// <summary>
    /// Токен авторизаии администратора.
    /// </summary>
    protected string _adminToken;
    
    /// <summary>
    /// Токен авторизации пользователя.
    /// </summary>
    protected string _defafultUserToken;    
    
    /// <summary>
    /// Кофе.
    /// </summary>
    protected Coffee _coffee;
    
    /// <summary>
    /// Кофемашина.
    /// </summary>
    protected Machine _machine;
    
    /// <summary>
    /// Банкноты.
    /// </summary>
    protected List<Banknote> _banknotes;
    
    /// <summary>
    /// Банкноты в кофемашине.
    /// </summary>
    protected List<BanknoteToMachine> _banknotesToMachines;
    
    /// <summary>
    /// Кофе в кофемашине.
    /// </summary>
    protected List<CoffeeToMachine> _coffeeToMachines;
    
    /// <summary>
    /// Заказ.
    /// </summary>
    protected Order _order;

    /// <summary>
    /// Отчистка.
    /// </summary>
    [TearDown]
    public void Dispose()
    {
        _client.Dispose();
        _dataContext.Dispose();
    }

    /// <summary>
    /// Инициализация DI.
    /// </summary>
    [SetUp]
    public async Task Init()
    {
        _client = new HttpClient();
        var webHost = new WebApplicationFactory<Startup>().WithWebHostBuilder(builder =>
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
        
        FillingData();
        
        _dataContext = webHost.Services.CreateScope().ServiceProvider.GetService<DataContext>();
        
        await _dataContext.AddRangeAsync(_coffee);
        await _dataContext.AddRangeAsync(_machine);
        await _dataContext.AddRangeAsync(_banknotes);
        await _dataContext.AddRangeAsync(_banknotesToMachines);
        await _dataContext.AddRangeAsync(_coffeeToMachines);
        await _dataContext.AddRangeAsync(_order);
        await _dataContext.SaveChangesAsync();

        _adminToken = await GetToken("testuseradmin", "root");
        _defafultUserToken = await GetToken("defaultuser", "toor");
        
        _client = webHost.CreateClient();
    }

    /// <summary>
    /// Получение JWT токена от keycloak.
    /// </summary>
    /// <param name="login">Имя пользователя.</param>
    /// <param name="password">Пароль.</param>
    /// <returns>JWT токен.</returns>
    public async Task<string> GetToken(string login, string password)
    {
        var reqestKeycloak = new Dictionary<string, string>
        {
            {"grant_type", "password"},
            {"client_id", "backend"},
            {"username", login},
            {"password", password},
            {"client_secret", "kRXuEEcKD54WZGvOC0X3Di8ObhMUrFnl"},
            {"scope", "roles"}
        };
            
        var response = await _client.PostAsync("http://localhost:8282/realms/MyRealm/protocol/openid-connect/token",
            new FormUrlEncodedContent(reqestKeycloak));
        var responseString = JObject.Parse(await response.Content.ReadAsStringAsync());
        var token = (string)responseString["access_token"];
        
        return token;
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