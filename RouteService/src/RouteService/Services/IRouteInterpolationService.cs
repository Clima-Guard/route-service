using PolylinerNet;
using RouteService.DTOs;

namespace RouteService.Services;

public interface IRouteInterpolationService
{
    IList<IList<PolylinePoint>> GetInterpolatedPointsForRoutes(RouteRequest request);
}