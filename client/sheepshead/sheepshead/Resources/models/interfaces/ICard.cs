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
    interface ICard
    {

        int Num { get; set; } //the type of the card 7,8,9,10,11,12,13 (13 is the ace)
        int Value { get; set; } //score value
        int Power { get; set; } //queen of clubs is 14. And it goes down to 2 for 7 of diamonds. Everything elase is 1
        Suit CardSuit { get; set; } 

        Boolean isTrump();
        Boolean isHigher(ICard otherCard); //compare powers
         
    }
}