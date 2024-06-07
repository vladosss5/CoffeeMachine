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
using Microsoft.AspNetCore.Authorization;
using Newtonsoft.Json;
using NUnit.Framework.Legacy;

namespace CoffeeMachine.IntegrationTests;

/// <summary>
/// Тестирование CoffeeController.
/// </summary>
[TestFixture]
public class CoffeeControllerTests : BaseTest
{
    /// <summary>
    /// Тест для получения списка кофе.
    /// </summary>
    [Test]
    public async Task GetCoffeeList_SendRequest_StatusCodeOk()
    {
        //Arrange
        _dataContext.Database.EnsureCreated();
        var verifyCoffees = new List<Coffee>
        {
            new Coffee{ Id = 1, Name = "Cappuccino", Price = 836}
        };
        
        //Act
        _client.DefaultRequestHeaders.Add("Authorization", "Bearer " + _adminToken);
        var response = await _client.GetAsync("/api/coffee");
        var responseString = await response.Content.ReadAsStringAsync();
        var coffees = JsonConvert.DeserializeObject<List<Coffee>>(responseString);
        
        //Assert
        ClassicAssert.AreEqual(HttpStatusCode.OK, response.StatusCode);

        for (int i = 0; i < verifyCoffees.Count; i++)
        {
            ClassicAssert.AreEqual(verifyCoffees[i].Name, coffees[i].Name);
            ClassicAssert.AreEqual(verifyCoffees[i].Price, coffees[i].Price);
        }
        
        _dataContext.Database.EnsureDeleted();
    }
    
    /// <summary>
    /// Тест получения кофе по Id.
    /// </summary>
    [Test]
    public async Task GetCoffeeById_SendRequest_StatusCodeOk()
    {
        //Arrange
        _dataContext.Database.EnsureCreated();
        var verifyCoffee = new Coffee { Id = 1, Name = "Cappuccino", Price = 836 };
            
        //Act
        _client.DefaultRequestHeaders.Add("Authorization", "Bearer " + _adminToken);
        var response = await _client.GetAsync("/api/coffee/1");
        var responseString = await response.Content.ReadAsStringAsync();
        var coffee = JsonConvert.DeserializeObject<Coffee>(responseString);
        
        //Assert
        ClassicAssert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        ClassicAssert.AreEqual(verifyCoffee.Id, coffee.Id);
        
        _dataContext.Database.EnsureDeleted();
    }

    /// <summary>
    /// Тест создания кофе.
    /// </summary>
    [Test]
    public async Task CreateCoffee_SendRequest_StatusCodeOk()
    {
        //Arrange
        _dataContext.Database.EnsureCreated();
        var verifyCoffee = new Coffee{Id = 2, Name = "Latte", Price = 900};
        
        //Act
        _client.DefaultRequestHeaders.Add("Authorization", "Bearer " + _adminToken);
        var responseOk = await _client.PostAsJsonAsync("/api/coffee", verifyCoffee);
        var coffeeString = await responseOk.Content.ReadAsStringAsync();
        var coffee = JsonConvert.DeserializeObject<Coffee>(coffeeString);
        
        //Assert
        ClassicAssert.AreEqual(HttpStatusCode.OK, responseOk.StatusCode);
        ClassicAssert.AreEqual(verifyCoffee.Id, coffee.Id);
        ClassicAssert.AreEqual(verifyCoffee.Name, coffee.Name);
        ClassicAssert.AreEqual(verifyCoffee.Price, coffee.Price);

        _dataContext.Database.EnsureDeleted();
    }

    /// <summary>
    /// Тест обновления кофе.
    /// </summary>
    [Test]
    public async Task UpdateCoffee_SendRequest_StatusCodeOk()
    {
        //Arrange
        _dataContext.Database.EnsureCreated();
        var verifyCoffee = new Coffee{Id = 1, Name = "Latte", Price = 800};
        
        //Act
        _client.DefaultRequestHeaders.Add("Authorization", "Bearer " + _adminToken);
        var response = await _client.PutAsJsonAsync("/api/coffee/1", verifyCoffee);
        var coffeeString = await response.Content.ReadAsStringAsync();
        var coffee = JsonConvert.DeserializeObject<Coffee>(coffeeString);
        
        //Assert
        ClassicAssert.AreEqual(response.StatusCode, HttpStatusCode.OK);
        ClassicAssert.AreEqual(verifyCoffee.Id, coffee.Id);
        ClassicAssert.AreEqual(verifyCoffee.Name, coffee.Name);
        ClassicAssert.AreEqual(verifyCoffee.Price, coffee.Price);
        
        _dataContext.Database.EnsureDeleted();
    }
    
    /// <summary>
    /// Тест удаления кофе.
    /// </summary>
    [Test]
    public async Task DeleteCoffee_SendRequest_StatusCodeOk()
    {
        //Arrange
        _dataContext.Database.EnsureCreated();
        //Act
        _client.DefaultRequestHeaders.Add("Authorization", "Bearer " + _adminToken);
        var response = await _client.DeleteAsync("/api/coffee/1");
        
        //Assert
        ClassicAssert.AreEqual(response.StatusCode, HttpStatusCode.NoContent);
        
        _dataContext.Database.EnsureDeleted();
    }
}