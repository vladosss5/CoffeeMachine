using System.Data.Common;
using CoffeeMachine.Core.Models;
using CoffeeMachine.Persistence.Data.Context;
using CoffeeMachine.Persistence.Repositories;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace CoffeeMachine.UnitTests.RepositoryTests;

[TestFixture]
public class BanknoteRepositoryTests : IDisposable
{
    private readonly DbConnection _connection;
    private readonly DbContextOptions<DataContext> _contextOptions;
    
    #region ConstructorAndDispose
    public BanknoteRepositoryTests()
    {
        _connection = new SqliteConnection("Filename=:memory:");
        _connection.Open();    
        
        _contextOptions = new DbContextOptionsBuilder<DataContext>()
            .UseSqlite(_connection)
            .Options;
        
        using var context = new DataContext(_contextOptions);

        if (context.Database.EnsureCreated())
        {
            using var viewCommand = context.Database.GetDbConnection().CreateCommand();
            viewCommand.CommandText = @"
CREATE VIEW AllResources AS
SELECT Url
FROM CoffeeMachine;";
            viewCommand.ExecuteNonQuery();
        }
        
        context.AddRange(
            new Coffee{Id = 1, Name = "Cappuccino", Price = 500},
            new Coffee{Id = 2, Name = "Latte", Price = 400},
            new Coffee{Id = 3, Name = "Espresso", Price = 300}
            );
        context.SaveChanges();
    }
    
    DataContext CreateContext() => new DataContext(_contextOptions);
    
    public void Dispose() => _connection.Dispose();
    #endregion

    [Test]
    public async Task TestGetAll()
    {
        using var context = CreateContext();
        var repository = new CoffeeRepository(context);
        
        var coffees = await repository.GetAllAsync();
        
        var expected = new List<Coffee>
        {
            new Coffee{Id = 1, Name = "Cappuccino", Price = 500},
            new Coffee{Id = 2, Name = "Latte", Price = 400},
            new Coffee{Id = 3, Name = "Espresso", Price = 300}
        };
        
        Assert.AreEqual(expected[1].Name, coffees.ToList()[1].Name);
        Assert.AreEqual(expected[2].Name, coffees.ToList()[2].Name);
        Assert.AreEqual(expected[3].Name, coffees.ToList()[3].Name);
    }

    [Test]
    public async Task TestGetById()
    {
        using var context = CreateContext();
        var repository = new CoffeeRepository(context);
        
        var coffee = await repository.GetByIdAsync(1);
        
        Assert.AreEqual("Cappuccino", coffee.Name);
    }

    [Test]
    public async Task TestAdd()
    {
        using var context = CreateContext();
        var repository = new CoffeeRepository(context);
        
        var coffee = await repository.AddAsync(new Coffee{Id = 4, Name = "Americano", Price = 300});
        
        Assert.AreEqual("Americano", coffee.Name);
    }
}