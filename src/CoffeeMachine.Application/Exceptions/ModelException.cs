using System.ComponentModel.DataAnnotations;

namespace CoffeeMachine.Application.Exceptions;

public class ModelException : Exception
{
    public List<ValidationResult> Errors { get; set; }
    public ModelException(List<ValidationResult> validationResults)
    {
        Errors = validationResults;
    }
}