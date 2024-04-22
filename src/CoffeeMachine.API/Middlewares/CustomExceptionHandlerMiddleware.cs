using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Text.Json;
using CoffeeMachine.Application.Exceptions;
using Microsoft.AspNetCore.Http.HttpResults;

namespace CoffeeMachine.API.Middlewares;

public class CustomExceptionHandlerMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<CustomExceptionHandlerMiddleware> _logger;

    public CustomExceptionHandlerMiddleware(RequestDelegate next,
        ILogger<CustomExceptionHandlerMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

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
                _logger.LogError(
                    "Error Message: {exceptionMessage}, Time of occurrence {time}",
                    notFoundException.Message, DateTime.UtcNow);
                break;
            
            case AlreadyExistsException alreadyExistsException:
                code = HttpStatusCode.BadRequest;
                _logger.LogError(
                    "Error Message: {exceptionMessage}, Time of occurrence {time}",
                    alreadyExistsException.Message, DateTime.UtcNow);
                break;
        }
        
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)code;

        if (result == string.Empty)
        {
            result = JsonSerializer.Serialize(new { error = exception.Message });
        }
        
        return context.Response.WriteAsync(result);
    }
}