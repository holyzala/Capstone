using SharedSheep.Game;
using SharedSheep.Player;
using System;
using System.Collections.Generic;

namespace SharedSheep.Table
{
    public class Table : ITable
    {
        public delegate string Prompt(string msg);

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
            game.StartGame(Players);
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