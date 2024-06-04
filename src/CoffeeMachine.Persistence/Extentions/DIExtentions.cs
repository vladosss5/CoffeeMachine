using CoffeeMachine.Application.Interfaces;
using CoffeeMachine.Application.Interfaces.IRepositories;
using CoffeeMachine.Application.Interfaces.IServices;
using CoffeeMachine.Persistence.Data.Context;
using CoffeeMachine.Persistence.Repositories;
using CoffeeMachine.Persistence.Services;
using Keycloak.AuthServices.Authentication;
using Keycloak.AuthServices.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace CoffeeMachine.Persistence.Extentions;

public static class DIExtentions
{
    /// <summary>
    /// Натсройка подключения к БД, связывание интерфейсов с реализациейми.
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configuration"></param>
    /// <returns>Сервисы с контекстом БД и связанными интерфейсами</returns>
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, 
        IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");

        services.AddDbContext<DataContext>(options => options.UseNpgsql(connectionString));

        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IPasswordHasher, PasswordHasher>();
        
        services.AddScoped<IOrderService, OrderService>();
        services.AddScoped<IAdminService, AdminService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IRoleService, RoleService>();
        
        services.AddScoped<IMachineRepository, MachineRepository>();
        services.AddScoped<ICoffeeRepository, CoffeeRepository>();
        services.AddScoped<IBanknoteRepository, BanknoteRepository>();
        services.AddScoped<ITransactionRepository, TransactionRepository>();
        services.AddScoped<IOrderRepository, OrderRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IRoleRepository, RoleRepository>();
        
        return services;
    }
    
    /// <summary>
    /// Настройка Swagger
    /// </summary>
    /// <param name="services">Серивисы.</param>
    /// <returns>Сервисы с настройками Swagger</returns>
    public static IServiceCollection AddSwaggerGen(this IServiceCollection services)
    {
        services.AddSwaggerGen(options => 
        {
            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Name = "Authorization",
                In = ParameterLocation.Header,
                Scheme = "Bearer"
            });
            options.AddSecurityRequirement(new OpenApiSecurityRequirement()
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        },
                        Scheme = "oauth2",
                        Name = "Bearer",
                        In = ParameterLocation.Header
                    },
                    new List<string>()
                }
            });
        });
        
        return services;
    }

    public static IServiceCollection AddKeycloakAuthentication(
        this IServiceCollection services, IConfiguration configuration)
    {
        services.AddKeycloakWebApiAuthentication(configuration, options =>
        {
            options.RequireHttpsMetadata = false;
            options.Audience = "test-client";
            options.SaveToken = true;
        });
        return services;
    }

    public static IServiceCollection AddCustomAuthorization(this IServiceCollection services)
    {
        services.AddAuthorization(options =>
        {
            options.AddPolicy("AdminPolicy", policy => policy.RequireClaim("Role", "Admin"));
            options.AddPolicy("UserPolicy", policy => policy.RequireClaim("Role", "Default"));
        });

        return services;
    }
}