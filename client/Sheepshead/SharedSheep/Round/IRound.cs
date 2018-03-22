using SharedSheep.Blind;
using SharedSheep.Player;
using SharedSheep.Trick;
using System.Collections.Generic;

namespace SharedSheep.Round
{
    public interface IRound
    {
        ITrick Trick { get; }
        int RoundNumber { get; }
        IPlayer RoundStarter { get; }
        IPlayer CurrentPlayer { get; }

        IPlayer Start(Prompt prompt, List<IPlayer> players, List<IRound> rounds, IPlayer picker, IBlind blind);
    }
}