using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

using iface = sheepshead.Resources.models.interfaces;


namespace sheepshead.Resources.models
{
    public class Trick : iface.ITrick
    {

        public List<(iface.IPlayer, iface.ICard)> TrickCards { get; private set ; }



        public Trick(List<(iface.IPlayer, iface.ICard)> cards)
        {
            TrickCards = new List<(iface.IPlayer, iface.ICard)>(5);
            TrickCards = cards;
        }



        public iface.ICard LeadingCard()
        {
            return TrickCards[0].Item2;
        }
        public iface.IPlayer TheWinnerPlayer()
        {
            iface.IPlayer winner = TrickCards[0].Item1;
            for (int i=0; i<TrickCards.Count;i++)
            {
                iface.ICard card = TrickCards[i].Item2;
                if (TheWinnerCard().Equals(card))
                    winner = TrickCards[i].Item1;
            } 
            return winner;
        }




        public iface.ICard TheWinnerCard()
        {
            iface.ICard winner = LeadingCard();
            foreach((iface.IPlayer,iface.ICard) crd in TrickCards)
            {
                iface.ICard card = crd.Item2;
                if (winner.CardSuit == card.CardSuit && winner.Power < card.Power)
                    winner = card;
                else if (winner.CardSuit != Suit.Trump && card.CardSuit == Suit.Trump)
                    winner = card;
            }
            return winner;
        }



    }
}


