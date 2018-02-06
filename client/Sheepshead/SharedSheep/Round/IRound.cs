using System;
using System.Collections.Generic;
using System.Text;
using SharedSheep.Player;
using SharedSheep.Trick;
namespace SharedSheep.Round
{
    interface IRound
    {
        ITrick Trick { get; set; }
        int RoundNumber { get; set; }
        IPlayer RoundStarter { get; set; }

        IPlayer TurnToPlay();
        
    }
}
