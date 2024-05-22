using CoffeeMachine.Application.Exceptions;
using CoffeeMachine.Application.Interfaces;
using CoffeeMachine.Application.Interfaces.IServices;
using CoffeeMachine.Core.Models;

namespace CoffeeMachine.Persistence.Services;

public class AccountService : IAccountService
{
    /// <summary>
    /// <inheritdoc cref="IUnitOfWork"/>
    /// </summary>
    private readonly IUnitOfWork _unitOfWork;

    /// <summary>
    /// Конструктор класса.
    /// </summary>
    /// <param name="unitOfWork">Unit of work.</param>
    public AccountService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<User> Login(string login, string password)
    {
        var user = await _unitOfWork.User.GetByLoginAndPasswordAsync(login, password);
        if (user == null)
            throw new LoginException();
        
        return user;
    }
}