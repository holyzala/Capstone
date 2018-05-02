using System;
using System.Collections.Generic;
using System.Linq;
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
        private int _playedIndex;
        private int _offset;
        private string _prompt;
        public bool NextRound;
        public bool IsTrickSet;
        private readonly List<AndroidComponent> _components;
        private readonly List<AndroidButton> _roundStats;

        public AndroidRoundOverState(AndroidSheepGame table, GraphicsDevice graphicsDevice, GameContent gameContent) : base(table, graphicsDevice, gameContent)
        {
            NextRound = false;
            IsTrickSet = false;
            AndroidButton doneButton = new AndroidButton(gameContent.Button, gameContent.Font)
            {
                Position = new Vector2(Table.ScreenWidth / 2 - 125, Table.ScreenHeight / 2 - 300),
                Text = "Next Game"
            };

            doneButton.Click += DoneButton_Click;

            _components = new List<AndroidComponent>()
            {
                doneButton,
            };

            AndroidButton trickWinner = new AndroidButton(gameContent.Button, gameContent.Font)
            {
                Position = new Vector2(Table.ScreenWidth / 2 - 125, Table.ScreenHeight / 2 + 200),
            };
            AndroidButton trickScore = new AndroidButton(gameContent.Button, gameContent.Font)
            {
                Position = new Vector2(Table.ScreenWidth / 2 - 125, Table.ScreenHeight / 2 + 300),
            };

            _roundStats = new List<AndroidButton>()
            {
                trickWinner,
                trickScore
            };
        }

        public string PickingPrompt()
        {
            _prompt = "";
            _playedIndex = 0;
            if (NextRound)
                _prompt += "done";
            return _prompt;
        }

        private void DoneButton_Click(object sender, EventArgs e)
        {
            NextRound = true;
        }

        public void SetTrick(ITrick trick)
        {
            _trick = trick;
            _playedIndex = 0;
            _offset = -400;
            if (!IsTrickSet)
            {
                Table.PlayedCards = new AndroidCard[trick.Count()];
                IsTrickSet = true;
            }

            var score = trick.TrickValue();
            var winner = trick.TheWinnerPlayer();
            _roundStats[0].Text = "Trick Score: " + score.ToString();
            _roundStats[1].Text = "Round Winner: " + winner.Name;
            foreach (var cardPlayerPair in _trick)
            {
                var card = cardPlayerPair.Item2;
                if (card == null) continue;
                var cardtexture = Table.GameContent.TextureDict[card];
                var trickCard = new AndroidCard(cardtexture, card)
                {
                    Position = new Vector2((Table.ScreenWidth / 2 + _offset), Table.ScreenHeight / 4.0f)
                };
                _offset += 150;
                trickCard.State = StateType.Playing;
                trickCard.IsTrick = true;
                Table.PlayedCards[_playedIndex++] = trickCard;
            }
        }
        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin(SpriteSortMode.Immediate);
            foreach (var card in Table.PlayedCards)
            {
                card?.Draw(gameTime, spriteBatch);
            }
            foreach (var component in _components)
            {
                component?.Draw(gameTime, spriteBatch);
            }
            foreach (var component in _roundStats)
            {
                component?.Draw(gameTime, spriteBatch);
            }
            spriteBatch.End();
        }

        public override void Update(GameTime gameTime)
        {
            foreach (var player in Table.PlayerGraphicsDict)
            {
                if (!(player.Key is LocalPlayer)) continue;
                var playerCards = player.Value.PlayableCards;
                if (playerCards == null) continue;
                foreach (var card in playerCards)
                {
                    card.State = StateType.RoundOver;
                    card.Update(gameTime);
                }
            }
            foreach (var component in _components)
            {
                component.Update(gameTime);
            }
        }
    }
}