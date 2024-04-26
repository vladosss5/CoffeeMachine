using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Text.Json;
using CoffeeMachine.Application.Exceptions;
using Microsoft.AspNetCore.Http.HttpResults;

namespace CoffeeMachine.API.Middlewares;

/// <summary>
/// Миддлвейр для обработки исключений.
/// </summary>
public class CustomExceptionHandlerMiddleware
{
    /// <summary>
    /// Ссылка на следующий миддлвейр
    /// </summary>
    private readonly RequestDelegate _next;
    
    /// <summary>
    /// Логгер
    /// </summary>
    private readonly ILogger<CustomExceptionHandlerMiddleware> _logger;

    public CustomExceptionHandlerMiddleware(RequestDelegate next,
        ILogger<CustomExceptionHandlerMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    /// <summary>
    /// Асинхронный метод для отлова исключений.
    /// </summary>
    /// <param name="context"></param>
    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception e)
        {
            await HandleExceptionAsync(context, e);
        }
    }

    /// <summary>
    /// Метод обработки исключений.
    /// </summary>
    /// <param name="context"></param>
    /// <param name="exception"></param>
    /// <returns></returns>
    private Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        var code = HttpStatusCode.InternalServerError;
        var result = string.Empty;

        switch (exception)
        {
            case ValidationException validationException:
                code = HttpStatusCode.BadRequest;
                result = JsonSerializer.Serialize(validationException.ValidationResult.ErrorMessage);
                break;
            
            case NotFoundException notFoundException:
                code = HttpStatusCode.NotFound;
                result = JsonSerializer.Serialize(new { error = notFoundException.Message });
                _logger.LogError(
                    "Error Message: {exceptionMessage}, Time of occurrence {time}",
                    notFoundException.Message, DateTime.UtcNow);
                break;
            
            case AlreadyExistsException alreadyExistsException:
                code = HttpStatusCode.BadRequest;
                result = JsonSerializer.Serialize(new { error = alreadyExistsException.Message });
                _logger.LogError(
                    "Error Message: {exceptionMessage}, Time of occurrence {time}",
                    alreadyExistsException.Message, DateTime.UtcNow);
                break;
            
            default: 
                code = HttpStatusCode.InternalServerError;
                result = JsonSerializer.Serialize(new { error = exception.Message });
                _logger.LogError(
                    "Error Message: {ex}, Time of occurrence {time}", exception.Message, DateTime.UtcNow);
                break;
        }
        
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)code;
        
        return context.Response.WriteAsync(result);
    }
}