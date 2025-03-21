using PolylinerNet;
using RouteService.Validators;

namespace RouteService.DTOs;

[WaypointValidator]
public class WaypointDto
{
    public PolylinePoint? Coordinates { get; set; }
    public string? Address { get; set; }

    public WaypointDto()
    {
    }

    public WaypointDto(PolylinePoint? coordinates, string? address)
    {
        Coordinates = coordinates;
        Address = address;
    }
}