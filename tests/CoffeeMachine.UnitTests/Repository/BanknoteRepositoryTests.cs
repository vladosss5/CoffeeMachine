using System.Data.Common;
using CoffeeMachine.Core.Models;
using CoffeeMachine.Persistence.Data.Context;
using CoffeeMachine.Persistence.Repositories;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace CoffeeMachine.UnitTests.Repository;

public class BanknoteRepositoryTests 
{
    [Test]
    public async Task TestGetByNominalAsync()
    {
        var data = new Data();
        using var context = data.CreateContext();
        var repository = new BanknoteRepository(context);
        
        var banknote = await repository.GetByNominalAsync(100);
        
        Assert.AreEqual(100, banknote.Nominal);
        Assert.AreEqual(2, banknote.Id);
    }

    [Test]
    public async Task GetBanknotesByMachineAsync()
    {
        var data = new Data();
        using var context = data.CreateContext();
        var repository = new BanknoteRepository(context);
    }
}