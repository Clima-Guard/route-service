using Microsoft.AspNetCore.Mvc;
using RouteService.DTOs;
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
    public ActionResult<IList<RouteResponse>> Get([FromQuery] RouteRequest request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        
        IList<RouteResponse> routes = _googleMapsService.GetRouteDetails(request);
        return Ok(routes);
    }
}