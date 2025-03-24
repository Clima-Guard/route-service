using System.Collections;
using Microsoft.AspNetCore.Mvc;
using PolylinerNet;
using RouteService.DTOs;
using RouteService.Services;

namespace RouteService.Controllers;

[Route("api/routes")]
public class RoutesController : ControllerBase
{
    private readonly IGoogleMapsService _googleMapsService;
    private readonly IRouteInterpolationService _routeInterpolationService;
    
    public RoutesController(IGoogleMapsService googleMapsService, IRouteInterpolationService routeInterpolationService)
    {
        _googleMapsService = googleMapsService;
        _routeInterpolationService = routeInterpolationService;
    }
    
    [HttpGet]
    public ActionResult<IList<RouteResponse>> GetRoutes([FromQuery] RouteRequest request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        
        IList<RouteResponse> routes = _googleMapsService.GetRouteDetails(request);
        return Ok(routes);
    }

    [HttpGet("interpolation")]
    public ActionResult<IList<IList<PolylinePoint>>> GetInterpolatedPoints([FromQuery] RouteRequest request)
    {
        if(!ModelState.IsValid)
            return BadRequest(ModelState);
        IList<IList<PolylinePoint>> interpolatedPointsForRoutes = _routeInterpolationService.GetInterpolatedPointsForRoutes(request);
        return Ok(interpolatedPointsForRoutes);
    }
}