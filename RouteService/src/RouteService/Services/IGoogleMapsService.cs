using RouteService.DTOs;

namespace RouteService.Services;

public interface IGoogleMapsService
{
    Task<IList<RouteResponse>> GetRouteDetails(RouteRequest request);
}