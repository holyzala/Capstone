using System;
using System.Collections.Generic;
using System.Linq;
using AndroidSheep.Models.Buttons;
using AndroidSheep.Models.Buttons.Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SharedSheep.Player;
using SharedSheep.ScoreSheet;
using SharedSheep.Trick;

namespace AndroidSheep.Models.States
{
    class AndroidPlayState : AndroidState
    {
        private string prompt;
        private List<AndroidButton> _components;
        private List<AndroidLeaderBoardPanel> leaderboardpanel;
        private int playedIndex = 0;
        private ITrick _trick;
        private IScoreSheet _score;
        private int offset;
        private AndroidCard selectedPlayCard;
        private bool wantsToPlay;
        public bool isTrickSet;
        private bool areBarsSet;
        public bool LeaderboardSet;
        public AndroidPlayState(AndroidSheepGame table, GraphicsDevice graphicsDevice, GameContent gameContent) : base(table, graphicsDevice, gameContent)
        {
            selectedPlayCard = null;
            wantsToPlay = false;
            isTrickSet = false;
            areBarsSet = false;
            LeaderboardSet = false;
            AndroidButton leadingCard = new AndroidButton(_table.gameContent.Button2, _table.gameContent.Font)
            {
                Position = new Vector2(_table.screenWidth * 0.8f, _table.screenHeight * 0.25f),
                Text = "Lead Card"
            };
            _components = new List<AndroidButton>()
            {
                leadingCard
            };
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
                card?.Draw(gameTime, spriteBatch);
            }

            foreach (var component in _components)
            {
                component?.Draw(gameTime, spriteBatch);
            }
            if (leaderboardpanel != null)
            {
                foreach (var component in leaderboardpanel)
                {
                    component?.Draw(gameTime, spriteBatch);
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

        public void SetScore(IScoreSheet score)
        {
            _score = score;
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
            if (!LeaderboardSet)
            {
                SetScoreboard();
                LeaderboardSet = true;
            }
            
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
                    var player = cardPlayerPair.Item1;
                    leaderboardpanel[playedIndex].PlayerName = player.Name;
                    leaderboardpanel[playedIndex].Text = "Last Played Card: ";
                    leaderboardpanel[playedIndex].card = trickCard;

                    _table.playedCards[playedIndex++] = trickCard;
                }

            }
        }
        public string PickingPrompt()
        {
            prompt = "";
            playedIndex = 0;
           
            foreach (var player in _table._playerGraphicsDict)
            {
                if (player.Key is LocalPlayer)
                {
                    var playerCards = player.Value.playableCards;
                    if (playerCards != null)
                    {
                        playedIndex = 0;
                        foreach (var card in playerCards)
                        {
                            if (card.played)
                            {
                                selectedPlayCard = card;
                            }
                            playedIndex++;
                        }
                    }
                }
            }

            if (selectedPlayCard != null)
            {
                playedIndex = 0;
                foreach (var card in _table.playableCards)
                {

                    if (card.Equals(selectedPlayCard))
                    {
                        prompt = playedIndex.ToString();
                        break;
                    }
                    playedIndex++;
                }
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
                                if (!card.played)
                                {
                                    newPlayableCards[playedIndex] = card;
                                    playedIndex++;
                                }
                                else
                                {
                                    card.played = false;
                                    selectedPlayCard = null;
                                }
                            }
                            player.Value.playableCards = newPlayableCards;
                        }
                    }
                }
            }

            return prompt;
        }

        public void SetScoreboard()
        {
            var offset = _table.screenWidth / 4 - 150;
            AndroidLeaderBoardPanel playerOneScore = new AndroidLeaderBoardPanel(_table.gameContent.Button3, _table.gameContent.Font)
            {
                Position = new Vector2(offset, 0),
                
            };
            offset += _table.gameContent.Button2.Width - 50;
            AndroidLeaderBoardPanel playerTwoScore = new AndroidLeaderBoardPanel(_table.gameContent.Button3, _table.gameContent.Font)
            {
                Position = new Vector2(offset, 0),

            };
            offset += _table.gameContent.Button2.Width - 50;
            AndroidLeaderBoardPanel playerThreeScore = new AndroidLeaderBoardPanel(_table.gameContent.Button3, _table.gameContent.Font)
            {
                Position = new Vector2(offset, 0),

            };
            offset += _table.gameContent.Button2.Width - 50;
            AndroidLeaderBoardPanel playerFourScore = new AndroidLeaderBoardPanel(_table.gameContent.Button3, _table.gameContent.Font)
            {
                Position = new Vector2(offset, 0),

            };
            leaderboardpanel = new List<AndroidLeaderBoardPanel>()
            {
                playerOneScore,
                playerTwoScore,
                playerThreeScore,
                playerFourScore,
            };

            
        }
    }
}