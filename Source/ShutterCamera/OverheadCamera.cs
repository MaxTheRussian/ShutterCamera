// Max5957

using FlaxEngine;

namespace ShutterCamera;

/// <summary>
/// OverheadCameraVolume Script.
/// </summary>
public class OverheadCamera : ShutterTripod
{
    /// <inheritdoc/>
    /// 
     
    public Float3 MinCamPosition = Float3.Minimum;
    public Float3 MaxCamPosition = Float3.Maximum;
    public Float3 PlayerOffset;
    public Float2 SetAngle;
    public Style FollowStyle;
    public enum Style
    {
        Global,
        Local,
        StaticLook,
        RotateWithPlayer,
        FullyStatic
    }

    public override void OnStart()
    {

    }

    /// <inheritdoc/>
    public override void UpdateTransform()
    {
        switch (FollowStyle) { 
            case Style.Global:
                Actor.Position = PlayerOffset + PlayerInstance.Position;
                Actor.EulerAngles = new Float3(SetAngle.X, SetAngle.Y, 0f);
                break;
            case Style.Local:
                Actor.EulerAngles = new Float3(SetAngle.X, SetAngle.Y, 0f);
                Actor.Position = PlayerInstance.Position + PlayerOffset * Quaternion.Euler(0f, SetAngle.Y, 0f);
                break;
            case Style.StaticLook:
                Actor.LocalPosition = PlayerOffset;
                Actor.Direction = PlayerInstance.Position - Actor.Position + Float3.Up * SetAngle.Y;
                break;
            case Style.RotateWithPlayer:
                float dot = Float3.Dot(PlayerInstance.Direction, Actor.Transform.Right);
                if (Mathf.Abs(dot) > 0.85f)
                {
                    SetAngle.Y += dot * Time.DeltaTime * 50f;
                }
                Actor.Position = PlayerInstance.Position + PlayerOffset * Quaternion.Euler(0f, SetAngle.Y, 0f);
                Actor.Direction = (PlayerInstance.Position - Actor.Position).Normalized;
                Actor.Orientation *= Quaternion.Euler(SetAngle.X, 0f, 0f);
                break;
            case Style.FullyStatic:
                Actor.LocalPosition = PlayerOffset;
                Actor.LocalEulerAngles = new Float3(SetAngle.X, SetAngle.Y, 0f);
                break;

        }

        Actor.Position = Vector3.Clamp(Actor.Position, MinCamPosition, MaxCamPosition);
    }

    public override void OnDebugDraw()
    {
        if (MinCamPosition.X > MaxCamPosition.X)
            MaxCamPosition.X = MinCamPosition.X;
        if (MinCamPosition.Y > MaxCamPosition.Y)
            MaxCamPosition.Y = MinCamPosition.Y;
        if (MinCamPosition.Z > MaxCamPosition.Z)
            MaxCamPosition.Z = MinCamPosition.Z;

        DebugDraw.DrawLine(Actor.Position, Actor.Position + PlayerOffset, Color.HotPink, 0f, true);
        DebugDraw.DrawWireSphere(new BoundingSphere(Actor.Position + PlayerOffset, 35f), Color.Red, 0f, false);
        DebugDraw.DrawLine(Actor.Position + PlayerOffset, Actor.Position + PlayerOffset + Float3.Forward * Quaternion.Euler(SetAngle.X, SetAngle.Y, 0f), Color.HotPink, 0f, true);
        if (Style.StaticLook == FollowStyle)
            DebugDraw.DrawWireSphere(new BoundingSphere(MinCamPosition, 35f), Color.Red, 0f, false);
        if (MinCamPosition != MaxCamPosition)
            DebugDraw.DrawWireBox(new BoundingBox(MinCamPosition, MaxCamPosition), Color.Yellow, 0f, true);
        else
            DebugDraw.DrawWireSphere(new BoundingSphere(MinCamPosition, 15f), Color.Yellow, 0f, false);
    }


}
