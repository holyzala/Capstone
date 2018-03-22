using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AndroidSheep.Models.Buttons
{
    class Background : Component
    {
        private Texture2D _texture;
        private int _screenHeight;
        private int _screenWidth;

        public Vector2 _position;
        public Background(Texture2D texture, int width, int height)
        {
            _texture = texture;
            _position = new Vector2(0, 0);
            _screenHeight = height;
            _screenWidth = width;
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch, int height, int width)
        {
            spriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.LinearWrap, null, null);
            spriteBatch.Draw(_texture, _position, new Rectangle(0, 0, _screenWidth, height: _screenHeight), Color.White, 0, Vector2.Zero, 1f, SpriteEffects.None, 0);
            spriteBatch.End();
        }

        public override void Update(GameTime gameTime)
        {
            throw new NotImplementedException();
        }
    }
}