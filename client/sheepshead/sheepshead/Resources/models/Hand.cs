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
    class Hand
    {
        private Card[] cards = new Card[6];

        public Hand() { }



        public int getNumOfRemainingCards()
        {
            int size = 0;
            for (int i=0; i < cards.Length; i++)
            {
                if(cards[i] != null)  size++;
            }
            return size;
        }



        public Card getCard(int index)
        {
            return cards[index];
        }


    }
}