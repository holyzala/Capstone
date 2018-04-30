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
        public Texture2D _texture { get; set; }
        private Color _color;
        private int x;
        private int y;
        private bool IsInputPressed;
        #endregion

        #region Properties 
        public StateType State { get; set; }
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
        public bool played;
        public bool isPlayable;
        public bool isTrick;
        #endregion


        public AndroidCard(Texture2D texture, ICard card)
        {
            _texture = texture;
            _color = Color.White;
            _card = card;
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            switch (State)
            {
                case StateType.Picking:
                    DrawPicking(gameTime, spriteBatch);
                    break;
                case StateType.Playing:
                    DrawPlaying(gameTime, spriteBatch);
                    break;
                default:
                    break;
            }
        }

        public override void Update(GameTime gameTime)
        {
            switch (State)
            {
                case StateType.Picking:
                    UpdatePicking(gameTime);
                    break;
                case StateType.Playing:
                    UpdatePlaying(gameTime);
                    break;
                default:
                    break;
            }
        }

        private void DrawPlaying(GameTime gameTime, SpriteBatch spriteBatch)
        {
            var tempColor = Color.Gray;
            if (isPlayable || isTrick)
            {
                tempColor = Color.White;
            }

            if (played)
            {
                tempColor = Color.Yellow;
            }
            spriteBatch.Draw(_texture, Rectangle, tempColor);
        }

        private void UpdatePlaying(GameTime gameTime)
        {
            if (!isPlayable)
            {
                return;
            }
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
                        played = true;
                        isPlayable = false;
                        Click?.Invoke(this, new EventArgs());
                    }
                    IsInputPressed = touch.State == TouchLocationState.Pressed || touch.State == TouchLocationState.Moved;
                }
            }
        }
    
        private void DrawPicking(GameTime gameTime, SpriteBatch spriteBatch)
        {
            _color = Color.White;
            _blindColor = Color.Gray;

            if (IsSelected)
            {
                _color = _blindColor;
            }
            spriteBatch.Draw(_texture, Rectangle, _color);
        }

        private void UpdatePicking(GameTime gameTime)
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
                       ChangeSelectionPicking();                   
                       if (!IsBlind && IsSelected)
                        Click?.Invoke(this, new EventArgs());
                    }
                    IsInputPressed = touch.State == TouchLocationState.Pressed || touch.State == TouchLocationState.Moved;
                }
            }
        }

        public void ChangeSelectionPicking()
        {
            IsSelected = IsSelected == true ? false : true;
            if (!IsBlind)
            {
                if (IsSelected)
                {
                    Position = new Vector2(Position.X, Position.Y - 40);
                }
                else
                {
                    Position = new Vector2(Position.X, Position.Y + 40);
                }
            }
        }
    }
}