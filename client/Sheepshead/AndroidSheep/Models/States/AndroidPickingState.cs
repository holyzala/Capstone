using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AndroidSheep.Models.Buttons;
using AndroidSheep.Models.Player;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SharedSheep.Blind;
using SharedSheep.Hand;
using SharedSheep.Player;

namespace AndroidSheep.Models.States
{
    class AndroidPickingState : AndroidState
    {
        private readonly List<AndroidButton> _components;
        private bool _wantsToSwap = false;
        private bool _isDone = false;
        private AndroidCard _blindCard;
        private AndroidCard _handCard;
        private int _numCardsSelected;
        private AndroidPlayer _graphicsPlayer;

        public string Prompt = "";
        public IPlayer Player { get; set; }
        public IBlind Blind { get; set; }

        public AndroidPickingState(AndroidSheepGame table, GraphicsDevice graphicsDevice, GameContent gameContent) : base(table, graphicsDevice, gameContent)
        {
            AndroidButton doneButton = new AndroidButton(gameContent.Button, gameContent.Font)
            {
                Position = new Vector2(250, 50),
                Text = "Done?"
            };
            AndroidButton swapButton = new AndroidButton(gameContent.Button, gameContent.Font)
            {
                Position = new Vector2(750, 50),
                Text = "Swap?"
            };

            swapButton.Click += SwapButton_Click;
            doneButton.Click += DoneButton_Click;
            _components = new List<AndroidButton>()
            {
               swapButton,
               doneButton
            };
            _numCardsSelected = 0;
        }
        public void AssignPlayer(IPlayer player)
        {
            this.Player = player;
            Table.PlayerGraphicsDict.TryGetValue(player, out _graphicsPlayer);
        }
        public void AssignBlind(IBlind blind)
        {
            this.Blind = blind;
        }
        private void DoneButton_Click(object sender, EventArgs e)
        {
            _isDone = true;
        }

        private void SwapButton_Click(object sender, EventArgs e)
        {
            _wantsToSwap = true;
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin(SpriteSortMode.Immediate);
            foreach (var card in _graphicsPlayer.PlayableCards)
            {
                card.Draw(gameTime, spriteBatch);
            }
            foreach (var card in Table.BlindList)
            {
                card.Draw(gameTime, spriteBatch);
            }
            foreach (var component in _components)
            {
                component.Draw(gameTime, spriteBatch);
            }
            spriteBatch.End();                        
        }

        public override void Update(GameTime gameTime)
        {

            foreach(var component in _components)
            {
                component.Update(gameTime);
            }
            foreach (var card in Table.BlindList)
            {
                card.State = StateType.Picking;
                if (_numCardsSelected > 2)
                {
                    card.IsSelected = false;
                    _numCardsSelected--;
                }
                if (card.IsSelected)
                {
                    _blindCard = card;
                    _numCardsSelected++;
                }
                card.Update(gameTime);

            }
            foreach (var player in Table.PlayerGraphicsDict)
            {
                if (!(player.Key is LocalPlayer)) continue;
                var playerCards = player.Value.PlayableCards;
                if (playerCards == null) continue;
                foreach (var card in playerCards)
                {
                    card.State = StateType.Picking;
                    if (_numCardsSelected > 2)
                    {
                        card.IsSelected = false;
                        _numCardsSelected--;
                    }

                    if (card.IsSelected)
                    {
                        _handCard = card;
                        _numCardsSelected++;

                    }
                    card.Update(gameTime);             
                }
            }
        }
        public string PickingPrompt()
        {
            Prompt = "";
            if (_isDone)
            {
                Prompt += "done";
            }
            else if (_numCardsSelected == 2 && _wantsToSwap)
            {
                int cardOne = 0;
                int cardTwo = 0;
                IHand playerHand = Player.Hand;
                AndroidCard newBlind = null;
                AndroidCard newHand = null;
                foreach (var card in Blind)
                {
                    if (_blindCard.Card.Equals(card))
                    {
                        _blindCard.ChangeSelectionPicking();
                        newHand = new AndroidCard(_blindCard.Texture, _blindCard.Card)
                        {
                            Position = new Vector2(_handCard.Position.X, _handCard.Position.Y + 40),
                            State = _handCard.State,
                            IsBlind = true
                        };
                        break;
                    }
                    cardOne++;
                }
                foreach (var card in playerHand)
                {
                    if (_handCard.Card.Equals(card))
                    {
                        _handCard.ChangeSelectionPicking();
                        newBlind = new AndroidCard(_handCard.Texture, _handCard.Card)
                        {
                            Position = new Vector2(_blindCard.Position.X, _blindCard.Position.Y),
                            State = _blindCard.State,
                            IsBlind = false
                        };
                        break;
                    }
                    cardTwo++;
                }
                
                Prompt = $"{cardOne} {cardTwo}";
                foreach(var component in _components)
                {
                    component.Color = Color.White;
                }
                Table.BlindList[cardOne] = newBlind;
                _graphicsPlayer.PlayableCards[cardTwo] = newHand;
                _numCardsSelected = 0;
                _handCard = null;
                _blindCard = null;
                _wantsToSwap = false;
            }
            return Prompt;
        }
    }
}