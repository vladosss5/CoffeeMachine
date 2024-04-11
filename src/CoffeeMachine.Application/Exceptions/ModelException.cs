using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CoffeeMachine.Infrastructure.Exceptions;

public class ModelException : Exception
{
    public List<ValidationResult> Errors { get; set; }
    public ModelException(List<ValidationResult> validationResults)
    {
        Errors = validationResults;
    }
}