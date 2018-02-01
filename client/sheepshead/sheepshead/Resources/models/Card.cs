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
    class Card : iface.ICard
    {
        public CardID ID { get; private set; }
        public int Value { get; private set; }
        public CardPower Power { get; private set; }
        public Suit CardSuit { get; private set; }

        public Card(CardID num, int value, CardPower power, Suit cardSuit)
        {
            this.ID = num;
            this.Value = value;
            this.Power = power;
            this.CardSuit = CardSuit;
        }

        public bool IsHigher(iface.ICard otherCard)
        {
            return this.Power > otherCard.Power;
        }

        public bool IsTrump()
        {
            return this.CardSuit == Suit.Trump;
        }
    }
}