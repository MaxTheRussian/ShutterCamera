using System;
using FlaxEngine;

namespace ShutterCamera
{
    /// <summary>
    /// The sample game plugin.
    /// </summary>
    /// <seealso cref="FlaxEngine.GamePlugin" />
    public class ShutterCamera : GamePlugin
    {
        /// <inheritdoc />
        public ShutterCamera()
        {
            _description = new PluginDescription
            {
                Name = "ShutterCamera",
                Category = "Other",
                Author = "Max5957",
                AuthorUrl = null,
                HomepageUrl = null,
                RepositoryUrl = "https://github.com/FlaxEngine/ShutterCamera",
                Description = "This is a plugin that is very similar to Unity's cinemachine, while very barebones in comparison, it has the core (at least for me) feature of camera switching",
                Version = new Version(1, 0),
                IsAlpha = true,
                IsBeta = false,
            };
        }

        /// <inheritdoc />
        public override void Initialize()
        {
            base.Initialize();

            Debug.Log("Thank you for using ShutterCamera plugin <3!");
        }

        /// <inheritdoc />
        public override void Deinitialize()
        {
            // Use it to cleanup data

            base.Deinitialize();
        }
    }
}
