﻿using Google.Api.Gax.Grpc;
using Google.Maps.Routing.V2;
using RouteService.DTOs;
using RouteService.Services.Factories;
using RouteService.Services.Mappers;

namespace RouteService.Services;

public class GoogleMapsService: IGoogleMapsService
{
    private readonly IConfiguration _configuration;
    private IWaypointFactory _waypointFactory;
    private IRouteMapper _routeMapper;

    public GoogleMapsService(IConfiguration configuration, IWaypointFactory waypointFactory, IRouteMapper routeMapper)
    {
        _configuration = configuration;
        _waypointFactory = waypointFactory;
        _routeMapper = routeMapper;
    }

    private ComputeRoutesRequest CreateRoutesApiRequest(RouteRequest routeRequest)
    {
        ComputeRoutesRequest request = new ComputeRoutesRequest
        {
            Origin = _waypointFactory.Create(_waypointFactory.Parse(routeRequest.Origin)),
            Destination = _waypointFactory.Create(_waypointFactory.Parse(routeRequest.Destination)),
            TravelMode = RouteTravelMode.Drive,
            PolylineQuality = PolylineQuality.HighQuality,
            ComputeAlternativeRoutes = true,
            RoutingPreference = RoutingPreference.TrafficAware
        };
        if (routeRequest.IntermediateWaypoints != null)
        {
            request.Intermediates.AddRange(routeRequest.IntermediateWaypoints.Select(waypoint => _waypointFactory.Create(_waypointFactory.Parse(waypoint))));
        }
        return request;
    }
    
    public async Task<IList<RouteResponse>> GetRouteDetails(RouteRequest request)
    {
        RoutesClient client = new RoutesClientBuilder
        {
            ApiKey = _configuration["GoogleMapsApiKey"]
        }.Build();
        CallSettings callSettings = CallSettings.FromHeader("X-Goog-FieldMask", "routes.distanceMeters,routes.duration,routes.polyline");
        
        ComputeRoutesRequest routesApiRequest = CreateRoutesApiRequest(request);
        ComputeRoutesResponse response = await client.ComputeRoutesAsync(routesApiRequest, callSettings);
        
        return response.Routes.Select(route => _routeMapper.RouteToRouteResponse(route)).ToList();
    }
}