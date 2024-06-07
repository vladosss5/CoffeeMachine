using System.Net;
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
using NUnit.Framework.Legacy;

namespace CoffeeMachine.IntegrationTests;

/// <summary>
/// Тестирование MachineController.
/// </summary>
[TestFixture]
public class MachineControllerTests  : BaseTest
{
    /// <summary>
    /// Тест получения всех кофемашин.
    /// </summary>
    [Test]
    public async Task GetAllMachines_SendRequest_StatusCodeOk()
    {
        //Arrange
        _dataContext.Database.EnsureCreated();
        var verifyMachine = new List<Machine>
        {
            new Machine { Id = 1, SerialNumber = "11", Description = "wdw", Balance = 0 }
        };
        
        //Act
        _client.DefaultRequestHeaders.Add("Authorization", "Bearer " + _adminToken);
        var response = await _client.GetAsync("/api/Machine");
        var machinesString = await response.Content.ReadAsStringAsync();
        var machines = JsonConvert.DeserializeObject<List<Machine>>(machinesString);
        
        //Assert
        ClassicAssert.AreEqual(response.StatusCode, HttpStatusCode.OK);

        for (int i = 0; i < verifyMachine.Count; i++)
        {
            ClassicAssert.AreEqual(verifyMachine[i].Id, machines[i].Id);
            ClassicAssert.AreEqual(verifyMachine[i].SerialNumber, machines[i].SerialNumber);
            ClassicAssert.AreEqual(verifyMachine[i].Description, machines[i].Description);
            ClassicAssert.AreEqual(verifyMachine[i].Balance, machines[i].Balance);
        }
        
        _dataContext.Database.EnsureDeleted();
    }
    
    /// <summary>
    /// Тест получения кофемашины по Id.
    /// </summary>
    [Test]
    public async Task GetMachineById_SendRequest_StatusCodeOk()
    {
        //Arrange
        _dataContext.Database.EnsureCreated();
        var verifyMachine = new Machine { Id = 1, SerialNumber = "11", Description = "wdw", Balance = 0 };
        
        //Act
        _client.DefaultRequestHeaders.Add("Authorization", "Bearer " + _adminToken);
        var response = await _client.GetAsync($"/api/Machine/{verifyMachine.Id}");
        var machineString = await response.Content.ReadAsStringAsync();
        var machine = JsonConvert.DeserializeObject<Machine>(machineString);
        
        //Assert
        ClassicAssert.AreEqual(response.StatusCode, HttpStatusCode.OK);
        ClassicAssert.AreEqual(verifyMachine.Id, machine.Id);
        ClassicAssert.AreEqual(verifyMachine.SerialNumber, machine.SerialNumber);
        ClassicAssert.AreEqual(verifyMachine.Description, machine.Description);
        ClassicAssert.AreEqual(verifyMachine.Balance, machine.Balance);
        
        _dataContext.Database.EnsureDeleted();
    }
    
    /// <summary>
    /// Тест создания кофемашины.
    /// </summary>
    [Test]
    public async Task CreateMachine_SendRequest_StatusCodeOk()
    {
        //Arrange
        _dataContext.Database.EnsureCreated();
        var verifyMachine = new Machine { Id = 2, SerialNumber = "22", Description = "dfd", Balance = 0 };
        
        //Act
        _client.DefaultRequestHeaders.Add("Authorization", "Bearer " + _adminToken);
        var response = await _client.PostAsJsonAsync("/api/Machine", new Machine { SerialNumber = "22", Description = "dfd" });
        var machineString = await response.Content.ReadAsStringAsync();
        var machine = JsonConvert.DeserializeObject<Machine>(machineString);
        
        //Assert
        ClassicAssert.AreEqual(response.StatusCode, HttpStatusCode.OK);
        ClassicAssert.AreEqual(verifyMachine.Id, machine.Id);
        ClassicAssert.AreEqual(verifyMachine.SerialNumber, machine.SerialNumber);
        ClassicAssert.AreEqual(verifyMachine.Description, machine.Description);
        ClassicAssert.AreEqual(verifyMachine.Balance, machine.Balance);
        
        _dataContext.Database.EnsureDeleted();
    }
    
    /// <summary>
    /// Тест обновления кофемашины.
    /// </summary>
    [Test]
    public async Task UpdateMachine_SendRequest_StatusCodeOk()
    {
        //Arrange
        _dataContext.Database.EnsureCreated();
        var verifyMachine = new Machine { Id = 1, SerialNumber = "33", Description = "wdw", Balance = 1000 };
        
        //Act
        _client.DefaultRequestHeaders.Add("Authorization", "Bearer " + _adminToken);
        var response = await _client.PutAsJsonAsync($"/api/Machine/", new Machine { Id = 1, SerialNumber = "33", Description = "wdw", Balance = 1000 });
        var machineString = await response.Content.ReadAsStringAsync();
        var machine = JsonConvert.DeserializeObject<Machine>(machineString);
        
        //Assert
        ClassicAssert.AreEqual(response.StatusCode, HttpStatusCode.OK);
        ClassicAssert.AreEqual(verifyMachine.Id, machine.Id);
        ClassicAssert.AreEqual(verifyMachine.SerialNumber, machine.SerialNumber);
        ClassicAssert.AreEqual(verifyMachine.Description, machine.Description);
        
        _dataContext.Database.EnsureDeleted();
    }
    
    /// <summary>
    /// Тест удаления кофемашины.
    /// </summary>
    [Test]
    public async Task DeleteMachine_SendRequest_StatusCodeOk()
    {
        //Arrange
        _dataContext.Database.EnsureCreated();
        
        //Act
        _client.DefaultRequestHeaders.Add("Authorization", "Bearer " + _adminToken);
        var response = await _client.DeleteAsync($"/api/Machine/1");
        
        //Assert
        ClassicAssert.AreEqual(response.StatusCode, HttpStatusCode.NoContent);
        
        _dataContext.Database.EnsureDeleted();
    }
    
