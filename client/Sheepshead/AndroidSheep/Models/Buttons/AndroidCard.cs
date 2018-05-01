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
        public Texture2D Texture { get; set; }
        private Color _color;
        private int _x;
        private int _y;
        private bool _isInputPressed;
        #endregion

        #region Properties 
        public StateType State { get; set; }
        public event EventHandler Click;
        public bool Clicked { get; private set; }
        public Vector2 Position { get; set; }
        public Rectangle Rectangle => new Rectangle((int)Position.X, (int)Position.Y, Texture.Width / 2, Texture.Height / 2);

        #endregion

        #region Blind
        public ICard Card;
        public bool IsBlind { get; set; }
        public bool IsSelected { get; set; }
        private Color _blindColor;
        #endregion

        #region Playing
        public bool Played;
        public bool IsPlayable;
        public bool IsTrick;
        #endregion


        public AndroidCard(Texture2D texture, ICard card)
        {
            Texture = texture;
            _color = Color.White;
            Card = card;
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
            if (IsPlayable || IsTrick)
            {
                tempColor = Color.White;
            }

            if (Played)
            {
                tempColor = Color.Yellow;
            }
            spriteBatch.Draw(Texture, Rectangle, tempColor);
        }

        private void UpdatePlaying(GameTime gameTime)
        {
            if (!IsPlayable)
            {
                return;
            }
            _currentTouch = TouchPanel.GetState();
            if (_currentTouch.Count < 1) return;
            var touch = _currentTouch[0];
            _x = (int)touch.Position.X;
            _y = (int)touch.Position.Y;
            var touchRectangle = new Rectangle(_x, _y, 1, 1);
            if (!TouchPanel.IsGestureAvailable) return;
            if (touchRectangle.Intersects(Rectangle) && TouchPanel.ReadGesture().GestureType == GestureType.DoubleTap)
            {
                Played = true;
                IsPlayable = false;
                Click?.Invoke(this, new EventArgs());
            }
            _isInputPressed = touch.State == TouchLocationState.Pressed || touch.State == TouchLocationState.Moved;
        }
    
        private void DrawPicking(GameTime gameTime, SpriteBatch spriteBatch)
        {
            _color = Color.White;
            _blindColor = Color.Gray;

            if (IsSelected)
            {
                _color = _blindColor;
            }
            spriteBatch.Draw(Texture, Rectangle, _color);
        }

        private void UpdatePicking(GameTime gameTime)
        {
            _isInputPressed = false;
            _currentTouch = TouchPanel.GetState();
            if (_currentTouch.Count < 1) return;
            var touch = _currentTouch[0];
            _x = (int)touch.Position.X;
            _y = (int)touch.Position.Y;
            var touchRectangle = new Rectangle(_x, _y, 1, 1);

            if (!TouchPanel.IsGestureAvailable) return;
            if (touchRectangle.Intersects(Rectangle) && TouchPanel.ReadGesture().GestureType == GestureType.DoubleTap)
            {
                ChangeSelectionPicking();                   
                if (!IsBlind && IsSelected)
                    Click?.Invoke(this, new EventArgs());
            }
            _isInputPressed = touch.State == TouchLocationState.Pressed || touch.State == TouchLocationState.Moved;
        }

        public void ChangeSelectionPicking()
        {
            IsSelected = IsSelected != true;
            if (IsBlind) return;
            Position = IsSelected ? new Vector2(Position.X, Position.Y - 40) : new Vector2(Position.X, Position.Y + 40);
        }
    }
}