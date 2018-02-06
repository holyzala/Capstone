using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SH_Android.Resources.Models.Interfaces
{
    interface ICamera
    {
        string Name { get; }
        Vector3 Position { get; }

        float FieldOfView { get; }
        float NearPlane { get; }
        float FarPlane { get; }
        float AspectRatio { get; }

        bool IsLocked { get; }
        Matrix PerspectiveMatrix { get; }
        Matrix ViewMatrix { get; }

        /* TO DO:
         *
         * ToString()
         * == Method
         * != Method
         * Possibly .Equals for whatever reason
         */
        /* Possible TO DO:
         * Vector3 Translation - Moving the Camera
         * Vector3 Rotation - Rotating the Camera
         * Vector3 Scaling - Zooming in and Out
         */

    }
}