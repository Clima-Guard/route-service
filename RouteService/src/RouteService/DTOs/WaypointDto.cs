using Microsoft.AspNetCore.Mvc;
using RouteService.Validators;

namespace RouteService.DTOs;

[WaypointValidator]
public class WaypointDto
{
    [FromBody]
    public double? Latitude { get; set; }
    [FromBody]
    public double? Longitude { get; set; }
    [FromBody]
    public string? Address { get; set; }

    public WaypointDto()
    {
    }

    public WaypointDto(double? latitude, double? longitude, string? address)
    {
        Latitude = latitude;
        Longitude = longitude;
        Address = address;
    }
}