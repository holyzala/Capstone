using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input.Touch;
using System.Diagnostics;

namespace AndroidSheep.Models
{
    class PlayerCamera : ICamera
    {
        public PlayerCamera(Vector3 startpos, GraphicsDeviceManager graphics)
        {
            this.Position = startpos;
            this.Graphics = graphics.GraphicsDevice;
            this.FieldOfView = Microsoft.Xna.Framework.MathHelper.PiOver4;
            this.AspectRatio = (float)Graphics.Viewport.Width / (float)Graphics.Viewport.Height;
            this.NearPlane = 1;
            this.FarPlane = 200;
            this.LookAt = new Vector3(0, -1, -5f);
            this.UpVector = new Vector3(0, 0, 1f);
        }

        public Vector3 Position { get; set; }
        public Vector3 UpVector { get; private set; }
        public Vector3 LookAt { get; private set; }
        public GraphicsDevice Graphics { get; private set; }

        public float FieldOfView { get; private set; }
        public float NearPlane { get; private set; }
        public float FarPlane { get; private set; }
        public float AspectRatio { get; private set; }

        public Matrix ProjectionMatrix
        {
            get
            {
                return Matrix.CreatePerspectiveFieldOfView(FieldOfView, AspectRatio, NearPlane, FarPlane);
            }
        }

        public Matrix ViewMatrix
        {
            get
            {
                var lookAtVector = new Vector3(0, -10.0f, -0.5f);
                // We'll create a rotation matrix using our angle
                var rotationMatrix = Matrix.CreateRotationX(0.66f);
                // Then we'll modify the vector using this matrix:
                lookAtVector = Vector3.Transform(lookAtVector, rotationMatrix);
                lookAtVector += this.Position;

                var upVector = Vector3.UnitZ;

                return Matrix.CreateLookAt(
                    Position, lookAtVector, upVector);
            }
        }

        public void Update(GameTime gameTime)
        {

        }
    }
}