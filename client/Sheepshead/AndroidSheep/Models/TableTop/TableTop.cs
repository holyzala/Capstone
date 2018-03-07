using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

namespace AndroidSheep.Models
{
    class TableTop
    {
        VertexPositionNormalTexture[] floorVerts;
        GraphicsDeviceManager graphics;
        BasicEffect effect;
        int tablePoints = 5;

        public TableTop(GraphicsDeviceManager graphics, BasicEffect effect)
        {
            this.graphics = graphics;
            this.effect = effect;
        }

        public void InitializeTable()
        {
            InitializeVertices();
        }


        private void InitializeVertices()
        {
            float radius = 11;
            floorVerts = new VertexPositionNormalTexture[15];
            var tuplePoints = new List<Tuple<float, float>>();
            for(int i = 0; i < tablePoints; i++)
            {
                var tuple = Tuple.Create((float)-(radius * Math.Cos(2 * MathHelper.Pi * i / tablePoints)), (float)-(radius * Math.Sin(2 * MathHelper.Pi * i / tablePoints)));
                tuplePoints.Add(tuple);
            }
     
            floorVerts[0].Position = new Vector3(-tuplePoints[0].Item2 , tuplePoints[0].Item1, 0);
            floorVerts[1].Position = new Vector3(0, 0, 0);
            floorVerts[2].Position = new Vector3(-tuplePoints[1].Item2, tuplePoints[1].Item1, 0);

            floorVerts[3].Position = new Vector3(-tuplePoints[1].Item2, tuplePoints[1].Item1, 0);
            floorVerts[4].Position = floorVerts[1].Position;
            floorVerts[5].Position = new Vector3(-tuplePoints[2].Item2, tuplePoints[2].Item1, 0);

            floorVerts[6].Position = new Vector3(-tuplePoints[2].Item2, tuplePoints[2].Item1, 0);
            floorVerts[7].Position = floorVerts[1].Position;
            floorVerts[8].Position = new Vector3(-tuplePoints[3].Item2, tuplePoints[3].Item1, 0);

            floorVerts[9].Position = new Vector3(-tuplePoints[3].Item2, tuplePoints[3].Item1, 0);
            floorVerts[10].Position = floorVerts[1].Position;
            floorVerts[11].Position = new Vector3(-tuplePoints[4].Item2, tuplePoints[4].Item1, 0);

            floorVerts[12].Position = new Vector3(-tuplePoints[4].Item2, tuplePoints[4].Item1, 0);
            floorVerts[13].Position = floorVerts[1].Position;
            floorVerts[14].Position = new Vector3(-tuplePoints[0].Item2, tuplePoints[0].Item1, 0);

            int reps = 1;
            floorVerts[0].TextureCoordinate = new Vector2(0, 0);
            floorVerts[2].TextureCoordinate = new Vector2(reps, 0);

            floorVerts[1].TextureCoordinate = new Vector2(0, reps);

            floorVerts[3].TextureCoordinate = new Vector2(reps, 0);
            floorVerts[4].TextureCoordinate = new Vector2(0, reps);
            floorVerts[5].TextureCoordinate = new Vector2(0, 0);

            floorVerts[6].TextureCoordinate = new Vector2(reps, 0);
            floorVerts[7].TextureCoordinate = new Vector2(0, 0);
            floorVerts[8].TextureCoordinate = new Vector2(0, reps);

            floorVerts[10].TextureCoordinate = new Vector2(0, reps);
            floorVerts[11].TextureCoordinate = new Vector2(reps, 0);
            floorVerts[9].TextureCoordinate = new Vector2(0, 0);

            floorVerts[12].TextureCoordinate = new Vector2(reps, 0);
            floorVerts[13].TextureCoordinate = new Vector2(0, 0);
            floorVerts[14].TextureCoordinate = new Vector2(0, reps);
        }

        public void DrawGround(Texture2D texture)
        {
            this.effect.TextureEnabled = true;
            this.effect.Texture = texture;
            foreach (var pass in this.effect.CurrentTechnique.Passes)
            {
                pass.Apply();

                this.graphics.GraphicsDevice.DrawUserPrimitives(PrimitiveType.TriangleList, floorVerts, 0, 5);
            }
        }

        public Vector3 GetFloorVertex(int index)
        {
            return floorVerts[index].Position;
        }
    }
}