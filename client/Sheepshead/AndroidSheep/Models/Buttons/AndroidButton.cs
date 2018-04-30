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
        private readonly SpriteFont _font;
        private readonly Texture2D _texture;
        private bool _isInputPressed;
        #endregion

        #region Properties
        public event EventHandler Click;
        public bool Clicked { get; set; }
        public Color PenColor { get; set; }
        public Vector2 Position { get; set; }
        public Color Color;
        public Rectangle Rectangle => new Rectangle((int)Position.X, (int)Position.Y, _texture.Width / 2, _texture.Height / 2);
        public string Text; 
        #endregion

        public AndroidButton(Texture2D texture, SpriteFont font)
        {
            _texture = texture;
            _font = font;
            PenColor = Color.Black;
            Color = Color.White;
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, Rectangle, Color);
            if (string.IsNullOrEmpty(Text)) return;
            var x = (Rectangle.X + (Rectangle.Width / 2)) - (_font.MeasureString(Text).X / 2);
            var y = (Rectangle.Y + (Rectangle.Height / 2)) - (_font.MeasureString(Text).Y / 2);
            spriteBatch.DrawString(_font, Text, new Vector2(x, y), PenColor);
        }

        public override void Update(GameTime gameTime)
        {
            _isInputPressed = false;
            _currentTouch = TouchPanel.GetState();
            if (_currentTouch.Count < 1) return;
            var touch = _currentTouch[0];
            var x = (int)touch.Position.X;
            var y = (int)touch.Position.Y;
            var touchRectangle = new Rectangle(x, y, 1, 1);

            if (!TouchPanel.IsGestureAvailable) return;
            if (touchRectangle.Intersects(Rectangle) && TouchPanel.ReadGesture().GestureType == GestureType.DoubleTap)
            {
                Color = Color.Gray;
                Click?.Invoke(this, new EventArgs());
            }
            _isInputPressed = touch.State == TouchLocationState.Pressed || touch.State == TouchLocationState.Moved;
        }
    }
}