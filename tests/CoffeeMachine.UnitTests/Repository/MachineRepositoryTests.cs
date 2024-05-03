using CoffeeMachine.Persistence.Repositories;

namespace CoffeeMachine.UnitTests.Repository;

public class MachineRepositoryTests
{
    [Test]
    public async Task TestGetBySerialNumberAsync()
    {
        var data = new Data();
        using var context = data.CreateContext();
        var repository = new MachineRepository(context);
        
        var machine = await repository.GetBySerialNumberAsync("11");
        
        Assert.AreEqual(1, machine.Id);
    }
}