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
    
    public async Task<IList<IList<PolylinePoint>>> GetInterpolatedPointsForRoutes(RouteRequest request)
    {
        IList<RouteResponse> routeDetails = await _googleMapsService.GetRouteDetails(request);
        IList<IList<PolylinePoint>> listOfInterpolatedPointsAllRoutes = new List<IList<PolylinePoint>>();
        foreach (RouteResponse route in routeDetails)
        {
            IList<PolylinePoint> routePolylinePoints = _polyliner.Decode(route.Polyline);
            double distanceAssumedToBeTraveledInOneHour = (route.DistanceMeters * 3600.0) / route.Duration ;
            IList<PolylinePoint> listInterpolatedPointsAlongRoute = _interpolationService.GetInterpolatedPointsAlongRouteAtGivenDistance(
                routePolylinePoints, 
                route.DistanceMeters, 
                distanceAssumedToBeTraveledInOneHour);
            listOfInterpolatedPointsAllRoutes.Add(listInterpolatedPointsAlongRoute);
        }
        return listOfInterpolatedPointsAllRoutes;
    }
}