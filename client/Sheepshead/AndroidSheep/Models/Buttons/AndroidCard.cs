using System;
using System.Diagnostics;
using AndroidSheep.Models.States;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input.Touch;
using SharedSheep.Card;

namespace AndroidSheep.Models.Buttons
{
    public class AndroidCard : AndroidComponent
    {
        #region Fields
        private TouchCollection _currentTouch;
        private bool _isHovering;
        private Texture2D _texture;
        private Color _color;
        private bool isPlayable;
        private int x;
        private int y;
        private bool IsInputPressed;
        #endregion

        #region Properties 
        public StateType State { get; set; }
        public Texture2D Texture { get; set; }
        public event EventHandler Click;
        public bool Clicked { get; private set; }
        public Vector2 Position { get; set; }
        public Rectangle Rectangle
        {
            get
            {
                return new Rectangle((int)Position.X, (int)Position.Y, _texture.Width / 2, _texture.Height / 2);
            }
        }
        #endregion

        #region Blind
        public ICard _card;
        public bool IsBlind { get; set; }
        public bool IsSelected { get; set; }
        private Color _blindColor;
        #endregion

        #region Playing

        #endregion


        public AndroidCard(Texture2D texture, ICard card)
        {
            _texture = texture;
            _color = Color.White;
            _card = card;
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            _color = Color.White;
            _blindColor = Color.Gray;

            var prevRect = Rectangle;
            if (IsSelected)
            {
                _color = _blindColor;
            }
            spriteBatch.Draw(_texture, Rectangle, _color);
        }

        public override void Update(GameTime gameTime)
        {
            IsInputPressed = false;
            _currentTouch = TouchPanel.GetState();
            if (_currentTouch.Count >= 1)
            {
                var touch = _currentTouch[0];
                x = (int)touch.Position.X;
                y = (int)touch.Position.Y;
                var touchRectangle = new Rectangle(x, y, 1, 1);

                if (TouchPanel.IsGestureAvailable)
                {
                    if (touchRectangle.Intersects(Rectangle) && TouchPanel.ReadGesture().GestureType == GestureType.DoubleTap)
                    {
                        IsSelected = IsSelected == true ? false : true;
                        if (!IsBlind && IsSelected)
                            Position = new Vector2(Position.X, Position.Y - 40);
                        else if (!IsBlind)
                        {
                            Position = new Vector2(Position.X, Position.Y + 40);
                        }
                        Click?.Invoke(this, new EventArgs());
                    }
                    IsInputPressed = touch.State == TouchLocationState.Pressed || touch.State == TouchLocationState.Moved;
                }
            }
        }
    }
}