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

namespace sheepshead.Resources.models.interfaces
{
    interface IDeck
    {
        List<ICard> Cards { get; set; }
        
        void Shuffle(List<ICard> cards);
        int Size();
        void ResetDeck();
        bool RemoveCardByIndex(int index);
        ICard GetTopCard();

    }
}