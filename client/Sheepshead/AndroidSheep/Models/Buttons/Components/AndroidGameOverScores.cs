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
using SharedSheep.Card;

namespace AndroidSheep.Models.Buttons.Components
{
    class AndroidGameOverScores : AndroidComponent
    {
        #region Fields
        private SpriteFont _font;
        private Texture2D _texture;
        private bool IsInputPressed;
        #endregion

        #region Properties
        public bool Clicked { get; set; }
        public Color PenColor { get; set; }
        public Vector2 Position { get; set; }
        public Color Color;
        public Rectangle Rectangle => new Rectangle((int)Position.X, (int)Position.Y, _texture.Width / 2, _texture.Height / 2);
        public string Score;
        public string PlayerName;
        #endregion

        public AndroidGameOverScores(Texture2D texture, SpriteFont font)
        {
            _texture = texture;
            _font = font;
            PenColor = Color.Black;
            Color = Color.White;
        }
        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, Rectangle, Color);
            if (!string.IsNullOrEmpty(Score))
            {
                var x = (Rectangle.X + (Rectangle.Width / 2)) - (_font.MeasureString(Score).X / 2);
                var y = (Rectangle.Y + (Rectangle.Height / 2)) - (_font.MeasureString(PlayerName).Y / 2);

                spriteBatch.DrawString(_font, Score, new Vector2(x, y), PenColor);
            }

            if (string.IsNullOrEmpty(PlayerName)) return;
            {
                var x = (Rectangle.X + (Rectangle.Width / 2)) - (_font.MeasureString(PlayerName).X / 2);
                var y = (Rectangle.Y + (Rectangle.Height / 2) - 40) - (_font.MeasureString(PlayerName).Y / 2);

                spriteBatch.DrawString(_font, PlayerName, new Vector2(x, y), PenColor);
            }
        }

        public override void Update(GameTime gameTime)
        {

        }
    }
}