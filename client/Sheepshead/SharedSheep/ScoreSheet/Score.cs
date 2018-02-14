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
            return Total()[player];
        }



        public void AddGameScore(IPlayer picker, IPlayer partner, bool noTricks, bool cracked, int pickerCardsValue)
        {
            if (partner == null)
            {
                int pickerScore = RangeValue(pickerCardsValue, false, cracked, noTricks);
                Scores[picker].Add(pickerScore);
                foreach (KeyValuePair<IPlayer, List<int>> list in Scores)
                {
                    if (list.Key.Name != picker.Name)
                        Scores[list.Key].Add((pickerScore * -1) / 4);
                }
            }
            else
            {
                int pickerScore = RangeValue(pickerCardsValue, true, cracked, noTricks);
                Scores[picker].Add(pickerScore);
                Scores[partner].Add(pickerScore / 2);
                foreach (KeyValuePair<IPlayer, List<int>> list in Scores)
                {
                    if (list.Key.Name != picker.Name && list.Key.Name != partner.Name)
                        Scores[list.Key].Add((pickerScore * -1) / 2);
                }
            }
            //set the new winning player
            int max = 0;
            foreach (KeyValuePair<IPlayer, int> list in Total())
            {
                if (list.Value > max)
                {
                    WinningPlayer = list.Key;
                    max = list.Value;
                }
            }
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
            switch (partner)
            {
                case true:
                    if (noTricks) score = -6;
                    else if (values <= 30) score = -4;
                    else if (values <= 60) score = -2;
                    else if (values <= 90) score = 2;
                    else if (values < 120) score = 4;
                    else return 6;
                    break;
                case false:
                    if (noTricks) score = -12;
                    else if (values <= 30) score = -8;
                    else if (values <= 60) score = -4;
                    else if (values <= 90) score = 4;
                    else if (values < 120) score = 8;
                    else score = 12;
                    break;
            }
            if (cracked)
                score = 2 * score;
            return score;
        }
    }
}
