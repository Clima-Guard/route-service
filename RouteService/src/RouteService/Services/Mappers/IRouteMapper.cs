using RouteService.DTOs;
using Route = Google.Maps.Routing.V2.Route;

namespace RouteService.Services.Mappers;

public interface IRouteMapper
{
    RouteResponse RouteToRouteResponse(Route route);
}