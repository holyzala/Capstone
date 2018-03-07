using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using System;
using System.Diagnostics;

namespace AndroidSheep.Models
{
    public class PlayerCamera : ICamera
    {
        public PlayerCamera(Vector3 startpos, GraphicsDeviceManager graphics)
        {
            Position = startpos;
            Graphics = graphics.GraphicsDevice;
            this.Initialize();
        }

        private void Initialize()
        {
            FieldOfView = Microsoft.Xna.Framework.MathHelper.PiOver4;
            AspectRatio = (float)Graphics.Viewport.Width / (float)Graphics.Viewport.Height;
            NearPlane = 1;
            FarPlane = 200;
            LookAt = new Vector3(0, -1, -5f);
            UpVector = new Vector3(0, 0, 1f);
            CameraDirection = LookAt - Position;
            CameraDirection.Normalize();
        }

        public float FieldOfView { get; private set; }
        public float NearPlane { get; private set; }
        public float FarPlane { get; private set; }
        public float AspectRatio { get; private set; }

        public Vector3 Position { get; private set; }
        public Vector3 UpVector { get; private set; }
        public Vector3 LookAt { get; private set; }
        public GraphicsDevice Graphics { get; private set; }

        private Vector3 CameraDirection;
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
                return Matrix.CreateLookAt(Position, Position + CameraDirection, UpVector);
            }
        }

        public void Update(GameTime gameTime)
        {
            TouchCollection touchCollection = TouchPanel.GetState();
        }
    }
}