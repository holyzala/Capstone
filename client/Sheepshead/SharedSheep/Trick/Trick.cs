using System;
using System.Collections.Generic;
using SharedSheep.Player;
using SharedSheep.Card;

namespace SharedSheep.Trick
{
    public class Trick : ITrick
    {

        public List<(IPlayer, ICard)> TrickCards { get; private set; }



        public Trick()
        {
            TrickCards = new List<(IPlayer, ICard)>(5);
        }


        public void AddCardAndPlayer(IPlayer player, ICard card)
        {
            if (TrickCards.Count < 5)
                TrickCards.Add((player, card));
        }

        public ICard LeadingCard()
        {
            return TrickCards[0].Item2;
        }


        public IPlayer TheWinnerPlayer()
        {
            return Winners().Item1;
        }

        public ICard TheWinnerCard()
        {
            return Winners().Item2;
        }


        private (IPlayer, ICard) Winners()
        {
            (IPlayer, ICard) winner = TrickCards[0];
            foreach ((IPlayer, ICard) crd in TrickCards)
            {
                ICard card = crd.Item2;
                if (winner.Item2.CardSuit == card.CardSuit && winner.Item2.Power < card.Power)
                    winner = crd;
                else if (winner.Item2.CardSuit != Suit.Trump && card.CardSuit == Suit.Trump)
                    winner = crd;
            }
            return winner;
        }

        public int TrickValue()
        {
            int total = 0;
            foreach ((IPlayer, ICard) crd in TrickCards)
            {
                total = total + crd.Item2.Value;
            }
            return total;
        }
    }
}


