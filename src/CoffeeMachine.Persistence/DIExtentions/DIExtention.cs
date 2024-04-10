using CoffeeMachine.Infrastructure.Interfaces.IRepositories;
using CoffeeMachine.Infrastructure.Interfaces.IServices;
using CoffeeMachine.Persistence.Data.Context;
using CoffeeMachine.Persistence.Repositories;
using CoffeeMachine.Persistence.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CoffeeMachine.Persistence.DIExtentions;

public static class DIExtentions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection"); //Для локальной бд
        // var connectionString = configuration.GetConnectionString("ConnectionPostgresContainer"); //Для Docker-compose

        services.AddDbContext<MyDbContext>(options => options.UseNpgsql(connectionString));

        services.AddScoped<IBanknoteService, BanknoteService>();
        services.AddScoped<ICoffeeService, CoffeeService>();
        services.AddScoped<IMachineService, MachineService>();
        services.AddScoped<ITransactionService, TransactionService>();
        services.AddScoped<IPurechaseService, PurechaseService>();
        
        services.AddScoped<IMachineRepository, MachineRepository>();
        services.AddScoped<ICoffeeRepository, CoffeeRepository>();
        services.AddScoped<IBanknoteRepository, BanknoteRepository>();
        services.AddScoped<ITransactionRepository, TransactionRepository>();
        services.AddScoped<IPurechaseRepository, PurechaseRepository>();
        
        return services;
    }
}