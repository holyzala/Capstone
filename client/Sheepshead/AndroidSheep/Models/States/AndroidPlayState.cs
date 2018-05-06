using System;
using System.Collections.Generic;
using System.Linq;
using AndroidSheep.Models.Buttons;
using AndroidSheep.Models.Buttons.Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SharedSheep.Player;
using SharedSheep.ScoreSheet;
using SharedSheep.Table;
using SharedSheep.Trick;

namespace AndroidSheep.Models.States
{
    class AndroidPlayState : AndroidState
    {
        private string _prompt;
        private readonly List<AndroidButton> _components;
        private List<AndroidLeaderBoardPanel> _leaderboardpanel;
        private int _playedIndex = 0;
        private ITrick _trick;
        private IScoreSheet _score;
        private int _offset;
        private AndroidCard _selectedPlayCard;
        private bool _wantsToPlay;
        private bool _areBarsSet;
        private IPlayer _picker;
        private AndroidButton _pickerInfo;
        public bool IsTrickSet;
        public bool LeaderboardSet;

        public AndroidPlayState(AndroidSheepGame table, GraphicsDevice graphicsDevice, GameContent gameContent) : base(table, graphicsDevice, gameContent)
        {
            _selectedPlayCard = null;
            _wantsToPlay = false;
            IsTrickSet = false;
            _areBarsSet = false;
            LeaderboardSet = false;
            AndroidButton rankInfo = new AndroidButton(Table.GameContent.Ranks, Table.GameContent.Font)
            {
                Position = new Vector2(0, Table.ScreenHeight / 2 - 100)
            };
            _pickerInfo = new AndroidButton(Table.GameContent.Button3, Table.GameContent.Font)
            {
                Position = new Vector2(Table.ScreenWidth * 0.8f, Table.ScreenHeight * 0.25f)
           };
            _components = new List<AndroidButton>()
            {
                rankInfo,
                _pickerInfo
            };
        }

        public void SetPicker(IPlayer picker)
        {
            _picker = picker;
            _pickerInfo.Text = "Picker: "  + _picker.Name;
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

            foreach (var card in Table.PlayedCards)
            {
                card?.Draw(gameTime, spriteBatch);
            }

            foreach (var component in _components)
            {
                component?.Draw(gameTime, spriteBatch);
            }
            if (_leaderboardpanel != null)
            {
                foreach (var component in _leaderboardpanel)
                {
                    component?.Draw(gameTime, spriteBatch);
                }
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
                    if (card.Played)
                    {
                        _selectedPlayCard = card;
                    }
                    card.State = StateType.Playing;
                    card.Update(gameTime);
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
            _playedIndex = 0;
            _offset = -400;

            if (!IsTrickSet)
            {
                Table.PlayedCards = new AndroidCard[trick.Count()];
                IsTrickSet = true;
            }
            if (!LeaderboardSet)
            {
                SetScoreboard();
                LeaderboardSet = true;
            }
            
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
                var player = cardPlayerPair.Item1;
                _leaderboardpanel[_playedIndex].PlayerName = player.Name;
                _leaderboardpanel[_playedIndex].Text = "Card Played: ";
                _leaderboardpanel[_playedIndex].Card = trickCard;

                Table.PlayedCards[_playedIndex++] = trickCard;
            }
        }

        public void SetScoreboard()
        {
            var offset = Table.ScreenWidth / 4 - 150;
            AndroidLeaderBoardPanel playerOneScore = new AndroidLeaderBoardPanel(Table.GameContent.Button3, Table.GameContent.Font)
            {
                Position = new Vector2(offset, 0),

            };
            offset += Table.GameContent.Button2.Width - 50;
            AndroidLeaderBoardPanel playerTwoScore = new AndroidLeaderBoardPanel(Table.GameContent.Button3, Table.GameContent.Font)
            {
                Position = new Vector2(offset, 0),

            };
            offset += Table.GameContent.Button2.Width - 50;
            AndroidLeaderBoardPanel playerThreeScore = new AndroidLeaderBoardPanel(Table.GameContent.Button3, Table.GameContent.Font)
            {
                Position = new Vector2(offset, 0),

            };
            offset += Table.GameContent.Button2.Width - 50;
            AndroidLeaderBoardPanel playerFourScore = new AndroidLeaderBoardPanel(Table.GameContent.Button3, Table.GameContent.Font)
            {
                Position = new Vector2(offset, 0),

            };
            _leaderboardpanel = new List<AndroidLeaderBoardPanel>()
            {
                playerOneScore,
                playerTwoScore,
                playerThreeScore,
                playerFourScore,
            };
        }
        public string PickingPrompt()
        {
            _prompt = "";
            _playedIndex = 0;
           
            foreach (var player in Table.PlayerGraphicsDict)
            {
                if (!(player.Key is LocalPlayer)) continue;
                var playerCards = player.Value.PlayableCards;
                if (playerCards == null) continue;
                _playedIndex = 0;
                foreach (var card in playerCards)
                {
                    if (card.Played)
                    {
                        _selectedPlayCard = card;
                    }
                    _playedIndex++;
                }
            }

            if (_selectedPlayCard == null) return _prompt;
            {
                _playedIndex = 0;
                foreach (var card in Table.PlayableCards)
                {

                    if (card.Equals(_selectedPlayCard))
                    {
                        _prompt = _playedIndex.ToString();
                        break;
                    }
                    _playedIndex++;
                }
                foreach (var player in Table.PlayerGraphicsDict)
                {
                    if (!(player.Key is LocalPlayer)) continue;
                    var playerCards = player.Value.PlayableCards;
                    AndroidCard[] newPlayableCards = new AndroidCard[playerCards.Length - 1];
                    if (playerCards == null) continue;
                    _playedIndex = 0;
                    foreach (var card in playerCards)
                    {
                        if (!card.Played)
                        {
                            newPlayableCards[_playedIndex] = card;
                            _playedIndex++;
                        }
                        else
                        {
                            card.Played = false;
                            _selectedPlayCard = null;
                        }
                    }
                    player.Value.PlayableCards = newPlayableCards;
                    foreach (var card in playerCards)
                    {
                        card.IsPlayable = false;
                    }
                }
            }
            return _prompt;
        }
    }
}