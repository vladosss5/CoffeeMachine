using CoffeeMachine.Core.Models;
using CoffeeMachine.Persistence.Repositories;

namespace CoffeeMachine.UnitTests.Repository;

[TestFixture]
public class CoffeeRepositoryTest
{
    [Test]
    public async Task TestGetAllAsync()
    {
        var data = new Data();
        using var context = data.CreateContext();
        var repository = new CoffeeRepository(context);
        
        var coffees = await repository.GetAllAsync();
        
        var expected = new List<Coffee>
        {
            new Coffee{Id = 1, Name = "Cappuccino", Price = 500},
            new Coffee{Id = 2, Name = "Latte", Price = 400},
            new Coffee{Id = 3, Name = "Espresso", Price = 300}
        };

        for (int i = 0; i < expected.Count; ++i)
        {
            Assert.AreEqual(expected[i].Name, coffees.ToList()[i].Name);
        }
    }
    
    [Test]
    public async Task TestGetByIdAsync()
    {
        var data = new Data();
        using var context = data.CreateContext();
        var repository = new CoffeeRepository(context);
        
        var coffee = await repository.GetByIdAsync(1);
        
        Assert.AreEqual("Cappuccino", coffee.Name);
    }
    
    [Test]
    public async Task TestAdd()
    {
        var data = new Data();
        using var context = data.CreateContext();
        var repository = new CoffeeRepository(context);
        
        var coffee = await repository.AddAsync(new Coffee{Id = 4, Name = "Americano", Price = 300});
        
        Assert.AreEqual("Americano", coffee.Name);
    }
    
    [Test]
    public async Task TestUpdateAsync()
    {
        var data = new Data();
        using var context = data.CreateContext();
        var repository = new CoffeeRepository(context);
        
        var coffee = await repository.UpdateAsync(new Coffee
        {
            Id = 1, Name = "Americano", Price = 555
        });
        
        Assert.AreEqual("Americano", coffee.Name);
        Assert.AreEqual(555, coffee.Price);
    }
    
    [Test]
    public async Task TestDeleteAsync()
    {
        var data = new Data();
        using var context = data.CreateContext();
        var repository = new CoffeeRepository(context);
        
        await repository.DeleteAsync(new Coffee
        {
            Id = 1, Name = "Cappuccino", Price = 500
        });
        
        var result = await repository.GetByIdAsync(1);
        
        Assert.IsNull(result);
    }
}