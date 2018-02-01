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
        
        void Shuffle();
        int Size();
        void Reset();
        Card RemoveCardBy(int index);
        void AddCard();
        Card GetTopCard();

    }
}