﻿using System.Net.Http.Headers;
using System.Net.Http.Json;
using CoffeeMachine.API;
using CoffeeMachine.API.DTOs.Account;
using CoffeeMachine.Core.Models;
using CoffeeMachine.IntegrationTests.AuthorizationMoq;
using CoffeeMachine.Persistence.Data.Context;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Policy;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Newtonsoft.Json.Linq;

namespace CoffeeMachine.IntegrationTests;

/// <summary>
/// Базовый тест. Служит для настройки DI тестируемого проета.
/// </summary>
public abstract class BaseTest : WebApplicationFactory<Startup>
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
        
        // _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Test");

        var webHost = new WebApplicationFactory<Startup>().WithWebHostBuilder(builder =>
        {
            builder.ConfigureTestServices(services =>
            {
                services.AddSingleton<IPolicyEvaluator, FakePolicyEvaluator>();
                var dbContextDescriptor = services.FirstOrDefault(d =>
                    d.ServiceType == typeof(DbContextOptions<DataContext>));

                services.Remove(dbContextDescriptor);
                services.AddDbContext<DataContext>(options =>
                {
                    options.UseInMemoryDatabase("CoffeeMachine");
                });
            });
        });
        _client = webHost.CreateClient();
        
        FillingData();
        
        _dataContext = webHost.Services.CreateScope().ServiceProvider.GetService<DataContext>();
        
        await _dataContext.AddRangeAsync(_coffee);
        await _dataContext.AddRangeAsync(_machine);
        await _dataContext.AddRangeAsync(_banknotes);
        await _dataContext.AddRangeAsync(_banknotesToMachines);
        await _dataContext.AddRangeAsync(_coffeeToMachines);
        await _dataContext.AddRangeAsync(_order);
        await _dataContext.SaveChangesAsync();
        
        //_client = webHost.CreateClient();

        // _adminToken = await GetToken("admin", "admin");
    }
    
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        //builder.ConfigureTestServices(ConfigureServices);
        builder.ConfigureLogging((WebHostBuilderContext context, ILoggingBuilder loggingBuilder) =>
        {
            loggingBuilder.ClearProviders();
            loggingBuilder.AddConsole(options => options.IncludeScopes = true);
        });
    }

    //protected virtual void ConfigureServices(IServiceCollection services)
    //{
    //    services.Configure<JwtBearerOptions>(JwtBearerDefaults.AuthenticationScheme, options =>
    //    {
    //        var config = new OpenIdConnectConfiguration()
    //        {
    //            Issuer = MockJwtTokens.Issuer
    //        };
//
    //        config.SigningKeys.Add(MockJwtTokens.SecurityKey);
    //        options.Configuration = config;
    //    });
    //}

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
            {"client_id", "admin-cli"},
            {"username", login},
            {"password", password},
            {"scope", "roles"}
        };
            
        var response = await _client.PostAsync("http://keycloak:8282/realms/master/protocol/openid-connect/token",
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