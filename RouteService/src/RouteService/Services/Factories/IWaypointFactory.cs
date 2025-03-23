using Google.Maps.Routing.V2;
using RouteService.DTOs;

namespace RouteService.Services.Factories;

public interface IWaypointFactory
{
    Waypoint Create(WaypointDto waypointDto);
    WaypointDto Parse(string waypointAsString); 
}