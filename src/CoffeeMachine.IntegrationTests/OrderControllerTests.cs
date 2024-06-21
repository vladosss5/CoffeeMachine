using System.Net;
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
public class OrderControllerTests : BaseTest
{
    /// <summary>
    /// Тест создания заказа.
    /// </summary>
    [Test]
    public async Task CreateOrder_SendRequest_StatusCodeOk()
    {
        //Arrange
        _dataContext.Database.EnsureCreated();
        
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
        var sumDelivery = 0;
        
        //Act
        _client.DefaultRequestHeaders.Add("Authorization", "Bearer " + _adminToken);
        var response = await _client.PostAsJsonAsync("/api/order", requestOrder);
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
        
        _dataContext.Database.EnsureDeleted();
    }

    /// <summary>
    /// Тест получения заказа по Id.
    /// </summary>
    [Test]
    public async Task GetOrderById_SendRequest_StatusCodeOk()
    {
        //Arrange
        _dataContext.Database.EnsureCreated();
        
        //Act
        _client.DefaultRequestHeaders.Add("Authorization", "Bearer " + _adminToken);
        var response = await _client.GetAsync($"/api/order/{_order.Id}");
        var responseString = await response.Content.ReadAsStringAsync();
        var responseOrder = JsonConvert.DeserializeObject<OrderResponseDto>(responseString);
        
        //Assert
        ClassicAssert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        ClassicAssert.AreEqual(_order.Id, responseOrder.Id);
        
        _dataContext.Database.EnsureDeleted();
    }
}