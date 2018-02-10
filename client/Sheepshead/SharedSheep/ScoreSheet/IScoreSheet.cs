using System;
using System.Collections.Generic;
using System.Text;
using SharedSheep.Player;

namespace SharedSheep.ScoreSheet
{
    public interface IScoreSheet
    {
        IPlayer WinningPlayer { get; }
        List<(IPlayer, int)> Scores { get; }

        int PlayerScore(IPlayer player);
        void AddScore((IPlayer, int) score);
    }
}
