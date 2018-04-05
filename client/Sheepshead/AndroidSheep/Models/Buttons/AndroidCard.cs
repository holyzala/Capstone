using System;
using System.Diagnostics;
using AndroidSheep.Models.States;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input.Touch;

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
        #endregion

        #region Properties 
        public StateType State { get; set; }
        public Texture2D Texture { get; set; }
        public event EventHandler Click;
        public bool Clicked { get; private set; }
        public Vector2 Position { get; set; }
        public bool canClick { get; set; }
        public bool canSwap { get; set; }
        public bool isPicking { get; set; }
        public Rectangle Rectangle
        {
            get
            {
                return new Rectangle((int)Position.X, (int)Position.Y, _texture.Width / 2, _texture.Height / 2);
            }
        }
        #endregion

        public AndroidCard(Texture2D texture)
        {
            _texture = texture;
            _color = Color.White;
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            switch (State)
            {
                case StateType.PreGame:
                    DrawPreGame(gameTime, spriteBatch);
                    break;
                case StateType.Blind:
                    DrawBlind(gameTime, spriteBatch);
                    break;
                case StateType.Playing:
                    DrawPlay(gameTime, spriteBatch);
                    break;
                default:
                    break;
            }
        }

        public override void Update(GameTime gameTime)
        {
            switch (State)
            {
                case StateType.PreGame:
                    UpdatePreGame(gameTime);
                    break;
                case StateType.Blind:
                    UpdateBlind(gameTime);
                    break;
                case StateType.Playing:
                    UpdatePlay(gameTime);
                    break;
                default:
                    break;
            }
        }

        public void DrawPreGame(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, Rectangle, _color);
        }
        public void UpdatePreGame(GameTime gameTime)
        {

        }

        public void DrawBlind(GameTime gameTime, SpriteBatch spriteBatch)
        {
            var prevRect = Rectangle;
            _color = Color.Gray;
            prevRect = new Rectangle(150, 150, 1, 1);
            if (_isHovering && canClick)
            {
                prevRect.Y = Rectangle.Y - 40;
                _color = Color.Gray;
            }
            spriteBatch.Draw(_texture, prevRect, _color);

        }

        public void UpdateBlind(GameTime gameTime)
        {
            int y = 0;
            int x = 0;
            bool isInputPressed = false;
            _isHovering = true;

            _currentTouch = TouchPanel.GetState();
            TouchPanel.EnabledGestures = GestureType.DoubleTap;
            if (_currentTouch.Count >= 1)
            {
                var touch = _currentTouch[0];
                x = (int)touch.Position.X;
                y = (int)touch.Position.Y;
                var touchRectangle = new Rectangle(x, y, 1, 1);
                if (touchRectangle.Intersects(Rectangle))
                {
                    if (!isPicking && isPlayable)
                    {
                        if (TouchPanel.IsGestureAvailable)
                        {
                            GestureSample gesture = TouchPanel.ReadGesture();
                            if (gesture.GestureType == GestureType.DoubleTap)
                            {

                                isPlayable = false;
                                Position += new Vector2(-50, -300);
                            }
                        }
                    }

                    Click?.Invoke(this, new EventArgs());
                    var color = Color.Gray;
                    _isHovering = true;
                }

                isInputPressed = touch.State == TouchLocationState.Pressed || touch.State == TouchLocationState.Moved;

            }
        }
        public void DrawPlay(GameTime gameTime, SpriteBatch spriteBatch)
        {
            var prevRect = Rectangle;
            _color = Color.Gray;
            prevRect = new Rectangle(150, 150, 1, 1);
            if (_isHovering)
            {
                prevRect.Y = Rectangle.Y - 40;
                _color = Color.Gray;
            }
            spriteBatch.Draw(_texture, prevRect, _color);
        }

        public void UpdatePlay(GameTime gameTime)
        {
            int y = 0;
            int x = 0;
            bool isInputPressed = false;
            _isHovering = false;

            _currentTouch = TouchPanel.GetState();
            TouchPanel.EnabledGestures = GestureType.DoubleTap;



            if (_currentTouch.Count >= 1)
            {
                var touch = _currentTouch[0];
                x = (int)touch.Position.X;
                y = (int)touch.Position.Y;
                var touchRectangle = new Rectangle(x, y, 1, 1);
                if (touchRectangle.Intersects(Rectangle))
                {
                    if (!isPicking && isPlayable)
                    {
                        if (TouchPanel.IsGestureAvailable)
                        {
                            GestureSample gesture = TouchPanel.ReadGesture();
                            if (gesture.GestureType == GestureType.DoubleTap)
                            {

                                isPlayable = false;
                                Position += new Vector2(-50, -300);
                            }
                        }
                    }

                    Click?.Invoke(this, new EventArgs());
                    var color = Color.Gray;
                    _isHovering = true;
                }

                isInputPressed = touch.State == TouchLocationState.Pressed || touch.State == TouchLocationState.Moved;

            }
        }

    }
}