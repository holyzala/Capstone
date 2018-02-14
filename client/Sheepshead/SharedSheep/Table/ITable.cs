using System;
using System.Collections.Generic;
using System.Text;
using SharedSheep.Player;
using SharedSheep.Game;

namespace SharedSheep.Table
{
    public interface ITable
    {
        List<IPlayer> Players { get; }
        List<IGame> Games { get; }

        //ScoreSheet ScrSheet {get; }
        IPlayer Dealer { get; }

        IPlayer GetNextPlayer(int CurrentPlayerIndexNumber);

        void ResetGame();

        void AddPlayer(IPlayer player);

        void StartNewGame();

        void Start();
    }
}