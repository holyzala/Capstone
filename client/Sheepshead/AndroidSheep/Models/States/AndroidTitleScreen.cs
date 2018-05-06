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
using AndroidSheep.Models.Buttons;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AndroidSheep.Models.States
{
    class AndroidTitleScreen : AndroidState
    {
        private AndroidButton _button;
        private AndroidButton _logo;
        private bool _startGame;
        private readonly Texture2D _backgroundTexture;
        private readonly Vector2 _backgroundPosition;

        public AndroidTitleScreen(AndroidSheepGame table, GraphicsDevice graphicsDevice, GameContent gameContent) : base(table, graphicsDevice, gameContent)
        {
            _button = new AndroidButton(gameContent.Button, gameContent.Font)
            {
                Position = new Vector2(table.ScreenWidth / 2 - 130, table.ScreenHeight / 2 + 200),
                Text = "Start Game!"
            };

            _button.Click += Button_Click;

            _logo = new AndroidButton(gameContent.Logo, gameContent.Font)
            {
                Position = new Vector2(-100, table.ScreenHeight / 8)
            };

            _backgroundTexture = gameContent.TableTop;
            table.InitializeGameContent();
        }

        private void Button_Click(object sender, EventArgs e)
        {
            _startGame = true;
        }
        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.LinearWrap, null, null);
            spriteBatch.Draw(_backgroundTexture, _backgroundPosition, new Rectangle(0, 0, Table.ScreenWidth, Table.ScreenHeight), Color.White, 0, Vector2.Zero, 1f, SpriteEffects.None, 0);
            spriteBatch.End();

            spriteBatch.Begin();
            _button.Draw(gameTime, spriteBatch);
            _logo.Draw(gameTime, spriteBatch);
            spriteBatch.End();
        }

        public override void Update(GameTime gameTime)
        {
            _button.Update(gameTime);
            if (_startGame)
            {
                Table.LoadGame();
                _startGame = false;
            }
        }
    }
}