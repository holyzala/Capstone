using System;
using System.Collections.Generic;
using SharedSheep.Player;
using SharedSheep.Card;

namespace SharedSheep.Trick
{
    public class Trick : ITrick
    {

        public List<(IPlayer, ICard)> TrickCards { get; private set ; }



        public Trick(List<(IPlayer, ICard)> cards)
        {
            TrickCards = new List<(IPlayer, ICard)>(5);
            TrickCards = cards;
        }



        public ICard LeadingCard()
        {
            return TrickCards[0].Item2;
        }
        public IPlayer TheWinnerPlayer()
        {
            IPlayer winner = TrickCards[0].Item1;
            for (int i=0; i<TrickCards.Count;i++)
            {
                ICard card = TrickCards[i].Item2;
                if (TheWinnerCard().Equals(card))
                    winner = TrickCards[i].Item1;
            } 
            return winner;
        }




        public ICard TheWinnerCard()
        {
            ICard winner = LeadingCard();
            foreach((IPlayer, ICard) crd in TrickCards)
            {
                ICard card = crd.Item2;
                if (winner.CardSuit == card.CardSuit && winner.Power < card.Power)
                    winner = card;
                else if (winner.CardSuit != Suit.Trump && card.CardSuit == Suit.Trump)
                    winner = card;
            }
            return winner;
        }



    }
}


