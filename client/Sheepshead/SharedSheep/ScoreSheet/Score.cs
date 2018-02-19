using System;
using System.Collections.Generic;
using System.Text;
using SharedSheep.Player;
using System.Linq;

namespace SharedSheep.ScoreSheet
{
    public class Score : IScoreSheet
    {
        public IPlayer WinningPlayer { get; private set; }
        public Dictionary<IPlayer, List<int>> Scores { get; }

        public Score(List<IPlayer> players)
        {
            Scores = new Dictionary<IPlayer, List<int>>();
            players.ForEach(player => Scores.Add(player, new List<int>()));
        }

        public int PlayerScore(IPlayer player)
        {
            return Scores[player].Sum();
        }

        public void AddGameScore(IPlayer picker, IPlayer partner, bool noTricks, bool cracked, int pickerCardsValue)
        {
            if (partner == null)
            {
                int pickerScore = RangeValue(pickerCardsValue, false, cracked, noTricks);
                Scores[picker].Add(pickerScore);
                foreach (KeyValuePair<IPlayer, List<int>> list in Scores)
                {
                    if (list.Key != picker)
                        Scores[list.Key].Add(pickerScore / -4);
                }
            }
            else
            {
                int pickerScore = RangeValue(pickerCardsValue, true, cracked, noTricks);
                Scores[picker].Add(pickerScore);
                Scores[partner].Add(pickerScore / 2);
                foreach (KeyValuePair<IPlayer, List<int>> list in Scores)
                {
                    if (list.Key != picker && list.Key != partner)
                        Scores[list.Key].Add(pickerScore / -2);
                }
            }
            //set the new winning player
            Dictionary<IPlayer, int> total = Total();
            WinningPlayer = total.Aggregate((result, next) => next.Value > result.Value ? next : result).Key;
        }

        public Dictionary<IPlayer, int> Total()
        {
            Dictionary<IPlayer, int> total = new Dictionary<IPlayer, int>();
            foreach (KeyValuePair<IPlayer, List<int>> list in Scores)
            {
                total.Add(list.Key, list.Value.Sum());
            }
            return total;
        }

        private int RangeValue(int values, bool partner, bool cracked, bool noTricks)
        {
            int score = 0;
            if (noTricks) score = -6;
            else if (values <= 30) score = -4;
            else if (values <= 60) score = -2;
            else if (values <= 90) score = 2;
            else if (values < 120) score = 4;
            else score = 6;
            if (!partner) { score = 2 * score; }
            if (cracked) { score = 2 * score; }
            return score;
        }
    }
}