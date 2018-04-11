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
        private List<AndroidComponent> _components;
        private bool wantsToSwap = false;
        private bool isDone = false;
        private AndroidCard blindCard;
        private AndroidCard handCard;
        //ADD Prompts
        //1. DisplayBlock
        //2. SwapBUtton
        public string prompt = "";
        private int numCardsSelected;
        public IPlayer player { get; set; }
        public IBlind blind { get; set; }
        private AndroidPlayer graphicsPlayer;
        public AndroidPickingState(AndroidSheepGame table, GraphicsDevice graphicsDevice, GameContent gameContent) : base(table, graphicsDevice, gameContent)
        {
            AndroidButton DoneButton = new AndroidButton(gameContent.Button, gameContent.Font)
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
            DoneButton.Click += DoneButton_Click;
            _components = new List<AndroidComponent>()
            {
               swapButton,
               DoneButton
            };
            numCardsSelected = 0;
        }
        public void assignPlayer(IPlayer player)
        {
            this.player = player;
            _table._playerGraphicsDict.TryGetValue(player, out graphicsPlayer);
        }
        public void assignBlind(IBlind blind)
        {
            this.blind = blind;
        }
        private void DoneButton_Click(object sender, EventArgs e)
        {
            isDone = true;
        }

        private void SwapButton_Click(object sender, EventArgs e)
        {
            wantsToSwap = true;
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            foreach (var card in graphicsPlayer.playableCards)
            {
                card.Draw(gameTime, spriteBatch);
            }
            foreach (var card in _table._blindList)
            {
                card.Draw(gameTime, spriteBatch);
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

            foreach (var card in _table._blindList)
            {
                card.State = StateType.Picking;
                if (numCardsSelected > 2)
                {
                    card.IsSelected = false;
                    numCardsSelected--;
                }
                if (card.IsSelected)
                {
                    blindCard = card;
                    numCardsSelected++;
                }
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
                            card.State = StateType.Picking;
                            if (numCardsSelected > 2)
                            {
                                card.IsSelected = false;
                                numCardsSelected--;
                            }

                            if (card.IsSelected)
                            {
                                handCard = card;
                                numCardsSelected++;

                            }
                            card.Update(gameTime);             
                        }
                    }
                }
            }
        }
        public string PickingPrompt()
        {
            if (isDone)
            {
                prompt += "done";
            }
            else if (numCardsSelected == 2 && wantsToSwap)
            {
                IHand playerHand = player.Hand;
                int index = 0;
                foreach (var card in blind)
                {
                    if (blindCard._card.Equals(card))
                        prompt += string.Format("{0}) {1}\n", index++, card);
                }
                index = 0;
                foreach (var card in playerHand)
                {
                    if (handCard._card.Equals(card))
                        prompt += string.Format("{0}) {1}\n", index++, card);
                }
                wantsToSwap = false;
            }
            return prompt;
        }
    }
}