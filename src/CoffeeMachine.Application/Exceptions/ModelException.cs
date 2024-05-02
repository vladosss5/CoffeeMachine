using System.ComponentModel.DataAnnotations;

namespace CoffeeMachine.Application.Exceptions;

/// <summary>
/// Модель некорректна.
/// </summary>
public class ModelException : Exception
{
    /// <summary>
    /// Список ошибок валидации.
    /// </summary>
    public List<ValidationResult> Errors { get; set; }
    
    /// <summary>
    /// Конструктор.
    /// </summary>
    /// <param name="validationResults"></param>
    public ModelException(List<ValidationResult> validationResults)
    {
        Errors = validationResults;
    }
}