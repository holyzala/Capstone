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
    public interface ICard
    {
        CardID ID { get; }
        int Value { get; } //score value
        CardPower Power { get; }
        Suit CardSuit { get; }

        Boolean IsTrump();
        Boolean IsHigher(ICard otherCard); //compare powers
    }
}
