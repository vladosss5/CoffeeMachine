namespace CoffeeMachine.API;

using Middlewares;
using Application.Extensions;
using Persistence.Extentions;
using Serilog;

/// <summary>
/// Основной класс проекта.
/// </summary>
public class Program
{
    /// <summary>
    /// Основной метод.
    /// </summary>
    /// <param name="args"> Аргументы. </param>
    public static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        var services = builder.Services;
        var configuration = builder.Configuration;

        var logger = Log.Logger = new LoggerConfiguration()
            .Enrich.FromLogContext()
            .WriteTo.Console()
            .WriteTo.File($"{Environment.CurrentDirectory}/Logs/{DateTime.UtcNow:yyyy/dd/MM}.txt")
            .CreateLogger();

        logger.Information("Starting web host");

        services.AddControllers();
        services.AddApplicationCore();
        services.AddInfrastructure(configuration);
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
        services.AddKeycloakAuthentication(configuration);
        services.AddCustomAuthorization();

        var app = builder.Build();

        if (app.Environment.IsDevelopment())
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

        app.Run();
    }
}