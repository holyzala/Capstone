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
    class Hand: iface.IHand
    {
        public List<iface.ICard> Cards { get; set; }

        public Hand() { }
        
        public int GetNumOfRemainingCards()
        {
            int size = 0;
            for (int i=0; i < Cards.Count; i++)
            {
                if(Cards[i] != null)  size++;
            }
            return size;
        }

        public iface.ICard GetCard(int index)
        {
            if (index >= 6 || index < 0)
                throw new IndexOutOfRangeException();
            else if (Cards[index] == null)
                throw new NullReferenceException("Card doesn't exist");

            else
            return Cards[index];
        }


        public int NumOfThisSuit(Suit suit)
        {
            int numOfCards = 0;
            for(int i=0; i< Cards.Count; i++)
            {
                if (this.Cards[i].CardSuit == suit)
                    numOfCards++;
            }
            return numOfCards;
        }

    }
}