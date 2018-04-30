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
    class AndroidGameOverState : AndroidState
    {
        private List<AndroidButton> leaderboardpanel;
        public AndroidGameOverState(AndroidSheepGame table, GraphicsDevice graphicsDevice, GameContent gameContent) : base(table, graphicsDevice, gameContent)
        {
            var offset = 0;
            AndroidButton playerOneScore = new AndroidButton(gameContent.Button2, gameContent.Font)
            {
                Position = new Vector2(0, offset)
            };
            offset += gameContent.Button2.Height /2;
            AndroidButton playerTwoScore = new AndroidButton(gameContent.Button2, gameContent.Font)
            {
                Position = new Vector2(0, offset)
            };
            offset += gameContent.Button2.Height / 2;
            AndroidButton playerThreeScore = new AndroidButton(gameContent.Button2, gameContent.Font)
            {
                Position = new Vector2(0, offset)
            };
            offset += gameContent.Button2.Height / 2;
            AndroidButton playerFourScore = new AndroidButton(gameContent.Button2, gameContent.Font)
            {
                Position = new Vector2(0, offset)
            };
            offset += gameContent.Button2.Height / 2;
            AndroidButton playerFiveScore = new AndroidButton(gameContent.Button2, gameContent.Font)
            {
                Position = new Vector2(0, offset)
            };
            leaderboardpanel = new List<AndroidButton>()
            {
                playerOneScore,
                playerTwoScore,
                playerThreeScore,
                playerFourScore,
                playerFiveScore
            };
        }
        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
        }

        public override void PostUpdate(GameTime gameTime)
        {
        }

        public override void Update(GameTime gameTime)
        {
        }
    }
}