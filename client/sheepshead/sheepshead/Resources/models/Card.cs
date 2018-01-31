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

namespace sheepshead.Resources.models
{
    class Card : ICard
    {
        public Card(){}
        public Card(int num, int value, int power, Suit cardSuit)
        {
            this.Num = num;
            this.Value = value;
            this.Power = power;
            this.CardSuit = CardSuit;
        }

        public int Num { get ; set ; }
        public int Value { get ; set ; }
        public int Power { get ; set ; }
        public Suit CardSuit { get ; set ; }

        public bool isHigher(ICard otherCard)
        {
            return this.Power > otherCard.Power;
        }

        public bool isTrump()
        {
            return this.CardSuit == Suit.Trump;
        }
    }
}