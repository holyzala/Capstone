using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace AndroidSheep.Models
{
    public abstract class Card
    {
        public Card(Vector3 startPosition, Model mesh)
        {
            this.MeshModel = mesh;
            this.Position = startPosition;
        }

        Model MeshModel;
        Vector3 Position;   
        float RotatedAngle;

        public void Initialize(ContentManager contentManager)
        {
            string ModelName = MeshModel.ToString();
            MeshModel = contentManager.Load<Model>(ModelName);
        }

        private Matrix GetWorldMatrix()
        {
            Matrix translation = Matrix.CreateTranslation(Position);
            Matrix rotation = Matrix.CreateRotationZ(RotatedAngle);
            return translation * rotation;
        }
    }
}