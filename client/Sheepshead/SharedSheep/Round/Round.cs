using System;
using System.Collections.Generic;
using System.Text;
using SharedSheep.Player;
using SharedSheep.Trick;

namespace SharedSheep.Round
{
    class Round : IRound
    {
        public ITrick Trick { get; set; }
        public int RoundNumber { get; set; }
        public IPlayer RoundStarter { get; set; }

        public Round() { }

        public Round(ITrick trick, int roundNum, IPlayer roundStarter)
        {
            Trick = trick;
            RoundNumber = roundNum;
            RoundStarter = roundStarter;
        }

        public IPlayer TurnToPlay()
        {
            throw new NotImplementedException();
        }
    }
}
