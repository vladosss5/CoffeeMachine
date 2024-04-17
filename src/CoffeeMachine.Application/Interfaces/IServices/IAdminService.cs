using CoffeeMachine.Core.Models;

namespace CoffeeMachine.Application.Interfaces.IServices;

public interface IAdminService
{
    public Task<IEnumerable<Machine>> GetAllMachinesAsync();
    public Task<Machine> CreateNewMachineAsync(Machine machine);
    public Task<bool> DeleteMachineAsync(Machine machine);
    public Task<IEnumerable<Banknote>> GetBanknotesByMachineAsync(Machine machine);
    public Task<Machine> AddBanknotesToMachineAsync(List<Banknote> banknotes, Machine machine);
    public Task<Machine> SubtractBanknotesFromMachineAsync(List<Banknote> banknotes, Machine machine);
    public Task<Machine> AddCoffeeToMachineAsync(Coffee coffee, Machine machine);
    public Task<Machine> SubtractCoffeeFromMachineAsync(Coffee coffee, Machine machine);
    
    public Task<IEnumerable<Coffee>> GetAllCoffeesAsync();
    public Task<Coffee> CreateNewCoffeeAsync(Coffee coffee);
    public Task<bool> DeleteCoffeeAsync(Coffee coffee);
    
    public Task<IEnumerable<Order>> GetAllOrdersAsync();
}