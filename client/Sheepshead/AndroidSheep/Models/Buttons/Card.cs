using System;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input.Touch;

namespace AndroidSheep.Models.Buttons
{
    public class Card : Component
    {
        #region Fields
        private TouchCollection _currentTouch;
        private SpriteFont _font;
        private bool _isHovering;
        public Color color;
        public bool inPlayedArea;
        #endregion

        #region Properties
        public Texture2D texture { get; set; }
        public PlayedArea playedArea;
        public event EventHandler Click;
        public bool Clicked { get; private set; }
        public Color PenColor { get; set; }
        public Vector2 Position { get; set; }
        public Rectangle Rectangle
        {
            get
            {
                return new Rectangle((int)Position.X, (int)Position.Y, texture.Width / 2, texture.Height / 2);
            }
        }
        public string Text { get; set; }
        #endregion

        #region Methods
        public Card(Texture2D cardtexture, SpriteFont font, PlayedArea playarea)
        {
            playedArea = playarea;
            texture = cardtexture;
            _font = font;
            color = Color.White;
            PenColor = Color.Black;
            inPlayedArea = false;
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch, int width, int height)
        {
            var prevRect = Rectangle;
            color = Color.White;
            if (inPlayedArea)
                return;
            else
            {
                if (_isHovering)
                {
                    prevRect.Y = Rectangle.Y - 40;
                    color = Color.Gray;
                }
                Vector2 origin = new Vector2(texture.Width / 2, texture.Height / 2);

                if (!string.IsNullOrEmpty(Text))
                {
                    var x = (Rectangle.X + (Rectangle.Width / 2)) - (_font.MeasureString(Text).X / 2);
                    var y = (Rectangle.Y + (Rectangle.Width / 2)) - (_font.MeasureString(Text).Y / 2);
                    spriteBatch.DrawString(_font, Text, new Vector2(x, y), PenColor);
                }
                spriteBatch.Draw(texture, prevRect, color);

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
            if (inPlayedArea)
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
                            inPlayedArea = true;
                            playedArea.AddCardToPlayArea(this);
                        }
                    }

                    Click?.Invoke(this, new EventArgs());
                    var color = Color.Gray;
                    _isHovering = true;
                }

                isInputPressed = touch.State == TouchLocationState.Pressed || touch.State == TouchLocationState.Moved;

            }
            


            #endregion
        }
    }
}