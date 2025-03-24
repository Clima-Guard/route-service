using PolylinerNet;
using RouteService.DTOs;

namespace RouteService.Services;

public interface IRouteInterpolationService
{
    Task<IList<IList<PolylinePoint>>> GetInterpolatedPointsForRoutes(RouteRequest request);
}