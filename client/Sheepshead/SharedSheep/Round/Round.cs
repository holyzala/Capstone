using System;
using System.Collections.Generic;
using System.Text;
using SharedSheep.Player;
using SharedSheep.Trick;

namespace SharedSheep.Round
{
    public class Round : IRound
    {
        public ITrick Trick { get; private set; }
        public int RoundNumber { get; private set; }
        public IPlayer RoundStarter { get; private set; }
        public IPlayer CurrentPlayer { get; private set; }

        public Round(int roundNum, IPlayer roundStarter)
        {
            RoundNumber = roundNum;
            RoundStarter = roundStarter;
            Trick = new Trick.Trick();
        }

        public IPlayer Start(Prompt prompt, List<IPlayer> players)
        {
            foreach (IPlayer player in players)
            {
                CurrentPlayer = player;
                Trick.AddCardAndPlayer(player, player.PlayCard(prompt, Trick.LeadingCard()));
            }
            return Trick.TheWinnerPlayer();
        }
    }
}