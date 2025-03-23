using Google.Maps.Routing.V2;
using RouteService.DTOs;

namespace RouteService.Utils;

public interface IWaypointFactory
{
    Waypoint Create(WaypointDto waypointDto);
    WaypointDto Parse(string waypointAsString); 
}