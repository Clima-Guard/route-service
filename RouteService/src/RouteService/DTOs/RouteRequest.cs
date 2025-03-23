using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace RouteService.DTOs;

public class RouteRequest
{
    [Required]
    [FromBody]
    public WaypointDto Origin { get; set; }
    [Required]
    [FromBody]
    public WaypointDto Destination { get; set; }
    [FromBody]
    public List<WaypointDto>? IntermediateWaypoints { get; set; }
    
    public RouteRequest() 
    {
        IntermediateWaypoints = new List<WaypointDto>();
    }

    public RouteRequest(WaypointDto origin, WaypointDto destination, List<WaypointDto>? intermediateWaypoints)
    {
        Origin = origin;
        Destination = destination;
        IntermediateWaypoints = intermediateWaypoints;
    }
    
    public RouteRequest(WaypointDto origin, WaypointDto destination)
    {
        Origin = origin;
        Destination = destination;
        IntermediateWaypoints = new List<WaypointDto>();
    }
}