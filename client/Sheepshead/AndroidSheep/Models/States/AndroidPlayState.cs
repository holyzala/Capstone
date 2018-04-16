using System;
using System.Collections.Generic;
using AndroidSheep.Models.Buttons;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SharedSheep.Player;
using SharedSheep.Trick;

namespace AndroidSheep.Models.States
{
    class AndroidPlayState : AndroidState
    {
        private string prompt;
        private List<AndroidComponent> _components;
        private int playedIndex = 0;
        private ITrick _trick;
        private int offset = -200;
        private AndroidCard selectedPlayCard;
        public AndroidPlayState(AndroidSheepGame table, GraphicsDevice graphicsDevice, GameContent gameContent) : base(table, graphicsDevice, gameContent)
        {
            _components = new List<AndroidComponent>();
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
                if(card != null) 
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
                            if (card.played)
                            {
                                selectedPlayCard = card;
                            }
                            card.State = StateType.Playing;
                            card.Update(gameTime);
                        }
                    }
                }
            }
        }

        public void SetTrick(ITrick trick)
        {
            _trick = trick;
            playedIndex = 0;
            offset = -200;
            foreach (var cardPlayerPair in _trick)
            {
                var card = cardPlayerPair.Item2;
                if (card != null)
                {
                    Texture2D cardtexture = _table.gameContent.textureDict[card];
                    AndroidCard trickCard = new AndroidCard(cardtexture, card);
                    trickCard.Position = new Vector2((_table.screenWidth / 2 + offset), _table.screenHeight / 4);
                    offset += 100;
                    trickCard.State = StateType.Playing;
                    _table. playedCards[playedIndex++] = trickCard;
                }
            }
        }
        public string PickingPrompt()
        {
            prompt = "";
            foreach (var player in _table._playerGraphicsDict)
            {
                if (player.Key is LocalPlayer)
                {
                    var playerCards = player.Value.playableCards;
                    AndroidCard[] newPlayableCards = new AndroidCard[playerCards.Length - 1];
                    if (playerCards != null)
                    {
                        playedIndex = 0;
                        foreach (var card in playerCards)
                        {
                            if (card.played)
                            {
                                prompt = playedIndex.ToString();
                                break;
                            }
                            playedIndex++;
                        }
                    }
                }
            }

            return prompt;
        }
    }
}