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
    class Piquet : iface.IDeck
    {
        public List<iface.ICard> Cards { get ; set ; }


        public Piquet()
        {

        }

        public void AddCard()
        {
            throw new NotImplementedException();
        }

        public Card GetTopCard()
        {
            throw new NotImplementedException();
        }

        public Card RemoveCardBy(int index)
        {
            throw new NotImplementedException();
        }

        public void Reset()
        {
            throw new NotImplementedException();
        }

        public void Shuffle()
        {
            throw new NotImplementedException();
        }

        public int Size()
        {
            throw new NotImplementedException();
        }
    }
}