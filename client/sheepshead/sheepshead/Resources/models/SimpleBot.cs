using System;
using iface = sheepshead.Resources.models.interfaces;

namespace sheepshead.Resources.models
{
    public class SimpleBot: iface.IPlayer
    {
        public iface.IHand Hand { get; set; }
        public String Name { get; private set; }
        private Boolean Partner;

        public SimpleBot(String name)
        {
            Partner = false;
            Name = name;
        }

        public Boolean IsPartner()
        {
            return Partner;
        }

        public Boolean WantPick()
        {
            return false;
        }

        public iface.ICard PlayCard()
        {
            return Hand.Cards[0];
        }
    }
}
