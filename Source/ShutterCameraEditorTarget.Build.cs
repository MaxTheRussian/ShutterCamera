using Flax.Build;

public class ShutterCameraEditorTarget : GameProjectEditorTarget
{
    /// <inheritdoc />
    public override void Init()
    {
        base.Init();

        // Reference the modules for editor
        Modules.Add("ShutterCamera");
        Modules.Add("ShutterCameraEditor");
    }
}
