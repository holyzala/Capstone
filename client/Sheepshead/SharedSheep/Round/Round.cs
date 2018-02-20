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

        public Round(int roundNum, IPlayer roundStarter)
        {
            RoundNumber = roundNum;
            RoundStarter = roundStarter;
            Trick = new Trick.Trick();
        }

        public IPlayer TurnToPlay()
        {
            throw new NotImplementedException();
        }

        public IPlayer Start(List<IPlayer> players)
        {
            foreach (IPlayer player in players)
            {
                Trick.AddCardAndPlayer(player, player.PlayCard());
            }
            return Trick.TheWinnerPlayer();
        }
    }
}