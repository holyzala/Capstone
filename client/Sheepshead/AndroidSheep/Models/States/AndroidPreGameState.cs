using System;
using System.Collections.Generic;
using AndroidSheep.Models.Buttons;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SharedSheep.Player;

namespace AndroidSheep.Models.States
{
    class AndroidPreGameState : AndroidState
    {
        public AndroidPreGameState(AndroidSheepGame table, GraphicsDevice graphicsDevice, GameContent gameContent) : base(table, graphicsDevice, gameContent)
        {
        }
        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin(SpriteSortMode.Immediate);
            foreach (var player in Table.PlayerGraphicsDict)
            {
                if (!(player.Key is LocalPlayer)) continue;
                var playerCards = player.Value.PlayableCards;
                if (playerCards == null) continue;
                foreach (var card in playerCards)
                {
                    card.Draw(gameTime, spriteBatch);
                }
            }
            spriteBatch.End();
        }

        public override void Update(GameTime gameTime)
        {
            foreach (var card in Table.BlindList)
            {
                card.State = StateType.PreGame;
                card.Update(gameTime);
            }
            foreach (var player in Table.PlayerGraphicsDict)
            {
                if (!(player.Key is LocalPlayer)) continue;
                var playerCards = player.Value.PlayableCards;
                if (playerCards == null) continue;
                foreach (var card in playerCards)
                {
                    card.State = StateType.PreGame;
                    card.Update(gameTime);
                }
            }
        }
    }
}