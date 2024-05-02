using Microsoft.Extensions.DependencyInjection;

namespace CoffeeMachine.Application.Extensions;

public static class DIExtensions
{
    /// <summary>
    /// Добавления автомапера.
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    public static IServiceCollection AddApplicationCore(this IServiceCollection services) =>
        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
}