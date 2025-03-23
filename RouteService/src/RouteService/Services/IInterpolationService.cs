using PolylinerNet;

namespace RouteService.Services;

public interface IInterpolationService
{
    IList<PolylinePoint> GetInterpolatedPointsAlongRouteAtGivenDistance(
        IList<PolylinePoint> pointsAlongRoute, 
        double routeDistance,
        double distanceBetweenPoints);
}