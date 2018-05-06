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
using SharedSheep.ScoreSheet;
using SharedSheep.Table;

namespace AndroidSheep.Models.States
{
    class AndroidTableOverState : AndroidState
    {
        private readonly List<AndroidGameOverScores> _leaderboardpanel;
        private readonly List<AndroidButton> _components;
        private ITable _table;
        private IScoreSheet _scoresheet;
        private string _prompt;
        private AndroidFinalScoreBanner _finalScoreBanner;
        private bool _done;
        private bool _playAgain;
        private int maxScore = 0;
        private bool _scoreIsSet;
    
        public AndroidTableOverState(AndroidSheepGame table, GraphicsDevice graphicsDevice, GameContent gameContent) : base(table, graphicsDevice, gameContent)
        {
            var offset = 50;
            var halfscreen = table.ScreenHeight / 2;

            AndroidButton playAgain = new AndroidButton(gameContent.Button, gameContent.Font)
            {
                Position = new Vector2(240, table.ScreenHeight / 2 - 100),
                Text = "Play Again"
            };
            AndroidButton doneButton = new AndroidButton(gameContent.Button, gameContent.Font)
            {
                Position = new Vector2(740, table.ScreenHeight / 2 - 100),
                Text = "Quit"
            };

            doneButton.Click += DoneButton_Click;
            playAgain.Click += PlayAgain_Click;
            _components = new List<AndroidButton>
            {
                playAgain,
                doneButton
            };

            _finalScoreBanner = new AndroidFinalScoreBanner(gameContent.Button4, gameContent.FinalScoreFont)
            {
                Position = new Vector2(table.ScreenWidth / 4.0f - 100, table.ScreenHeight / 4.0f - 50)
            };
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
            _done = true;
        }
        private void PlayAgain_Click(object sender, EventArgs e)
        {
            _playAgain = true;
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin(SpriteSortMode.Immediate);
            if (_leaderboardpanel != null) { 
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
            }
            _finalScoreBanner?.Draw(gameTime, spriteBatch);
            spriteBatch.End();
        }


        public override void Update(GameTime gameTime)
        {
            if (_components == null) return;
            foreach (var component in _components)
            {
                component?.Update(gameTime);
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
            if (!_scoreIsSet)
            {
                foreach (var pair in Table.PlayerGraphicsDict)
                {
                    var player = pair.Key;
                    _leaderboardpanel[index].PlayerName = player.Name;
                    var score = _table.ScrSheet.Total()[player];
                    _leaderboardpanel[index++].Score = "Final Score: " + score.ToString();
                    if (score > maxScore)
                    {
                        _finalScoreBanner.PlayerName = "Winner is " + player.Name;
                        _finalScoreBanner.Score = "Score: " + score.ToString();
                        maxScore = score;
                    }
                    else if (score == maxScore)
                    {
                        _finalScoreBanner.PlayerName += ", " + player.Name;
                    }
                }

                _scoreIsSet = true;
            }
        }

        public string PickingPrompt()
        {
            _prompt = "";
            if (_done)
            {
                _prompt += "done";
            }
            else if (_playAgain)
            {
                _prompt += "playAgain";
            }
            return _prompt; 
        }
    }
}