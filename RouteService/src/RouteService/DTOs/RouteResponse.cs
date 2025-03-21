namespace RouteService.DTOs;

public class RouteResponse
{
    public int DistanceMeters { get; set; }
    public int Duration { get; set; }
    public string Polyline { get; set; }

    public RouteResponse(int distanceMeters, int duration, string polyline)
    {
        DistanceMeters = distanceMeters;
        Duration = duration;
        Polyline = polyline;
    }
}