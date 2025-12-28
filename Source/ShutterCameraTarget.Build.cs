using Flax.Build;

public class ShutterCameraTarget : GameProjectTarget
{
    /// <inheritdoc />
    public override void Init()
    {
        base.Init();

        // Reference the modules for game
        Modules.Add("ShutterCamera");
    }
}
