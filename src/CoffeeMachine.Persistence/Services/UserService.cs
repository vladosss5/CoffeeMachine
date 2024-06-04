using CoffeeMachine.Application.Exceptions;
using CoffeeMachine.Application.Interfaces;
using CoffeeMachine.Application.Interfaces.IServices;
using CoffeeMachine.Core.Models;
using Newtonsoft.Json;

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
        if (await _unitOfWork.User.GetByLoginAsync(user.Login) != null)
            throw new AlreadyExistsException(nameof(User), user.Login);
        
        var role = await _unitOfWork.Role.GetByIdAsync(user.Role.Id);
        var hashedPassword = await _passwordHasher.GenerateHashAsync(user.PasswordHash);
        var newUser = new User
        {
            Login = user.Login,
            PasswordHash = hashedPassword,
            Role = role
        };
        
        var createdUser = await _unitOfWork.User.AddAsync(newUser);
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
        var user = await _unitOfWork.User.GetByLoginAsync(login);

        if (user == null)
            throw new LoginException();

        if (!await _passwordHasher.ValidateHashAsync(password, user.PasswordHash))
            throw new LoginException();
        
        var client = new HttpClient();
        string url = "http://localhost:8080/realms/TestRealm/protocol/openid-connect/token";
        
        var content = new FormUrlEncodedContent(new[]
        {
            new KeyValuePair<string, string>("grant_type", "password"),
            new KeyValuePair<string, string>("client_id", "test-client"),
            new KeyValuePair<string, string>("username", "test"),
            new KeyValuePair<string, string>("password", "test"),
            new KeyValuePair<string, string>("client_secret", "NzVbv4eJ8ncga6cdunKFdl1HMXfvwrSz"),
            new KeyValuePair<string, string>("scope", "roles")
        });

        var response = await client.PostAsync(url, content);
        var stringResponse = await response.Content.ReadAsStringAsync();
        var token = JsonConvert.DeserializeObject<Dictionary<string, string>>(stringResponse)["access_token"];
        
        return token;
    }
}