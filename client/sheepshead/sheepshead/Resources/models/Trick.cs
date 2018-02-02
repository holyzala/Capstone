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
    class Trick
    {
        List<(iface.IPlayer,iface.ICard)> TrickCards { get; set; }
        iface.IPlayer TrickWinner { get; set; }


        public (iface.IPlayer, iface.ICard) CurrentWinningCard()
        {
            (iface.IPlayer, iface.ICard) winner = (null,null);
         
            if(TrickCards.Count != 0)
            {
                for(int i =0; i< TrickCards.Count; i++)
                {
                    if (TrickCards[i].Item2.IsHigher(winner.Item2))
                        winner = TrickCards[i];
                }
            }
            return winner;
        }

    }
}