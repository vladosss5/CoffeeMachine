using System.ComponentModel.DataAnnotations;

namespace CoffeeMachine.Application.Exceptions;

/// <summary>
/// Ошибка: модель некорректна.
/// </summary>
public class ModelException : Exception
{
    public List<ValidationResult> Errors { get; set; }
    public ModelException(List<ValidationResult> validationResults)
    {
        Errors = validationResults;
    }
}