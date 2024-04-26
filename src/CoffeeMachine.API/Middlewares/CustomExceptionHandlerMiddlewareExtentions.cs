namespace CoffeeMachine.API.Middlewares;

public static class CustomExceptionHandlerMiddlewareExtentions
{
    /// <summary>
    /// Метод расширения добавлябщий миддлвейр для обработки исключений.
    /// </summary>
    /// <param name="builder"></param>
    /// <returns></returns>
    public static IApplicationBuilder UseCustomExceptionHandler(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<CustomExceptionHandlerMiddleware>();
    }
}