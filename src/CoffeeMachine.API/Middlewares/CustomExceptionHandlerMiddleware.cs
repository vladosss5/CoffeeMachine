using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Text.Json;
using CoffeeMachine.Application.Exceptions;

namespace CoffeeMachine.API.Middlewares;

/// <summary>
/// Обработки исключений.
/// </summary>
public class CustomExceptionHandlerMiddleware
{
    /// <summary>
    /// Ссылка на следующий объект в конвейере.
    /// </summary>
    private readonly RequestDelegate _next;
    
    /// <summary>
    /// Сервис логирования.
    /// </summary>
    private readonly ILogger<CustomExceptionHandlerMiddleware> _logger;

    /// <summary>
    /// Конструктор класса.
    /// </summary>
    /// <param name="next">Ссылка на следующий объект в конвейере.</param>
    /// <param name="logger">Сервис логирования.</param>
    public CustomExceptionHandlerMiddleware(RequestDelegate next, ILogger<CustomExceptionHandlerMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    /// <summary>
    /// Отлов исключений.
    /// </summary>
    /// <param name="context">Специфичная информация об отдельном HTTP-запросе.</param>
    public async Task InvokeAsync(HttpContext context)
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
    /// Обработка исключений.
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