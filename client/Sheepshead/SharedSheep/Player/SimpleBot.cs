using System;
using SharedSheep.Card;
using SharedSheep.Hand;
using SharedSheep.Player;

namespace sheepshead.Resources.models
{
    public class SimpleBot: IPlayer
    {
        public IHand Hand { get; set; }
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

        public ICard PlayCard()
        {
            return Hand.Cards[0];
        }
    }
}
