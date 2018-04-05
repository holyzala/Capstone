using System;
using System.Collections.Generic;
using AndroidSheep.Models.Buttons;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SharedSheep.Player;

namespace AndroidSheep.Models.States
{
    class AndroidBlindState : AndroidState
    {
        private AndroidCard cardSwapOne;
        private AndroidCard cardSwapTwo;
        private List<AndroidComponent> _components;

        public AndroidBlindState(AndroidSheepGame table, GraphicsDevice graphicsDevice, GameContent gameContent) : base(table, graphicsDevice, gameContent)
        {
            _components = new List<AndroidComponent>();
        }
        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            foreach (var player in _table._playerGraphicsDict)
            {
                if (player.Key is LocalPlayer)
                {
                    var playerCards = player.Value.playableCards;
              
                    foreach (var card in playerCards)
                    {
                        if (card.canSwap)
                        {

                        }
                        card.Draw(gameTime, spriteBatch);
                    }
                }
            }
            foreach (var card in _table._blindList)
            {
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
            foreach (var card in _table._blindList)
            {
                card.State = StateType.Blind;
                card.Update(gameTime);
            }
            foreach (var player in _table._playerGraphicsDict)
            {
                if (player.Key is LocalPlayer)
                {
                    var playerCards = player.Value.playableCards;
                    if (playerCards != null)
                    {
                        foreach (var card in playerCards)
                        {
                            card.State = StateType.Blind;
                            card.Update(gameTime);
                        }
                    }
                }
            }
        }
    }
}