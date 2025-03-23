using System.ComponentModel.DataAnnotations;
using RouteService.DTOs;

namespace RouteService.Validators;

public class WaypointValidator: ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (value is WaypointDto waypoint)
        {
            bool hasCoordinates = waypoint.Latitude.HasValue && waypoint.Longitude.HasValue;
            bool hasAddress = !string.IsNullOrWhiteSpace(waypoint.Address);

            if (hasCoordinates == hasAddress)
            {
                return new ValidationResult("A waypoint must have either coordinates or an address.");
            }
            return ValidationResult.Success;
        }
        return new ValidationResult("Invalid waypoint");
    }
}