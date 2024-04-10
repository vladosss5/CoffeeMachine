using Microsoft.Extensions.DependencyInjection;

namespace CoffeeMachine.Infrastructure.Extensions;

public static class DIExtensions
{
    public static IServiceCollection AddApplicationCore(this IServiceCollection services) =>
        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
}