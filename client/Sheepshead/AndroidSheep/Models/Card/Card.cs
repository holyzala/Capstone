using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input.Touch;
using System.Collections.Generic;
using System.Diagnostics;

namespace AndroidSheep.Models
{
    public class Card
    {
        Vector2 position;
        Texture2D texture;
        SpriteBatch sprite;
        GraphicsDeviceManager graphics;

        public Card(Vector2 position, GraphicsDeviceManager graphics, SpriteBatch sprite)
        {
            this.position = position;
            this.graphics = graphics;
            this.sprite = sprite;
        }

        public void SetCardTexture(Texture2D texture)
        {
            this.texture = texture;
        }

        public Texture2D GetCardTexture()
        {
            return this.texture;
        }

        public void InitializeCard()
        {

        }

        public void DrawCard()
        {
            sprite.Begin();
            sprite.Draw(texture, position, null, Color.White, 0f, Vector2.Zero, 0.5f, SpriteEffects.None, 0f);
            sprite.End();
        }

        public void UpdateCard(GameTime gameTime, DebugCamera camera)
        {
            TouchCollection touches = TouchPanel.GetState();
            bool touched = touches.Count == 1;
            if (touched)
            {

            }
        }
    }
}