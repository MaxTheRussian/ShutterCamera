using FlaxEngine;

namespace ShutterCamera;

/// <summary>
/// ShutterTripod Script.
/// </summary>
public class ShutterTripod : Script
{
    /// <inheritdoc/>
    /// 
    public float transitionTime = 1f;
    ShutterHeart ShutterHeartRef;
    public int priority;
    public float FOV = 70f;
    public Actor PlayerInstance;


    public override void OnAwake()
    {
        

    }

    /// <inheritdoc/>
    public override void OnEnable()
    {
        ShutterHeartRef = ShutterHeart.instance;
        Debug.Log(Actor.Name);
        UpdateTransform();
        ShutterHeartRef.AssignNewCamera(Actor);

    }

    /// <inheritdoc/>
    public override void OnDisable()
    {
        ShutterHeartRef.FindOldCamera(this);
    }

    /// <inheritdoc/>
    public virtual void UpdateTransform()
    {

    }

}
