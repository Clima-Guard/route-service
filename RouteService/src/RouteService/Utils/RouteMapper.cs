using RouteService.DTOs;
using Route = Google.Maps.Routing.V2.Route;

namespace RouteService.Utils;

public class RouteMapper: IRouteMapper
{
    public RouteResponse RouteToRouteResponse(Route route)
    {
        return new RouteResponse(route.DistanceMeters, (int)route.Duration.Seconds, route.Polyline.EncodedPolyline);
    }
}