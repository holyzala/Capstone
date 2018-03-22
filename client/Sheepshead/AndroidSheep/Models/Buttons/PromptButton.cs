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
using Microsoft.Xna.Framework.Input.Touch;

namespace AndroidSheep.Models.Buttons
{
    public class ButtonPrompt : Component
    {
        #region Fields
        private Texture2D _texture;
        private PromptType _prompt_type;
        private TouchCollection _currentTouch;
        private SpriteFont _font;
        private Color color;
        private bool _isHovering;
        #endregion

        #region Properties
        public event EventHandler Click;
        public bool Clicked { get; private set; }
        public Color PenColor { get; set; }
        public Vector2 Position { get; set; }
        public Rectangle Rectangle
        {
            get
            {
                return new Rectangle((int)Position.X, (int)Position.Y, _texture.Width / 2, _texture.Height / 2);
            }
        }
        public string Text { get; set; }
        #endregion

        public ButtonPrompt(PromptType prompt_type, Texture2D texture, SpriteFont font)
        {
            PenColor = Color.Black;
            _prompt_type = prompt_type;
            _texture = texture;
            _font = font;

        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch, int height, int width)
        {
            color = Color.White;
            bool OptionChose = false;
            switch (_prompt_type)
            {
                case PromptType.Pick:
                    while (!OptionChose)
                    {
                        if (_isHovering)
                            color = Color.Gray;

                        else
                            color = Color.White;

                        spriteBatch.Draw(_texture, Rectangle, color);
                        if (!string.IsNullOrEmpty(Text))
                        {
                            var x = (Rectangle.X + (Rectangle.Width / 2)) - (_font.MeasureString(Text).X / 2);
                            var y = (Rectangle.Y + (Rectangle.Width / 2)) - (_font.MeasureString(Text).Y / 2);
                            spriteBatch.DrawString(_font, Text, new Vector2(x, y), PenColor);
                        }
                    }
                    break;
                default:
                    break;

            }
        }

        public override void Update(GameTime gameTime)
        {
            int y = 0;
            int x = 0;
            bool isInputPressed = false;
            _isHovering = false;

            _currentTouch = TouchPanel.GetState();
            if (_currentTouch.Count >= 1)
            {
                var touch = _currentTouch[0];
                x = (int)touch.Position.X;
                y = (int)touch.Position.Y;
                var touchRectangle = new Rectangle(x, y, 1, 1);

                if (touchRectangle.Intersects(Rectangle))
                {
                    Click?.Invoke(this, new EventArgs());
                    var color = Color.Gray;
                    _isHovering = true;
                }
                isInputPressed = touch.State == TouchLocationState.Pressed || touch.State == TouchLocationState.Moved;
            }
        }
    }
}