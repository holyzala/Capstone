using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace AndroidSheep.Models
{
    class Camera : ICamera
    {
        GraphicsDevice graphicsDevice;

        public Camera(string name, Vector3 startPos)
        {

            this.Position = startPos;
            this.Name = name;
            this.FieldOfView = Microsoft.Xna.Framework.MathHelper.PiOver4;
            this.AspectRatio = (float)graphicsDevice.Viewport.Width / (float)graphicsDevice.Viewport.Height;
            this.NearPlane = 1;
            this.FarPlane = 200;
        }

        public bool IsLocked { get; private set; }
        public string Name { get; private set; }
        public Vector3 Position { get; private set; }

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

        public Camera(GraphicsDevice graphicsDevice)
        {
            this.graphicsDevice = graphicsDevice;
        }

        public void Update(GameTime gameTime)
        {
            // We'll be doing some input-based movement here
        }
    }
}