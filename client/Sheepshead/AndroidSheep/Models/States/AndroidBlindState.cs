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
        private readonly List<AndroidButton> _components;
        public string Prompt = "";

        public AndroidBlindState(AndroidSheepGame table, GraphicsDevice graphicsDevice, GameContent gameContent) : base(table, graphicsDevice, gameContent)
        {
            AndroidButton pickwords = new AndroidButton(gameContent.PickWords, gameContent.Font)
            {
                Position = new Vector2(100, 100)
            };
            AndroidButton yesButton = new AndroidButton(gameContent.Button, gameContent.Font)
            {
                Position = new Vector2(Table.ScreenWidth / 2 - 400, Table.ScreenHeight / 2 - 100),
                Text = "Yes"
            };
            AndroidButton noButton = new AndroidButton(gameContent.Button, gameContent.Font)
            {
                Position = new Vector2(Table.ScreenWidth / 2 + 150, Table.ScreenHeight / 2 - 100),
                Text = "No"
            };

            yesButton.Click += yesButton_Click;
            noButton.Click += noButton_Click;
            _components = new List<AndroidButton>()
            {
                yesButton,
                noButton,
                pickwords
            };
            
        }

        private void noButton_Click(object sender, EventArgs e)
        {
            Prompt += "no";
        }

        private void yesButton_Click(object sender, EventArgs e)
        {
            Prompt += "yes";
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
            foreach (var component in _components)
            {
                component.Color = Color.White;
                component.Draw(gameTime, spriteBatch);
            }
            spriteBatch.End();
        }

        public override void Update(GameTime gameTime)
        {

            foreach (var component in _components)
            {
                component.Update(gameTime);
            }
            foreach (var player in Table.PlayerGraphicsDict)
            {
                if (player.Key is LocalPlayer)
                {
                    var playerCards = player.Value.PlayableCards;
                    if (playerCards != null)
                    {
                        foreach (var card in playerCards)
                        {
                            card.State = StateType.Picking;
                            card.Update(gameTime);
                        }
                    }
                }
            }
        }

        public string PickingPrompt()
        {
            System.Threading.Thread.Sleep(1000);
            return Prompt;
        }
    }
}