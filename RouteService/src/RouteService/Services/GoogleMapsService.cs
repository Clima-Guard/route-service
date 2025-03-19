using Google.Api.Gax.Grpc;
using Google.Maps.Routing.V2;
using Google.Type;
using Route = Google.Maps.Routing.V2.Route;

namespace RouteService.Services;

public class GoogleMapsService: IGoogleMapsService
{
    private readonly IConfiguration _configuration;

    public GoogleMapsService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public IList<IDictionary<string, object>> GetRouteDetails(
        string? originAddress,
        double? originLongitude,
        double? originLatitude,
        string? destinationAddress,
        double? destinationLongitude,
        double? destinationLatitude)
    {
        RoutesClient client = new RoutesClientBuilder
        {
            ApiKey = _configuration["GoogleMapsApiKey"]
        }.Build();
        CallSettings callSettings = CallSettings.FromHeader("X-Goog-FieldMask", "routes.distanceMeters,routes.duration,routes.polyline");

        Waypoint originWaypoint;
        Waypoint destinationWaypoint;
        if (originAddress != null)
        {
            originWaypoint = new Waypoint
            {
                Address = originAddress,
            };
        }
        else if (originLongitude.HasValue && originLatitude.HasValue)
        {
            originWaypoint = new Waypoint
            {
                Location = new Location
                    { LatLng = new LatLng { Latitude = originLatitude.Value, Longitude = originLongitude.Value } }
            };
        }
        else
            throw new ArgumentException("Origin address or latitude and longitude are required.");

        if (destinationAddress != null)
        {
            destinationWaypoint = new Waypoint
            {
                Address = destinationAddress,
            };
        }
        else if (destinationLongitude.HasValue && destinationLatitude.HasValue)
        {
            destinationWaypoint = new Waypoint
            {
                Location = new Location { LatLng = new LatLng { Latitude = destinationLatitude.Value, Longitude = destinationLongitude.Value } }
            };
        }
        else
            throw new ArgumentException("Destination address or latitude and longitude are required.");
        
        ComputeRoutesRequest request = new ComputeRoutesRequest
        {
            Origin = originWaypoint,
            Destination = destinationWaypoint,
            TravelMode = RouteTravelMode.Drive,
            PolylineQuality = PolylineQuality.HighQuality,
            ComputeAlternativeRoutes = true,
            // RoutingPreference = RoutingPreference.TrafficAware
        };
        ComputeRoutesResponse response = client.ComputeRoutes(request, callSettings);

        IList<IDictionary<string, object>> listRouteDetails = new List<IDictionary<string, object>>();
        foreach (Route route in response.Routes)
        {
            IDictionary<string, object> routeDetails = new Dictionary<string, object>();
            routeDetails.Add("distance", route.DistanceMeters);
            routeDetails.Add("duration", route.Duration);
            routeDetails.Add("polyline", route.Polyline);
            listRouteDetails.Add(routeDetails);
        }
        return listRouteDetails;
    }
}