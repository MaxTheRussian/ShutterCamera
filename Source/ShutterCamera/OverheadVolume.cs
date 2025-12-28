// Max5957
using FlaxEngine;

namespace ShutterCamera;


public class OverheadVolume : Script
{

    Actor CameraObject;

    public void ActivateOverheadCam(bool a)
    {
        CameraObject.IsActive = a;
    }

    public override void OnEnable()
    {
        
        CameraObject = Actor.GetChild(1);
        CameraObject.IsActive = false;
    }

}
