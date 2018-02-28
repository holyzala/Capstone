using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
namespace AndroidSheep.Models
{
    class TableTop
    {
        VertexPositionNormalTexture[] floorVerts;
        GraphicsDeviceManager graphics;
        BasicEffect effect;

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
            float s = 10;
            floorVerts = new VertexPositionNormalTexture[15];

            floorVerts[0].Position = new Vector3(s, s, 0);
            floorVerts[1].Position = new Vector3(0, 0, 0);
            floorVerts[2].Position = new Vector3(0, s, 0);

            floorVerts[3].Position = new Vector3((s * .5f), 2 * s, 0);
            floorVerts[4].Position = new Vector3(s, s, 0);
            floorVerts[5].Position = floorVerts[2].Position;

            floorVerts[6].Position = new Vector3(-(s * .5f), 2 * s, 0);
            floorVerts[7].Position = new Vector3(s * .5f, 2 * s, 0);
            floorVerts[8].Position = floorVerts[2].Position;

            floorVerts[9].Position = new Vector3(-s, s, 0);
            floorVerts[10].Position = new Vector3(-(s * .5f), 2 * s, 0);
            floorVerts[11].Position = floorVerts[2].Position;

            floorVerts[12].Position = new Vector3(0, 0, 0);
            floorVerts[13].Position = new Vector3(-s, s, 0);
            floorVerts[14].Position = floorVerts[2].Position;

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

    }
}