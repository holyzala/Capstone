using System;
using System.Collections.Generic;
using System.Text;
using SharedSheep.Game;
using SharedSheep.Player;

namespace SharedSheep.Table
{
    public class Table : ITable
    {
        public List<IPlayer> Players { get; private set; }
        public List<IGame> Games { get; private set; }
        public IPlayer Dealer { get; private set; }

        public delegate string Prompt(string msg);


        public Table(Prompt prompt)
        {
            Players = new List<IPlayer>();
            Games = new List<IGame>();
            //Dealer = new IPlayer(); this is going to be the host
         }

        public void AddPlayer(IPlayer player)
        {
            Players.Add(player);
        }

        public IPlayer GetNextPlayer(int CurrentPlayerIndexNumber)
        {
            return Players[(CurrentPlayerIndexNumber + 1)%5];
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
    }
}
