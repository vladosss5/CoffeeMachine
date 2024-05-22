using CoffeeMachine.Application.Interfaces.IRepositories;
using CoffeeMachine.Core.Models;
using CoffeeMachine.Persistence.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace CoffeeMachine.Persistence.Repositories;

public class UserRepository : GenericRepository<User>, IUserRepository
{
    /// <summary>
    /// <inheritdoc cref="DataContext"/>
    /// </summary>
    private readonly DataContext _dataContext;
    
    /// <summary>
    /// Конструктор класса.
    /// </summary>
    /// <param name="dataContext">Контекст для работы с базой данных.</param>
    public UserRepository(DataContext dataContext) : base(dataContext)
    {
        _dataContext = dataContext;
    }
    
    /// <summary>
    /// <inheritdoc cref="IUserRepository.GetByLoginAndPasswordAsync"/>
    /// </summary>
    /// <param name="login">Логин.</param>
    /// <param name="password">Пароль.</param>
    /// <returns>User.</returns>
    public async Task<User> GetByLoginAndPasswordAsync(string login, string password)
    {
        return await _dataContext.Users.FirstOrDefaultAsync(u => u.Login == login && u.Password == password);
    }
}