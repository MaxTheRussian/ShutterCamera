using FlaxEngine;
using System.Collections.Generic;

namespace ShutterCamera;

/// <summary>
/// ShutterHeart Script.
/// </summary>
public class ShutterHeart : Script
{
    /// <inheritdoc/>
    /// 

    public static ShutterHeart instance;
    Camera self;
    public ShutterTripod CurrentActiveCamera;
    public UpdateMode updateMode;
    [ShowInEditor]
    List<ShutterTripod> TripodsList = new List<ShutterTripod>();
    bool duringTransition;
    float a;
    Float3 TransitionPosition;
    Quaternion TransitionOrient;
    public EasingType TransitionStyle;



    public enum UpdateMode
    {
        OnLateUpdate,
        OnLateFixedUpdate,
    }

    public override void OnAwake()
    {
        self = Actor.As<Camera>();
        instance = this;
    }
    

    /// <inheritdoc/>
    public void AssignNewCamera(Actor incamera)
    {
        ShutterTripod camera = incamera.GetScript<ShutterTripod>();
        if (!TripodsList.Contains(camera))
            TripodsList.Add(camera);

        if (CurrentActiveCamera == null)
        {
            CurrentActiveCamera = camera;
            StartTransition();
        }
        else if (CurrentActiveCamera.priority <= camera.priority)
        {
            CurrentActiveCamera = camera;
            StartTransition();
        }

        Debug.Log(CurrentActiveCamera.Actor.Name);

            
    }

    public void FindOldCamera(ShutterTripod shutterTripod)
    {
        if (CurrentActiveCamera != shutterTripod)
            return;
        int maxprior = int.MinValue;
        int j = 0;
        for (int i = 0; i < TripodsList.Count; i++)
            if (TripodsList[i].priority >= maxprior && TripodsList[i].Actor.IsActive) 
            {
                maxprior = TripodsList[i].priority;
                j = i;
            }

        CurrentActiveCamera = TripodsList[j];

        //Debug.Log(CurrentActiveCamera.Actor.Name);
        StartTransition();
    }

    /// <inheritdoc/>
    public override void OnLateUpdate()
    {
        UpdateCameraTransform(UpdateMode.OnLateUpdate);
    }

    public override void OnLateFixedUpdate()
    {
        UpdateCameraTransform(UpdateMode.OnLateFixedUpdate);
    }


    private void UpdateCameraTransform(UpdateMode ToCheck)
    {
        if (updateMode != ToCheck || CurrentActiveCamera == null) return;
        else if (duringTransition) ProceedTransition();
        else
        {

            CurrentActiveCamera.UpdateTransform();
            self.FieldOfView = CurrentActiveCamera.FOV;
            Actor.Position = CurrentActiveCamera.Actor.Position;
            Actor.Orientation = CurrentActiveCamera.Actor.Orientation;
        }
    }

    private void StartTransition()
    {

        TransitionPosition = Actor.Position;
        TransitionOrient = Actor.Orientation;
        a = 0f;
        duringTransition = true;
    }

    private void ProceedTransition()
    {
        a += Time.DeltaTime / CurrentActiveCamera.transitionTime;
        CurrentActiveCamera.UpdateTransform();
        switch (TransitionStyle)
        {
            case EasingType.Linear:
                Actor.Position = Vector3.Lerp(TransitionPosition, CurrentActiveCamera.Actor.Position, a);
                Actor.Orientation = Quaternion.Lerp(TransitionOrient, CurrentActiveCamera.Actor.Orientation, a);
                self.FieldOfView = float.Lerp(self.FieldOfView, CurrentActiveCamera.FOV, a);
                break;
            case EasingType.EaseInOut:
                Actor.Position = Vector3.Lerp(TransitionPosition, CurrentActiveCamera.Actor.Position, Mathf.Sin(a * Mathf.Pi / 2));
                Actor.Orientation = Quaternion.Lerp(TransitionOrient, CurrentActiveCamera.Actor.Orientation, Mathf.Sin(a * Mathf.Pi / 2));
                self.FieldOfView = float.Lerp(self.FieldOfView, CurrentActiveCamera.FOV, Mathf.Sin(a * Mathf.Pi / 2));
                break;
        }

        if (a >= 1f)
            duringTransition = false;

    }

#if FLAX_EDITOR
    public override void OnDebugDraw()
    {
        if (!CurrentActiveCamera) return;
        Actor.Position = CurrentActiveCamera.Actor.Position;
        Actor.Orientation = CurrentActiveCamera.Actor.Orientation;
    }
#endif

}



public enum EasingType
{
    Linear,
    EaseInOut,
    EaseOut,
    EaseIn,
}