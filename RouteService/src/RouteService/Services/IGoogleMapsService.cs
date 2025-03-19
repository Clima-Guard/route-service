namespace RouteService.Services;

public interface IGoogleMapsService
{
    IList<IDictionary<string, object>> GetRouteDetails(
        string? originAddress,
        double? originLongitude,
        double? originLatitude,
        string? destinationAddress,
        double? destinationLongitude,
        double? destinationLatitude);
}