using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AndroidSheep.Models.Buttons
{
    public class AndroidBackground : AndroidComponent
    {
        private Texture2D _texture;
        private int _screenHeight;
        private int _screenWidth;

        public Vector2 _position;
        public AndroidBackground(Texture2D texture, int width, int height)
        {
            _texture = texture;
            _position = new Vector2(0, 0);
            _screenHeight = height;
            _screenWidth = width;
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.LinearWrap, null, null);
            spriteBatch.Draw(_texture, _position, new Rectangle(0, 0, _screenWidth, height: _screenHeight), Color.White, 0, Vector2.Zero, 1f, SpriteEffects.None, 0);
            spriteBatch.End();
        }

        public override void Update(GameTime gameTime)
        {
            return;
        }
    }
}