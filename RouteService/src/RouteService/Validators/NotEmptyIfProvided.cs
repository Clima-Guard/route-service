using System.ComponentModel.DataAnnotations;

namespace RouteService.Validators;

public class NotEmptyIfProvided: ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (value is string str && string.IsNullOrWhiteSpace(str))
        {
            return new ValidationResult("Field cannot be empty if provided");
        }
        return ValidationResult.Success;
    }
}