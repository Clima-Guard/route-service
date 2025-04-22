using System.ComponentModel.DataAnnotations;

namespace RouteService.Validators;

public class MaxNumberElements: ValidationAttribute
{
    private readonly int _maxNumberElements;

    public MaxNumberElements(int maxNumberElements)
    {
        _maxNumberElements = maxNumberElements;
    }

    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (value is null)
            return new ValidationResult("Cannot be null");
        if (value is List<string> list)
        {
            if (list.Count > _maxNumberElements)
                return new ValidationResult($"Cannot contain more than {_maxNumberElements} elements");
        }
        return ValidationResult.Success;
    }
}