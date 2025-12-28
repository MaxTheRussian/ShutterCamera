using System;
using System.Collections.Generic;
using FlaxEngine;

namespace ShutterCamera;

/// <summary>
/// ObjectMover Script.
/// </summary>
public class ObjectMover : Script
{
    public Vector3 A;
    public Vector3 B;
    public float Speed;
    [ShowInEditor]
    bool c;

    public override void OnStart()
    {
        c = false;
    }

    /// <inheritdoc/>
    public override void OnUpdate()
    {
        if (c)
        {
            Actor.Position = Vector3.MoveTowards(Actor.Position, A, Speed * Time.DeltaTime);
            if ((Actor.Position - A).Length < 1f) c = false;
        }
        else
        {
            Actor.Position = Vector3.MoveTowards(Actor.Position, B, Speed * Time.DeltaTime);
            if ((Actor.Position - B).Length < 1f) c = true;
        }
    }

# if FLAX_EDITOR
    public override void OnDebugDraw()
    {
        DebugDraw.DrawWireSphere(new BoundingSphere(A, 10f), Color.Red);
        DebugDraw.DrawWireSphere(new BoundingSphere(B, 10f), Color.Green);
    }
#endif
}
