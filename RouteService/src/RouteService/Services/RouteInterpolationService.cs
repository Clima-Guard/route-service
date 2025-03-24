using PolylinerNet;
using RouteService.DTOs;

namespace RouteService.Services;

public class RouteInterpolationService: IRouteInterpolationService
{
    private readonly Polyliner _polyliner;
    private readonly IGoogleMapsService _googleMapsService;
    private readonly IInterpolationService _interpolationService;

    public RouteInterpolationService(
        Polyliner polyliner, 
        IGoogleMapsService googleMapsService,
        IInterpolationService interpolationService)
    {
        _polyliner = polyliner;
        _googleMapsService = googleMapsService;
        _interpolationService = interpolationService;
    }
    
    public IList<IList<PolylinePoint>> GetInterpolatedPointsForRoutes(RouteRequest request)
    {
        IList<RouteResponse> routeDetails = _googleMapsService.GetRouteDetails(request);
        IList<IList<PolylinePoint>> listOfInterpolatedPointsAllRoutes = new List<IList<PolylinePoint>>();
        foreach (RouteResponse route in routeDetails)
        {
            IList<PolylinePoint> routePolylinePoints = _polyliner.Decode(route.Polyline);
            double distanceAssumedToBeTraveledInFifteenMinutes = (route.DistanceMeters * 900.0) / route.Duration ;
            IList<PolylinePoint> listInterpolatedPointsAlongRoute = _interpolationService.GetInterpolatedPointsAlongRouteAtGivenDistance(
                routePolylinePoints, 
                route.DistanceMeters, 
                distanceAssumedToBeTraveledInFifteenMinutes);
            listInterpolatedPointsAlongRoute.Add(routePolylinePoints.Last());
            listOfInterpolatedPointsAllRoutes.Add(listInterpolatedPointsAlongRoute);
        }
        return listOfInterpolatedPointsAllRoutes;
    }
}