using Google.Protobuf.Collections;
using RouteService.DTOs;
using Route = Google.Maps.Routing.V2.Route;

namespace RouteService.Services;

public interface IGoogleMapsService
{
    IList<RouteResponse> GetRouteDetails(RouteRequest request);
}