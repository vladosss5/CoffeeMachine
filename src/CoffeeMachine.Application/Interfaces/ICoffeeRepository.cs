using CoffeeMachine.Domain.Models;

namespace CoffeeMachine.Infrastructure.Interfaces;

public interface ICoffeeRepository
{
    public Task<Coffee> GetByName(string nameCoffe);
    public Task<List<Coffee>> GetAllCoffees();
    public Task<Coffee> Add(Coffee coffee);
    public Task<Coffee> Update(Coffee coffee);
    public Task<Coffee> Delete(Coffee coffee);
}