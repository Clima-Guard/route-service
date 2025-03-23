using GeographicLib;
using PolylinerNet;

namespace RouteService.Models;

public class PolylineSegment
{
    public PolylinePoint A { get; private set; }
    public PolylinePoint B { get; private set; }

    public PolylineSegment(PolylinePoint a, PolylinePoint b)
    {
        A = a;
        B = b;
    }
    
    public InverseGeodesicResult GetInverseGeodesicResult() => Geodesic.WGS84.Inverse(A.Latitude, A.Longitude, B.Latitude, B.Longitude); 

    public double GetLength()
    {
        return GetInverseGeodesicResult().Distance;
    }
}