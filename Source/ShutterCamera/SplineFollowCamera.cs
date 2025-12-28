// Max5957
using FlaxEngine;
using ShutterCamera;


/// <summary>
/// SplineFollowCamera Script.
/// </summary>
public class SplineFollowCamera : ShutterTripod
{
    /// <inheritdoc/>
    Spline parent;
    public Float2 Offset;
    public Float2 Rotation;
    float t;

    public override void OnStart()
    {
        parent = Actor.Parent.As<Spline>();
    }
   

    public override void UpdateTransform()
    {
        if (parent == null)
            return;
        t = GetClosestPointRefined(parent, PlayerInstance.Position);
# if FLAX_EDITOR
        DebugDraw.DrawLine(parent.GetSplinePoint(t), PlayerInstance.Position, Color.IndianRed, 0f, false);
#endif
        Actor.Position = parent.GetSplinePoint(t) + Float3.Up * Offset.Y - parent.GetSplineDirection(t) * Offset.X;
        Actor.Direction = parent.GetSplineDirection(t);
        Actor.Orientation *= Quaternion.Euler(Rotation.X, Rotation.Y, 0f);

    }

    public float GetClosestPointRefined(Spline spline, Vector3 targetPoint, int initialSamples = 50, int refinementSteps = 70)
    {
        // First pass: coarse search
        float bestTime = 0f;
        double closestDistanceSqr = double.MaxValue;

        for (int i = 0; i <= initialSamples; i++)
        {
            float t = (float)i / initialSamples;
            float currentTime = t * spline.SplineDuration;

            Vector3 pointOnSpline = spline.GetSplinePoint(currentTime);
            double distSqr = Vector3.DistanceSquared(pointOnSpline, targetPoint);

            if (distSqr < closestDistanceSqr)
            {
                closestDistanceSqr = distSqr;
                bestTime = currentTime;
            }
        }

        // Second pass: refine around the best point
        float searchRadius = 1.0f / initialSamples; // Search in this region

        for (int step = 0; step < refinementSteps; step++)
        {
            searchRadius *= 0.5f; // Halve the search radius each iteration

            float leftTime = Mathf.Max(0, bestTime - searchRadius);
            float rightTime = Mathf.Min(spline.SplineDuration, bestTime + searchRadius);

            Vector3 leftPoint = spline.GetSplinePoint(leftTime);
            Vector3 rightPoint = spline.GetSplinePoint(rightTime);

            double leftDist = Vector3.DistanceSquared(leftPoint, targetPoint);
            double rightDist = Vector3.DistanceSquared(rightPoint, targetPoint);

            if (leftDist < rightDist)
            {
                bestTime = leftTime;
                closestDistanceSqr = leftDist;
            }
            else
            {
                bestTime = rightTime;
                closestDistanceSqr = rightDist;
            }
        }

        return bestTime;
    }
}
