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
    interface IBlind
    {
        List<ICard> BlindCards { get; set; }
        void AddCard(ICard card);
        ICard SwapCard(int index, ICard card);
        int BlindPoints();
    }
}