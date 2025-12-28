using System;
using System.Collections.Generic;
using FlaxEngine;

namespace ShutterCamera;

/// <summary>
/// CameraSwitcher Script.
/// </summary>
public class CameraSwitcher : Script
{
    public Actor[] Cameras;
    [ShowInEditor]
    int i;
    /// <inheritdoc/>
    /// 

    public override void OnUpdate()
    {
        if (Input.GetKeyDown(KeyboardKeys.Spacebar))
        {
            Cameras[i].IsActive = false;
            i++;
            if (i == Cameras.Length)
                i = 0;
            Cameras[i].IsActive = true;
        }
    }
}
