using System;
using System.Collections.Generic;
using System.Text;
using SharedSheep.Player;

namespace SharedSheep.ScoreSheet
{
    public class Score : IScoreSheet
    {
        public IPlayer WinningPlayer { get; private set; }
        public List<(IPlayer, int)> Scores { get; }


        public void AddScore((IPlayer, int) score)
        {
            Scores.Add(score);

            foreach ((IPlayer, int) scr in Scores)
            {

            }
        }

        public int PlayerScore(IPlayer player)
        {
            throw new NotImplementedException();
        }


    }
}
