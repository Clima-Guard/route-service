﻿using System.ComponentModel.DataAnnotations;

namespace RouteService.Validators;

public class NotEmptyIfProvided: ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (value is null)
            return new ValidationResult("Cannot be null");
        if (value is List<string> list)
        {
            if (list.Count != 0 && list.Any(string.IsNullOrWhiteSpace))
            {
                return new ValidationResult("Cannot contain empty or whitespace values if provided");
            }
        }
        return ValidationResult.Success;
    }
}