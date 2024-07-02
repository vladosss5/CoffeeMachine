namespace CoffeeMachine.API;

using CoffeeMachine.API.Middlewares;
using CoffeeMachine.Application.Extensions;
using CoffeeMachine.Persistence.Extentions;
using Serilog;

/// <summary>
/// Класс для конфигурации приложения.
/// </summary>
public class Startup
{
    /// <summary>
    /// Конфигурация проекта.
    /// </summary>
    private IConfiguration _configuration { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="Startup"/> class.
    /// </summary>
    /// <param name="_configuration">Конфигурация проекта.</param>
    public Startup(IConfiguration configuration)
    {
        _configuration = configuration;
        var logger = Log.Logger = new LoggerConfiguration()
            .Enrich.FromLogContext()
            .WriteTo.Console()
            .WriteTo.File($"{Environment.CurrentDirectory}/Logs/{DateTime.UtcNow:yyyy/dd/MM}.txt")
            .CreateLogger();
        logger.Information("Starting web host");
    }

    /// <summary>
    /// Конфигурация сервисов.
    /// </summary>
    /// <param name="services">Сервисы проекта.</param>
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddControllers();
        services.AddApplicationCore();
        services.AddInfrastructure(_configuration);
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
        services.AddKeycloakAuthentication(_configuration);
        services.AddCustomAuthorization();
    }

    /// <summary>
    /// Конфигурация проекта.
    /// </summary>
    /// <param name="app">Приложение.</param>
    /// <param name="env">Окружение.</param>
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        app.UseSerilogRequestLogging();

        if (env.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseCustomExceptionHandler();
        app.UseRouting();
        app.UseHttpsRedirection();
        app.UseAuthentication();
        app.UseAuthorization();
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }
}