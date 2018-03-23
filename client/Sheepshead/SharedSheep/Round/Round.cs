using SharedSheep.Blind;
using SharedSheep.Card;
using SharedSheep.Player;
using SharedSheep.Trick;
using System.Collections.Generic;

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

        public IPlayer Start(Prompt prompt, List<IPlayer> players, List<IRound> rounds, IPlayer picker, IBlind blind, ICard partnerCard)
        {
            foreach (IPlayer player in players)
            {
                CurrentPlayer = player;
                Trick.AddCardAndPlayer(player, player.PlayCard(prompt, rounds, picker, blind, partnerCard));
            }
            return Trick.TheWinnerPlayer();
        }
    }
}