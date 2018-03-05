using SharedSheep.Game;
using SharedSheep.Player;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SharedSheep.Table
{
    public class Table : ITable
    {
        public List<IPlayer> Players { get; private set; }
        public List<IGame> Games { get; private set; }
        public IPlayer Dealer { get; private set; }
        private Prompt prompt;
        private int GameIndex;

        public Table(IPlayer host, Prompt prompt)
        {
            Players = new List<IPlayer> { host };
            Dealer = host;
            Games = new List<IGame>();
            //Dealer = new IPlayer(); this is going to be the host
            this.prompt = prompt;
            GameIndex = 0;
        }

        public void AddPlayer(IPlayer player)
        {
            Players.Add(player);
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
            prompt(PromptType.GameOver);
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
        }
    }
}