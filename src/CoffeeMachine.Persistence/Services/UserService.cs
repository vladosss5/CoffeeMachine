using CoffeeMachine.Application.Interfaces;
using CoffeeMachine.Application.Interfaces.IServices;
using CoffeeMachine.Core.Models;

namespace CoffeeMachine.Persistence.Services;

public class UserService : IUserService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IPasswordHasher _passwordHasher;

    public UserService(IUnitOfWork unitOfWork, IPasswordHasher passwordHasher)
    {
        _unitOfWork = unitOfWork;
        _passwordHasher = passwordHasher;
    }
    
    public async Task<IEnumerable<User>> GetAllUsersAsync()
    {
        throw new NotImplementedException();
    }

    public async Task<User> GetUserByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<User> CreateUserAsync(User user)
    {
        var role = await _unitOfWork.Role.GetByIdAsync(user.Role.Id);
        var hashedPassword = await _passwordHasher.GenerateHashAsync(user.Password);
        var newUser = new User
        {
            Login = user.Login,
            Password = hashedPassword,
            Role = role
        };
        
        var createdUser = await _unitOfWork.User.(newUser);
        return createdUser;
    }

    public async Task<User> UpdateUserAsync(User user)
    {
        throw new NotImplementedException();
    }

    public async Task DeleteUserAsync(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<string> Login(string login, string password)
    {
        throw new NotImplementedException();
    }
}