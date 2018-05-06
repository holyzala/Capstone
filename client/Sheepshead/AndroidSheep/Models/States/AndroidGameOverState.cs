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
using AndroidSheep.Models.Buttons.Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SharedSheep.Game;
using SharedSheep.ScoreSheet;
using SharedSheep.Table;

namespace AndroidSheep.Models.States
{
    class AndroidGameOverState : AndroidState
    {
        private readonly List<AndroidGameOverScores> _leaderboardpanel;
        private readonly List<AndroidButton> _components;
        private bool _nextGame;
        private IGame _game;
        private IScoreSheet _scoresheet;
        private ITable _table;
        private AndroidGameOverScores _pickerScore;
        public AndroidGameOverState(AndroidSheepGame table, GraphicsDevice graphicsDevice, GameContent gameContent) : base(table, graphicsDevice, gameContent)
        {
            _nextGame = false;
            AndroidButton doneButton = new AndroidButton(gameContent.Button, gameContent.Font)
            {
                Position = new Vector2(Table.ScreenWidth / 2 - 130, Table.ScreenHeight / 2 - 300),
                Text = "Next Game",
                Color = Color.White
            };

            doneButton.Click += DoneButton_Click;

            _pickerScore = new AndroidGameOverScores(gameContent.Button3, gameContent.Font)
            {
                Position = new Vector2(Table.ScreenWidth / 2.0f - 115, Table.ScreenWidth / 5.0f - 20)
            };

            _components = new List<AndroidButton>()
            {
                doneButton,
            };
            var offset = 50;
            var halfscreen = table.ScreenHeight / 2;

            AndroidGameOverScores playerOneScore = new AndroidGameOverScores(gameContent.Button3, gameContent.Font)
            {
                Position = new Vector2(offset, halfscreen)
            };
            offset += gameContent.Button2.Width - 50;
            AndroidGameOverScores playerTwoScore = new AndroidGameOverScores(gameContent.Button3, gameContent.Font)
            {
                Position = new Vector2(offset, halfscreen)
            };
            offset += gameContent.Button2.Width - 50;
            AndroidGameOverScores playerThreeScore = new AndroidGameOverScores(gameContent.Button3, gameContent.Font)
            {
                Position = new Vector2(offset, halfscreen)
            };
            offset += gameContent.Button2.Width - 50;
            AndroidGameOverScores playerFourScore = new AndroidGameOverScores(gameContent.Button3, gameContent.Font)
            {
                Position = new Vector2(offset, halfscreen)
            };
            offset += gameContent.Button2.Width - 50;
            AndroidGameOverScores playerFiveScore = new AndroidGameOverScores(gameContent.Button3, gameContent.Font)
            {
                Position = new Vector2(offset, halfscreen)
            };
            _leaderboardpanel = new List<AndroidGameOverScores>()
            {
                playerOneScore,
                playerTwoScore,
                playerThreeScore,
                playerFourScore,
                playerFiveScore
            };
        }

        private void DoneButton_Click(object sender, EventArgs e)
        {
            _nextGame = true;
        }

        public void SetGame(IGame game)
        {
            _game = game;
            _pickerScore.PlayerName = "Picker: " + game.Picker.Name;
            _pickerScore.Score = "Points: " + game.GetPickerScore().ToString();
            foreach (var component in _components)
            {
                component.Color = Color.White;
            }
        }

        public void SetSharedSheepTable(ITable table)
        {
            _table = table;
        }
        public void SetScoreSheet(IScoreSheet scoresheet)
        {
            _scoresheet = scoresheet;
            var index = 0;
            foreach (var pair in Table.PlayerGraphicsDict)
            {
                var player = pair.Key;
                _leaderboardpanel[index].PlayerName = player.Name;
                var score = scoresheet.Scores[player][_table.Games.Count - 1];
                _leaderboardpanel[index++].Score = "Current Score: " + score.ToString();
            }
        }

        public string PickingPrompt()
        {
            var prompt = "";
            if (_nextGame)
            {
                prompt = "done";
            }

            _nextGame = false;
            return prompt;
        }
        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin(SpriteSortMode.Immediate);
            if (_leaderboardpanel != null)
            {
                foreach (var component in _leaderboardpanel)
                {
                    component?.Draw(gameTime, spriteBatch);
                }
            }

            if (_components != null)
            {
                foreach (var component in _components)
                {
                    component?.Draw(gameTime, spriteBatch);
                }

                _pickerScore?.Draw(gameTime, spriteBatch);
            }

            spriteBatch.End();
        }

        public override void Update(GameTime gameTime)
        {
            _pickerScore?.Update(gameTime);
            foreach (var component in _components)
            {
                component?.Update(gameTime);
            }
        }
    }
}