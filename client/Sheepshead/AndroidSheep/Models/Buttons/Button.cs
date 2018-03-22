using System;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input.Touch;

namespace AndroidSheep.Models.Buttons
{
    public class Button : Component
    {
        #region Fields
        private TouchCollection _currentTouch;
        private SpriteFont _font;
        private bool _isHovering;
        private Texture2D _texture;
        private Color _color;
        private int selected;
        private bool isPlayable;
        private float _scaleFactor;
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

        #region Methods
        public Button(Texture2D texture, SpriteFont font)
        {
            _texture = texture;
            _font = font;
            _color = Color.White;
            PenColor = Color.Black;
            _scaleFactor = .25f;
            isPlayable = true;
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch, int width, int height)
        {
            var prevRect = Rectangle;
            _color = Color.White;
            if (!isPlayable)
            {
              spriteBatch.Draw(_texture, Position, null,
                    Color.White, 0f, Vector2.Zero, _scaleFactor, SpriteEffects.None, 0f);
            }
            else
            {
                if (_isHovering)
                {
                    prevRect.Y = Rectangle.Y - 40;
                    _color = Color.Gray;
                }
                Vector2 origin = new Vector2(width / 2, height / 2);

                if (!string.IsNullOrEmpty(Text))
                {
                    var x = (Rectangle.X + (Rectangle.Width / 2)) - (_font.MeasureString(Text).X / 2);
                    var y = (Rectangle.Y + (Rectangle.Width / 2)) - (_font.MeasureString(Text).Y / 2);
                    spriteBatch.DrawString(_font, Text, new Vector2(x, y), PenColor);
                }
                spriteBatch.Draw(_texture, prevRect, _color);
            }
        }

        public override void Update(GameTime gameTime)
        {
            int y = 0;
            int x = 0;
            bool isInputPressed = false;
            _isHovering = false;

            _currentTouch = TouchPanel.GetState();
            TouchPanel.EnabledGestures = GestureType.DoubleTap;
            if (!isPlayable)
                return;

           
            if (_currentTouch.Count >= 1)
            {
                var touch = _currentTouch[0];
                x = (int)touch.Position.X;
                y = (int)touch.Position.Y;
                var touchRectangle = new Rectangle(x, y, 1, 1);

                if (touchRectangle.Intersects(Rectangle))
                {
                    if (TouchPanel.IsGestureAvailable)
                    {
                        GestureSample gesture = TouchPanel.ReadGesture();
                        if (gesture.GestureType == GestureType.DoubleTap)
                        {

                            isPlayable = false;
                            Debug.WriteLine("I'M DOUBLE TAPPING");

                            Position += new Vector2(-50, -300);
                        }
                    }

                    selected += (int)gameTime.ElapsedGameTime.TotalSeconds;
                    Click?.Invoke(this, new EventArgs());
                    var color = Color.Gray;
                    _isHovering = true;
                }

                isInputPressed = touch.State == TouchLocationState.Pressed || touch.State == TouchLocationState.Moved;

            }
            else
            {
                selected = 0;
            }
           

            #endregion
        }
    }
}