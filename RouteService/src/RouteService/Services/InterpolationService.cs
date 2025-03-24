using GeographicLib;
using PolylinerNet;
using RouteService.Models;

namespace RouteService.Services;

public class InterpolationService: IInterpolationService
{
    public IList<PolylinePoint> GetInterpolatedPointsAlongRouteAtGivenDistance(IList<PolylinePoint> pointsAlongRoute, double routeDistance, double distanceBetweenPoints)
    {
        int numberOfPointsToInterpolate = (int)(routeDistance / distanceBetweenPoints);
        IList<PolylinePoint> interpolatedPointsResultList = new List<PolylinePoint>();
        
        double sumAllSegmentDistancesUntilLastPointAtGivenDistance = 0.0;
        double sumAllSegmentsUntilStartOfSegmentContainingThePointToInterpolate = 0.0;
        double sumAllSegmentsUntilEndOfSegmentContainingThePointToInterpolate = 0.0;
        int currentSegmentStartPointIndex = 0;
        PolylineSegment segment = new PolylineSegment(pointsAlongRoute[0], pointsAlongRoute[1]);

        for (int i = 0; i <= numberOfPointsToInterpolate; i++)
        {
            while (sumAllSegmentDistancesUntilLastPointAtGivenDistance > sumAllSegmentsUntilEndOfSegmentContainingThePointToInterpolate) {
                sumAllSegmentsUntilStartOfSegmentContainingThePointToInterpolate = sumAllSegmentsUntilEndOfSegmentContainingThePointToInterpolate;
                segment = new PolylineSegment(pointsAlongRoute[currentSegmentStartPointIndex], pointsAlongRoute[currentSegmentStartPointIndex + 1]);
                sumAllSegmentsUntilEndOfSegmentContainingThePointToInterpolate += segment.GetLength();
                currentSegmentStartPointIndex++;
            }
            PolylinePoint pointToInterpolate = InterpolatePointAtGivenDistanceFromStartSegment(segment, sumAllSegmentDistancesUntilLastPointAtGivenDistance - sumAllSegmentsUntilStartOfSegmentContainingThePointToInterpolate);
            interpolatedPointsResultList.Add(pointToInterpolate);
            sumAllSegmentDistancesUntilLastPointAtGivenDistance += distanceBetweenPoints;
        }
        
        return interpolatedPointsResultList;
    }
    
    private PolylinePoint InterpolatePointAtGivenDistanceFromStartSegment(PolylineSegment segment, double distanceAlongSegment)
    {
        InverseGeodesicResult segmentGeodesicResult = segment.GetInverseGeodesicResult();
        DirectGeodesicResult result = Geodesic.WGS84.Direct(segment.A.Latitude, segment.A.Longitude, segmentGeodesicResult.Azimuth1, distanceAlongSegment);
        return new PolylinePoint(result.Latitude, result.Longitude);
    }
}