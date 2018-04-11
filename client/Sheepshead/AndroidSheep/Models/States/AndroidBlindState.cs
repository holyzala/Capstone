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
        private List<AndroidComponent> _components;

        public AndroidBlindState(AndroidSheepGame table, GraphicsDevice graphicsDevice, GameContent gameContent) : base(table, graphicsDevice, gameContent)
        {
            AndroidButton yesButton = new AndroidButton(gameContent.Button, gameContent.Font)
            {
                Position = new Vector2(200, 200),
                Text = "Yes"
            };
            AndroidButton noButton = new AndroidButton(gameContent.Button, gameContent.Font)
            {
                Position = new Vector2(300, 200),
                Text = "No"
            };
            _components = new List<AndroidComponent>()
            {
                yesButton,
                noButton
            };
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
                        card.Draw(gameTime, spriteBatch);
                    }
                }
            }
            foreach (var component in _components)
            {
                component.Draw(gameTime, spriteBatch);
            }
            spriteBatch.End();
        }

        public override void PostUpdate(GameTime gameTime)
        {
            throw new NotImplementedException();
        }

        public override void Update(GameTime gameTime)
        {
            foreach (var component in _components)
            {
                component.Update(gameTime);
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