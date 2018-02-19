using System;
using System.Collections.Generic;
using System.Text;
using SharedSheep.Player;
using SharedSheep.Trick;

namespace SharedSheep.Round
{
    public interface IRound
    {
        ITrick Trick { get; }
        int RoundNumber { get; }
        IPlayer RoundStarter { get; }

        IPlayer TurnToPlay();

        IPlayer Start(List<IPlayer> players);
    }
}