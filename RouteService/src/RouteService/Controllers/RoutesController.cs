using Google.Protobuf.Collections;
using Microsoft.AspNetCore.Mvc;
using RouteService.DTOs;
using RouteService.Services;
using Route = Google.Maps.Routing.V2.Route;

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
    public IList<RouteResponse> Get(RouteRequest request)
    {
        return _googleMapsService.GetRouteDetails(request);
    }
}