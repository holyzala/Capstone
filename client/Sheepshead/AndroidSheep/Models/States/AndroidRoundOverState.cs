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
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SharedSheep.Player;

namespace AndroidSheep.Models.States
{
    class AndroidRoundOverState : AndroidState
    {
        public AndroidRoundOverState(AndroidSheepGame table, GraphicsDevice graphicsDevice, GameContent gameContent) : base(table, graphicsDevice, gameContent)
        {
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin(SpriteSortMode.Immediate);

            foreach (var player in _table._playerGraphicsDict)
            {
                if (player.Key is LocalPlayer)
                {
                    var playerCards = player.Value.playableCards;
                    if (playerCards != null)
                    {
                        foreach (var card in playerCards)
                        {
                            card.Draw(gameTime, spriteBatch);
                        }
                    }
                }
            }
            foreach (var card in _table.playedCards)
            {
                if (card != null)
                    card.Draw(gameTime, spriteBatch);
            }
            spriteBatch.End();
        }

        public override void PostUpdate(GameTime gameTime)
        {
            throw new NotImplementedException();
        }

        public override void Update(GameTime gameTime)
        {
            foreach (var player in _table._playerGraphicsDict)
            {
                if (player.Key is LocalPlayer)
                {
                    var playerCards = player.Value.playableCards;
                    if (playerCards != null)
                    {
                        foreach (var card in playerCards)
                        {
                            card.State = StateType.RoundOver;
                            card.Update(gameTime);
                        }
                    }
                }
            }
        }
    }
}