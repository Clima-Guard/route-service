using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using RouteService.Validators;

namespace RouteService.DTOs;

public class RouteRequest
{
    [Required]
    [FromQuery(Name = "origin")]
    public string Origin { get; set; }
    [Required]
    [FromQuery(Name = "destination")]
    public string Destination { get; set; }
    [FromQuery(Name = "waypoints")]
    [NotEmptyIfProvided]
    public List<string>? IntermediateWaypoints { get; set; }
    
    public RouteRequest() 
    {
        IntermediateWaypoints = new List<string>();
    }

    public RouteRequest(string origin, string destination, List<string>? intermediateWaypoints)
    {
        Origin = origin;
        Destination = destination;
        IntermediateWaypoints = intermediateWaypoints;
    }
    
    public RouteRequest(string origin, string destination)
    {
        Origin = origin;
        Destination = destination;
        IntermediateWaypoints = new List<string>();
    }
}