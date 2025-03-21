using Google.Maps.Routing.V2;
using Google.Type;
using RouteService.DTOs;

namespace RouteService.Utils;

public class WaypointFactory: IWaypointFactory
{
    public Waypoint Create(WaypointDto waypointDto)
    {
        if (waypointDto.Address != null)
        {
            return new Waypoint
            {
                Address = waypointDto.Address
            };
        }
        if (waypointDto.Coordinates.HasValue)
        {
            return new Waypoint
            {
                Location = new Location
                { 
                    LatLng = new LatLng 
                    { 
                        Latitude = waypointDto.Coordinates.Value.Latitude, 
                        Longitude = waypointDto.Coordinates.Value.Longitude
                    } 
                }
            };
        }
        throw new ArgumentException("Origin address or latitude and longitude are required.");
    }
}