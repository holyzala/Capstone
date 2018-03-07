using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using System;
using System.Diagnostics;

namespace AndroidSheep.Models
{
    public class DebugCamera : ICamera
    {
        public DebugCamera(Vector3 startpos, GraphicsDeviceManager graphics)
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

        private float CameraSpeed = 0.5f;
        private Vector3 CameraDirection;
        private TouchCollection previousTouch;
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
            bool playerDebug = false;

            bool touched = touchCollection.Count == 1;

            if (touched)
            {
                if(previousTouch.Count == 0)
                {
                    previousTouch = touchCollection;
                }
                var xPosition = touchCollection[0].Position.X;
                var yPosition = touchCollection[0].Position.Y;
                float xRatio = xPosition / (float)Graphics.Viewport.Width;
                CameraDirection = Vector3.Transform(CameraDirection,
                                Matrix.CreateFromAxisAngle(UpVector,
                                (-MathHelper.PiOver4 / 20) * (xPosition - previousTouch[0].Position.X) / (float)Graphics.Viewport.Width));


                CameraDirection = Vector3.Transform(CameraDirection,
                                Matrix.CreateFromAxisAngle(Vector3.Cross(UpVector, CameraDirection),
                                (MathHelper.PiOver4 / 20) * (yPosition - previousTouch[0].Position.Y) / (float)Graphics.Viewport.Height));

                UpVector = Vector3.Transform(UpVector,
                            Matrix.CreateFromAxisAngle(Vector3.Cross(UpVector, CameraDirection),
                            (MathHelper.PiOver4 / 20) * (yPosition - previousTouch[0].Position.Y) / (float)Graphics.Viewport.Height));

            }

            if (Keyboard.GetState().IsKeyDown(Keys.W))
                Position += CameraDirection * CameraSpeed;

            if (Keyboard.GetState().IsKeyDown(Keys.S))
                Position -= CameraDirection * CameraSpeed;

            if (Keyboard.GetState().IsKeyDown(Keys.A))
                Position += Vector3.Cross(UpVector, CameraDirection) * CameraSpeed;
            if (Keyboard.GetState().IsKeyDown(Keys.D))
                Position -= Vector3.Cross(UpVector, CameraDirection) * CameraSpeed;

            if (Keyboard.GetState().IsKeyDown(Keys.P))
            {
                playerDebug = true;
            }

            if (playerDebug)
            {

                if (Keyboard.GetState().IsKeyDown(Keys.P))
                {

                }
            }
        }
    }
}