using System;
using System.Collections.Generic;
using System.Text;
using SharedSheep.Player;

namespace SharedSheep.ScoreSheet
{
    public interface IScoreSheet
    {
        IPlayer WinningPlayer { get; }
        Dictionary<IPlayer, List<int>> Scores { get; }

        int PlayerScore(IPlayer player);
        void AddGameScore(IPlayer picker, IPlayer partner,bool noTricks, bool cracked, int pickerCardsValue);
        Dictionary<IPlayer, int> Total();


    }
}
