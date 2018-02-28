using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input.Touch;
using System.Diagnostics;

namespace AndroidSheep.Models
{
    class DebugCamera : ICamera
    {
        public DebugCamera(Vector3 startpos, GraphicsDeviceManager graphics)
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
       
        public Vector3 Position { get; private set; }
        public Vector3 UpVector { get; private set; }
        public Vector3 LookAt { get; private set; }
        public GraphicsDevice Graphics { get; private set; }

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
            TouchCollection touchCollection = TouchPanel.GetState();
            bool oneFingerTouching = touchCollection.Count == 1;
            bool twoFIngerTouchihng = touchCollection.Count == 2;
            if (oneFingerTouching)
            {
                var xPosition = touchCollection[0].Position.X;
                var yPosition = touchCollection[0].Position.Y;


                float xRatio = xPosition / (float)Graphics.Viewport.Width;
                Debug.WriteLine(string.Format("X: {0}, Y: {1} Camera Position: {2}, {3}", 
                                touchCollection[0].Position.X, touchCollection[0].Position.Y,
                                this.Position.X, this.Position.Y));



                if (xRatio < 1.0f)
                {
                    var forwardVector = new Vector3(xPosition, yPosition, 0);
                    forwardVector = findQuadrant(forwardVector);

                    const float unitsPerSecond = 5;

                    this.Position += forwardVector * unitsPerSecond *
                        (float)gameTime.ElapsedGameTime.TotalSeconds;
                }
            }

            if (twoFIngerTouchihng)
            {
                var xOnePosition = touchCollection[0].Position.X;
                var yOnePosition = touchCollection[0].Position.Y;
                var xTwoPosition = touchCollection[1].Position.X;
                var yTwoPosition = touchCollection[1].Position.Y;


                var vectorOne = new Vector3(xOnePosition, yOnePosition, 0);
                var vectorTwo = new Vector3(xTwoPosition, yTwoPosition, 0);

                vectorOne = findQuadrant(vectorOne);
                vectorTwo = findQuadrant(vectorTwo);

                if (vectorOne.X == 1 && vectorTwo.Y == -1)
                {
                }





            }
        }

        private Vector3 findQuadrant(Vector3 point)
        {
            Vector3 addValue = new Vector3(0, 0, 0);
            float height = (float)Graphics.Viewport.Height;
            float width = (float)Graphics.Viewport.Width;

            int quadrant = SubdivideQuadrant(width, height, point);
            int subquadrant = 0;
            //Quadrant I
            switch (quadrant)
            {
                case 1:
                    subquadrant = SubdivideQuadrant(width * 3 / 4, height * 1 / 4, point);
                    switch (subquadrant)
                    {
                        case 1:
                            addValue.X = -1;
                            addValue.Y = -1;
                            break;
                        case 2:
                            addValue.Y = -1;
                            break;
                        case 3:
                            addValue.X = -1;
                            addValue.Y = -1;
                            break;
                        case 4:
                            addValue.X = -1;
                            break;
                        default:
                            break;
                    }
                    break;

                case 2:
                    subquadrant = SubdivideQuadrant(width * 1 / 4, height * 1 / 4, point);

                    switch (subquadrant)
                    {
                        case 1:
                            addValue.Y = -1;
                            break;
                        case 2:
                            addValue.X = 1;
                            addValue.Y = -1;
                            break;
                        case 3:
                            addValue.X = 1;
                            break;
                        case 4:
                            addValue.X = 1;
                            addValue.Y = -1;
                            break;
                        default:
                            break;
                    }
                    break;

                case 3:
                    subquadrant = SubdivideQuadrant(width * 1 / 4, height * 3 / 4, point);
                    switch (subquadrant)
                    {
                        case 1:
                            break;
                        case 2:
                            addValue.X = 1;
                            break;
                        case 3:
                            addValue.Y = 1;
                            addValue.X = 1;
                            break;
                        case 4:
                            addValue.Y = 1;
                            break;
                        default:
                            addValue.Y = 1;
                            addValue.X = 1;
                            break;
                    }
                    break;

                case 4:
                    subquadrant = SubdivideQuadrant(width * 3 / 4, height * 3 / 4, point);
                    switch (subquadrant)
                    {
                        case 1:
                            addValue.X = -1;
                            break;
                        case 2:
                            break;
                        case 3:
                            addValue.Y = 1;
                            break;
                        case 4:
                            addValue.X = -1;
                            addValue.Y = 1;
                            break;
                        default:
                            addValue.X = -1;
                            addValue.Y = 1;
                            break;
                    }
                    break;

                default:
                    break;
            }
            return addValue;
        }

        private int SubdivideQuadrant(float maxWidth, float maxHeight, Vector3 point)
        {
            int quadrant = 0;
            float xValue = point.X;
            float yValue = point.Y;

            quadrant += xValue >= maxWidth / 2 && yValue <= maxHeight / 2 ? 1 : 0;
            quadrant += xValue < maxWidth / 2 && yValue <= maxHeight / 2 ? 2 : 0;
            quadrant += xValue < maxWidth / 2 && yValue > maxHeight / 2 ? 3 : 0;
            quadrant += xValue >= maxWidth / 2 && yValue > maxHeight / 2 ? 4 : 0;

            return quadrant;
        }
    }
}