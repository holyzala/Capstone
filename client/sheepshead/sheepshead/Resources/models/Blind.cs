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
    class Blind : iface.IBlind
    {
        public List<iface.ICard> BlindCards { get; set ; }

        public void AddCard(iface.ICard card)
        {
            if (BlindCards.Count < 3) //what if not?
                BlindCards.Add(card);
        }

        public int BlindPoints()
        {
            return BlindCards[0].Value + BlindCards[1].Value;
        }

        public iface.ICard SwapCard(int index, iface.ICard card)
        {
            iface.ICard outCard = BlindCards[index];
            BlindCards[index] = card;

            return outCard;
          
        }
    }
}