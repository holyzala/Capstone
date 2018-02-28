using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AndroidSheep.Models
{
    internal interface ICamera
    {
        float FieldOfView { get; }
        float NearPlane { get; }
        float FarPlane { get; }
        float AspectRatio { get; }

        Matrix ProjectionMatrix { get; }
        Matrix ViewMatrix { get; }

        Vector3 Position { get; }
        Vector3 UpVector { get; }
        Vector3 LookAt { get; }
        GraphicsDevice Graphics { get; }
        /* TO DO:
         *
         * ToString()
         * == Method
         * != Method
         * Possibly .Equals for whatever reason
         */
    }
}