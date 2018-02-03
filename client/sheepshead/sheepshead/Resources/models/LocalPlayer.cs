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
    public class LocalPlayer : iface.IPlayer
    {
        public iface.IHand Hand { get; set; }
        public string Name { get; private set; }
        private Boolean Partner;


        public LocalPlayer(string name)
        {
            Name = name;
        }

        public bool IsPartner()
        {
            return false;   
        }

        public iface.ICard PlayCard()
        {
            return this.Hand.Cards[0];
        }

        public bool WantPick()
        {
            return false;
        }
    }
}