using CoffeeMachine.Application.Interfaces;
using CoffeeMachine.Application.Interfaces.IRepositories;
using CoffeeMachine.Application.Interfaces.IServices;
using CoffeeMachine.Persistence.Data.Context;
using CoffeeMachine.Persistence.Repositories;
using CoffeeMachine.Persistence.Services;
using Keycloak.AuthServices.Authentication;
using Keycloak.AuthServices.Authorization;
using Keycloak.AuthServices.Common;
using Keycloak.AuthServices.Sdk.Kiota;
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
        
        services.AddScoped<IMachineRepository, MachineRepository>();
        services.AddScoped<ICoffeeRepository, CoffeeRepository>();
        services.AddScoped<IBanknoteRepository, BanknoteRepository>();
        services.AddScoped<ITransactionRepository, TransactionRepository>();
        services.AddScoped<IOrderRepository, OrderRepository>();
        
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
        });

        // services
        //     .AddAuthorization(o =>
        //         o.AddPolicy(
        //             "IsAdmin",
        //             b =>
        //             {
        //                 b.RequireRealmRoles("admin");
        //                 b.RequireResourceRoles("r-admin");
        //                 // TokenValidationParameters.RoleClaimType is overridden
        //                 // by KeycloakRolesClaimsTransformation
        //                 b.RequireRole("r-admin");
        //             }
        //         )
        //     )
        //     .AddKeycloakAuthorization(configuration)
        //     .AddAuthorizationServer(configuration);
        //
        // services.AddKeycloakAdminHttpClient(configuration);
        
        return services;
    }

    public static IServiceCollection AddCustomAuthorization(this IServiceCollection services)
    {
        services
            .AddAuthorization()
            .AddKeycloakAuthorization(options =>
            {
                options.EnableRolesMapping = RolesClaimTransformationSource.Realm;
                options.RoleClaimType = KeycloakConstants.RoleClaimType;
            })
            .AddAuthorizationBuilder()
            .AddPolicy(
                "AdminPolicy",
                policy => policy.RequireRole("Admin")
            )
            .AddPolicy(
                "DefaultPolicy", 
                policy => policy.RequireRole("Default"));

        return services;
    }
}