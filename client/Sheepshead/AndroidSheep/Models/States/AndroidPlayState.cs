using System;
using System.Collections.Generic;
using AndroidSheep.Models.Buttons;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SharedSheep.Player;

namespace AndroidSheep.Models.States
{
    class AndroidPlayState : AndroidState
    {
        private List<AndroidComponent> _components;

        public AndroidPlayState(AndroidSheepGame table, GraphicsDevice graphicsDevice, GameContent gameContent) :base(table, graphicsDevice, gameContent)
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
                    if (playerCards != null)
                    {
                        foreach (var card in playerCards)
                        {
                            card.Draw(gameTime, spriteBatch);
                        }
                    }
                }
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
                            card.State = StateType.Playing;
                            card.Update(gameTime);
                        }
                    }
                }
            }
        }
    }
}