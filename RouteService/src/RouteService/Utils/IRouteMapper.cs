using RouteService.DTOs;
using Route = Google.Maps.Routing.V2.Route;

namespace RouteService.Utils;

public interface IRouteMapper
{
    RouteResponse RouteToRouteResponse(Route route);
}