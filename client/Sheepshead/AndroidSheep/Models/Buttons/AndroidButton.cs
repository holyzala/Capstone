using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input.Touch;

namespace AndroidSheep.Models.Buttons
{
    class AndroidButton : AndroidComponent
    {
        #region Fields
        private TouchCollection _currentTouch;
        private SpriteFont _font;
        private bool _isHovering;
        private TouchCollection _prevTouch;
        private Texture2D _texture;
        #endregion

        #region Properties
        public event EventHandler Click;
        public bool Clicked { get; set; }
        public Color PenColor { get; set; }
        public Vector2 Position { get; set; }
        public Rectangle Rectangle
        {
            get
            {
                return new Rectangle((int)Position.X, (int)Position.Y, _texture.Width / 2, _texture.Height / 2);
            }
        }
        public string Text; 
        #endregion

        public AndroidButton(Texture2D texture, SpriteFont font)
        {
            _texture = texture;
            _font = font;
            PenColor = Color.Black;
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            var color = Color.White;
            if (_isHovering)
            {
                color = Color.Gray;
            }
            spriteBatch.Draw(_texture, Rectangle, color);

            if (!string.IsNullOrEmpty(Text))
            {
                var x = (Rectangle.X + (Rectangle.Width / 2)) - (_font.MeasureString(Text).X / 2);
                var y = (Rectangle.Y + (Rectangle.Height / 2)) - (_font.MeasureString(Text).Y / 2);

                spriteBatch.DrawString(_font, Text, new Vector2(x, y), PenColor);
            }
        }

        public override void Update(GameTime gameTime)
        {
            int y = 0;
            int x = 0;
            _prevTouch = _currentTouch;
            _currentTouch = TouchPanel.GetState();

            _isHovering = false;

            if (_currentTouch.Count >= 1)
            {
                var touch = _currentTouch[0];
                x = (int)touch.Position.X;
                y = (int)touch.Position.Y;
                var touchRectangle = new Rectangle(x, y, 1, 1);
                if (touchRectangle.Intersects(Rectangle))
                {
                    _isHovering = true;
                }
                if(touch.State == TouchLocationState.Pressed || touch.State == TouchLocationState.Moved)
                {
                    Click?.Invoke(this, new EventArgs());
                }
            }
        }
    }
}