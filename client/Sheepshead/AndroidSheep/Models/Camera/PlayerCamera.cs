using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AndroidSheep.Models
{
    internal class PlayerCamera : ICamera
    {

        public PlayerCamera(string name, Vector3 startPos, GraphicsDeviceManager graphics)
        {
            this.Position = startPos;
            this.Name = name;
            this.Graphics = graphics.GraphicsDevice;
            this.FieldOfView = Microsoft.Xna.Framework.MathHelper.PiOver4;
            this.AspectRatio = (float)Graphics.Viewport.Width / (float)Graphics.Viewport.Height;
            this.NearPlane = 1;
            this.FarPlane = 200;
        }

        public bool IsLocked { get; private set; }
        public string Name { get; private set; }
        public Vector3 Position { get; private set; }

        public GraphicsDevice Graphics { get; private set; }
        public float FieldOfView { get; private set; }
        public float NearPlane { get; private set; }
        public float FarPlane { get; private set; }
        public float AspectRatio { get; private set; }

        public Matrix PerspectiveMatrix
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
                var lookAtVector = Vector3.Zero;
                var upVector = Vector3.UnitZ;
                return Matrix.CreateLookAt(Position, lookAtVector, upVector);
            }
        }

        public void Update(GameTime gameTime)
        {
            // We'll be doing some input-based movement here
        }
    }
}