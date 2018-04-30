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
using SharedSheep.Player;
using SharedSheep.Trick;

namespace AndroidSheep.Models.States
{
    class AndroidRoundOverState : AndroidState
    {
        private ITrick _trick;
        private int playedIndex;
        private int offset;
        private string prompt;
        public bool nextRound;
        public bool isTrickSet;
        private List<AndroidComponent> _components;
        private List<AndroidButton> roundStats;
        public AndroidRoundOverState(AndroidSheepGame table, GraphicsDevice graphicsDevice, GameContent gameContent) : base(table, graphicsDevice, gameContent)
        {
            nextRound = false;
            isTrickSet = false;
            AndroidButton doneButton = new AndroidButton(gameContent.Button, gameContent.Font)
            {
                Position = new Vector2(_table.screenWidth / 2 - 125, _table.screenHeight / 2 - 300),
                Text = "Next Round"
            };

            doneButton.Click += DoneButton_Click;

            _components = new List<AndroidComponent>()
            {
                doneButton,
            };

            AndroidButton trickWinner = new AndroidButton(gameContent.Button, gameContent.Font)
            {
                Position = new Vector2(_table.screenWidth / 2 - 125, _table.screenHeight / 2 + 200),
            };
            AndroidButton trickScore = new AndroidButton(gameContent.Button, gameContent.Font)
            {
                Position = new Vector2(_table.screenWidth / 2 - 125, _table.screenHeight / 2 + 300),
            };

            roundStats = new List<AndroidButton>()
            {
                trickWinner,
                trickScore
            };
        }

        public string PickingPrompt()
        {
            prompt = "";
            playedIndex = 0;
            if (nextRound)
                prompt += "done";
            return prompt;
        }

        private void DoneButton_Click(object sender, EventArgs e)
        {
            nextRound = true;
        }

        public void SetTrick(ITrick trick)
        {
            _trick = trick;
            playedIndex = 0;
            offset = -400;
            if (!isTrickSet)
            {
                _table.playedCards = new AndroidCard[trick.Count()];
                isTrickSet = true;
            }

            var score = trick.TrickValue();
            var winner = trick.TheWinnerPlayer();
            roundStats[0].Text = "Trick Score: " + score.ToString();
            roundStats[1].Text = "Round Winner: " + winner.Name;
            foreach (var cardPlayerPair in _trick)
            {
                var card = cardPlayerPair.Item2;
                if (card != null)
                {
                    Texture2D cardtexture = _table.gameContent.textureDict[card];
                    AndroidCard trickCard = new AndroidCard(cardtexture, card);
                    trickCard.Position = new Vector2((_table.screenWidth / 2 + offset), _table.screenHeight / 4);
                    offset += 150;
                    trickCard.State = StateType.Playing;
                    trickCard.isTrick = true;
                    _table.playedCards[playedIndex++] = trickCard;
                }
            }
        }
        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin(SpriteSortMode.Immediate);
            foreach (var card in _table.playedCards)
            {
                if (card != null)
                    card.Draw(gameTime, spriteBatch);
            }
            foreach (var component in _components)
            {
                component.Draw(gameTime, spriteBatch);
            }
            foreach (var component in roundStats)
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
            foreach (var component in _components)
            {
                component.Update(gameTime);
            }
        }
    }
}