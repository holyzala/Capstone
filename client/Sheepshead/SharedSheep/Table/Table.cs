﻿using SharedSheep.Game;
using SharedSheep.Player;
using SharedSheep.ScoreSheet;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SharedSheep.Table
{
    public class Table : ITable
    {
        public List<IPlayer> Players { get; }
        public List<IGame> Games { get; }
        public IPlayer Dealer { get; }
        public IScoreSheet ScrSheet { get; }

        private Prompt prompt;
        private int GameIndex;

        public Table(IPlayer host, Prompt prompt)
        {
            Players = new List<IPlayer> { host };
            Dealer = host;
            Games = new List<IGame>();
            ScrSheet = new Score(Players);
            this.prompt = prompt;
            GameIndex = 0;
        }

        public void AddPlayer(IPlayer player)
        {
            Players.Add(player);
            ScrSheet.Scores.Add(player, new List<int>());
        }

        public IPlayer GetCurrentPlayer()
        {
            return Games.Last().GetCurrentPlayer();
        }

        public IPlayer GetNextPlayer(int CurrentPlayerIndexNumber)
        {
            return Players[(CurrentPlayerIndexNumber + 1) % 5];
        }

        public void ResetGame()
        {
            throw new NotImplementedException();
        }

        public void StartNewGame()
        {
            Game.Game game = new Game.Game();
            Games.Add(game);
            // This is somewhat complicated code to move the dealer in each game. Probably refactor later to use the dealer property.
            game.StartGame(Players.Skip(GameIndex).Concat(Players.Take(GameIndex)).ToList(), prompt);
            ScrSheet.AddGameScore(game.Picker, game.Partner, game.GetPickerTrickCount(), game.IsCracked, game.GetPickerScore());
            while (true)
            {
                var answer = prompt(PromptType.GameOver, new Dictionary<PromptData, object>
                {
                    {PromptData.Game, game}
                });
                if (answer != "") break;
            }
        }

        public void Start()
        {
            if (Players.Count != 5)
                throw new InvalidOperationException();

            while (GameIndex < 6)
            {
                StartNewGame();
                ++GameIndex;
            }

            while (true)
            {
                var answer = prompt(PromptType.TableOver, new Dictionary<PromptData, object>
                {
                    {PromptData.Scores, ScrSheet}
                });
                if (answer == "playAgain") break;
            }
        }
    }
}