using Microsoft.AspNetCore.Mvc;
using RouteService.Services;

namespace RouteService.Controllers;

[Route("api/[controller]")]
public class RoutesController : ControllerBase
{
    private readonly IGoogleMapsService _googleMapsService;
    
    public RoutesController(IGoogleMapsService googleMapsService)
    {
        _googleMapsService = googleMapsService;
    }
    
    [HttpGet]
    public IList<IDictionary<string, object>> Get(
        string? originAddress,
        double? originLongitude,
        double? originLatitude,
        string? destinationAddress,
        double? destinationLongitude,
        double? destinationLatitude)
    {
        return _googleMapsService.GetRouteDetails(originAddress, originLongitude, originLatitude, destinationAddress, destinationLongitude, destinationLatitude);
    }
}