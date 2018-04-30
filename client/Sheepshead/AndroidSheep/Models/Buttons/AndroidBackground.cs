using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AndroidSheep.Models.Buttons
{
    public class AndroidBackground : AndroidComponent
    {
        private readonly Texture2D _texture;
        private readonly int _screenHeight;
        private readonly int _screenWidth;

        public Vector2 Position;
        public AndroidBackground(Texture2D texture, int width, int height)
        {
            _texture = texture;
            Position = new Vector2(0, 0);
            _screenHeight = height;
            _screenWidth = width;
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.LinearWrap, null, null);
            spriteBatch.Draw(_texture, Position, new Rectangle(0, 0, _screenWidth, height: _screenHeight), Color.White, 0, Vector2.Zero, 1f, SpriteEffects.None, 0);
            spriteBatch.End();
        }

        public override void Update(GameTime gameTime)
        {
            return;
        }
    }
}