    /// <summary>
    /// Тест добавления кофе в кофемашину.
    /// </summary>
    [Test]
    public async Task AddCoffeeToMachines_SendRequest_StatusCodeOk()
    {
        //Arrange
        _dataContext.Database.EnsureCreated();
        
        //Act
        _client.DefaultRequestHeaders.Add("Authorization", "Bearer " + _adminToken);
        var response = await _client.PostAsJsonAsync($"/api/Machine/AddCoffeeToMachines/1", 1);
        var responseString = await response.Content.ReadAsStringAsync();
        var coffeeToMachine = JsonConvert.DeserializeObject<CoffeesInMachineDto>(responseString);
        
        //Assert
        ClassicAssert.AreEqual(response.StatusCode, HttpStatusCode.OK);
        ClassicAssert.AreEqual(1, coffeeToMachine.Machine.Id);
        ClassicAssert.AreEqual(1, coffeeToMachine.Coffees.FirstOrDefault(x => x.Id == 1).Id);
        
        _dataContext.Database.EnsureDeleted();
    }
    
    /// <summary>
    /// Тест удаления кофе из кофемашины.
    /// </summary>
    [Test]
    public async Task DeleteCoffeeFromMachines_SendRequest_StatusCodeOk()
    {
        //Arrange
        _dataContext.Database.EnsureCreated();
        
        //Act
        _client.DefaultRequestHeaders.Add("Authorization", "Bearer " + _adminToken);
        var response = await _client.PostAsJsonAsync($"/api/Machine/DeleteCoffeeFromMachines/1", 1);
        var responseString = await response.Content.ReadAsStringAsync();
        var coffeeToMachine = JsonConvert.DeserializeObject<CoffeesInMachineDto>(responseString);
        
        //Assert
        ClassicAssert.AreEqual(response.StatusCode, HttpStatusCode.OK);
        ClassicAssert.AreEqual(0, coffeeToMachine.Coffees.Count);
        
        _dataContext.Database.EnsureDeleted();
    }

    /// <summary>
    /// Тест добавления банкнот в кофемашину.
    /// </summary>
    [Test]
    public async Task AddBanknotesToMachines_SendRequest_StatusCodeOk()
    {
        //Arrange
        _dataContext.Database.EnsureCreated();
        
        //Act
        _client.DefaultRequestHeaders.Add("Authorization", "Bearer " + _adminToken);
        var response = await _client.PostAsJsonAsync($"/api/Machine/AddBanknotesToMachines/1", 
            new List<Banknote>
            {
                new Banknote{Nominal = 500},
                new Banknote{Nominal = 500}
            });
        var responseString = await response.Content.ReadAsStringAsync();
        var machine = JsonConvert.DeserializeObject<Machine>(responseString);
        
        //Assert
        ClassicAssert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        ClassicAssert.AreEqual(87680, machine.Balance);
        
        _dataContext.Database.EnsureDeleted();
    }

    /// <summary>
    /// Тест вычитания банкнот из кофемашины.
    /// </summary>
    [Test]
    public async Task SubtractBanknotesFromMachines_SendRequest_StatusCodeOk()
    {
        //Arrange
        _dataContext.Database.EnsureCreated();
        
        //Act
        _client.DefaultRequestHeaders.Add("Authorization", "Bearer " + _adminToken);
        var response = await _client.PostAsJsonAsync($"/api/Machine/DeleteBanknotesFromMachines/1", 
            new List<Banknote>
            {
                new Banknote{Nominal = 500},
                new Banknote{Nominal = 500}
            });
        var responseString = await response.Content.ReadAsStringAsync();
        var machine = JsonConvert.DeserializeObject<Machine>(responseString);
        
        //Assert
        ClassicAssert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        ClassicAssert.AreEqual(85680, machine.Balance);
        
        _dataContext.Database.EnsureDeleted();
    }

    /// <summary>
    /// Тест получения списка банкнот в кофемашине.
    /// </summary>
    [Test]
    public async Task GetBanknotesByMachine_SendRequest_StatusCodeOk()
    {
        //Arrange
        _dataContext.Database.EnsureCreated();
        
        //Act
        _client.DefaultRequestHeaders.Add("Authorization", "Bearer " + _adminToken);
        var response = await _client.GetAsync($"/api/Machine/GetBanknotesByMachine/1");
        var responseString = await response.Content.ReadAsStringAsync();
        var banknotesToMachines = JsonConvert.DeserializeObject<List<BanknoteToMachine>>(responseString);
        
        //Assert
        ClassicAssert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        foreach (var banknoteToMachine in banknotesToMachines)
        {
            ClassicAssert.AreEqual(10, banknoteToMachine.CountBanknote);
        }
        
        _dataContext.Database.EnsureDeleted();
    }

    /// <summary>
    /// Получение списка кофе из кофемашины.
    /// </summary>
    [Test]
    public async Task GetCoffeesFromMachine_SendRequest_StatusCodeOk()
    {
        //Arrange
        _dataContext.Database.EnsureCreated();
        
        //Act
        _client.DefaultRequestHeaders.Add("Authorization", "Bearer " + _adminToken);
        var response = await _client.GetAsync($"/api/Machine/GetCoffeesFromMachine/1");
        var responseString = await response.Content.ReadAsStringAsync();
        var coffees = JsonConvert.DeserializeObject<List<Coffee>>(responseString);
        
        //Assert
        ClassicAssert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        ClassicAssert.AreEqual(1, coffees[0].Id);
        
        _dataContext.Database.EnsureDeleted();
    }
}