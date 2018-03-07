using System;
using System.Collections.Generic;
using System.Text;
using SharedSheep.Player;
using SharedSheep.Game;
using SharedSheep.ScoreSheet;

namespace SharedSheep.Table
{
    public interface ITable
    {
        List<IPlayer> Players { get; }
        List<IGame> Games { get; }

        IScoreSheet ScrSheet {get; }
        IPlayer Dealer { get; }

        IPlayer GetNextPlayer(int CurrentPlayerIndexNumber);

        void ResetGame();

        void AddPlayer(IPlayer player);

        void StartNewGame();

        void Start();

        IPlayer GetCurrentPlayer();
    }
}