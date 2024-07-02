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
    
    public IConfiguration Configuration { get; }
    public IWebHostEnvironment _env { get; set; }

    public Startup(IConfiguration configuration, IWebHostEnvironment env)
    {
        Configuration = configuration;
        var logger = Log.Logger = new LoggerConfiguration()
            .Enrich.FromLogContext()
            .WriteTo.Console()
            .WriteTo.File($"{Environment.CurrentDirectory}/Logs/{DateTime.UtcNow:yyyy/dd/MM}.txt")
            .CreateLogger();
        logger.Information("Starting web host");
    }
    
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddControllers();
        services.AddApplicationCore();
        services.AddInfrastructure(Configuration);
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
        services.AddKeycloakAuthentication(Configuration);
        services.AddCustomAuthorization();   
    }
    
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