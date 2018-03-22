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
using AndroidSheep.Models.Buttons;

namespace AndroidSheep.Models.Player
{
    public abstract class Player
    {
        public Card[] playableCards;
        public Card[] playedCards;
        public Prompt playerPrompt;

    }
}