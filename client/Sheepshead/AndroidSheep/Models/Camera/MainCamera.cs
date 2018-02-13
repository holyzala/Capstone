using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input.Touch;

namespace AndroidSheep.Models.Camera
{
    internal class MainCamera : ICamera
    {
        public MainCamera(string name, Vector3 startpos, GraphicsDeviceManager graphics)
        {
            this.Position = startpos;
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
                var lookAtVector = new Vector3(0, -1, -5);
                var rotationMatrix = Matrix.CreateRotationZ(angle);
                lookAtVector = Vector3.Transform(lookAtVector, rotationMatrix);

                lookAtVector += Position;
                var upVector = Vector3.UnitZ;
                return Matrix.CreateLookAt(Position, lookAtVector, upVector);
            }
        }

        float angle;
        public void Update(GameTime gameTime)
        {
            TouchCollection touchCollection = TouchPanel.GetState();
            bool isTouchingScreen = touchCollection.Count > 0;
            if (isTouchingScreen)
            {
                var xPosition = touchCollection[0].Position.X;

                float xRatio = xPosition / (float)Graphics.Viewport.Width;

                if (xRatio < 1 / 3.0f)
                {
                    angle += (float)gameTime.ElapsedGameTime.TotalSeconds;
                }
                else if (xRatio < 2 / 3.0f)
                {
                    var forwardVector = new Vector3(0, -1, 0);

                    var rotationMatrix = Matrix.CreateRotationZ(angle);
                    forwardVector = Vector3.Transform(forwardVector, rotationMatrix);

                    const float unitsPerSecond = 3;

                    this.Position += forwardVector * unitsPerSecond *
                        (float)gameTime.ElapsedGameTime.TotalSeconds;
                }
                else
                {
                    angle -= (float)gameTime.ElapsedGameTime.TotalSeconds;
                }
            }
        }
    }
}