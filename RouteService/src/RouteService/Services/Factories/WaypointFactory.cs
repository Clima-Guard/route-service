using System.Text.RegularExpressions;
using Google.Maps.Routing.V2;
using Google.Type;
using PolylinerNet;
using RouteService.DTOs;

namespace RouteService.Services.Factories;

public class WaypointFactory: IWaypointFactory
{
    private static readonly Regex CoordinateRegex = new Regex(@"^[-+]?\d*\.?\d+,\s*[-+]?\d*\.?\d+$", RegexOptions.Compiled);
    
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

    public WaypointDto Parse(string waypointAsString)
    {
        if (CoordinateRegex.IsMatch(waypointAsString))
        {
            var parts = waypointAsString.Split(',');
            return new WaypointDto
            {
                Coordinates = new PolylinePoint(
                    double.Parse(parts[0].Trim()),
                    double.Parse(parts[1].Trim())
                    )
            };
        }

        return new WaypointDto
        {
            Address = waypointAsString.Trim()
        };
    }
}