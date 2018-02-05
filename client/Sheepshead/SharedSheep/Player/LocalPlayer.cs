using System;
using SharedSheep.Hand;
using SharedSheep.Card;

namespace SharedSheep.Player
{
    public class LocalPlayer : IPlayer
    {
        public IHand Hand { get; set; }
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

        public ICard PlayCard()
        {
            return this.Hand.Cards[0];
        }

        public bool WantPick()
        {
            return false;
        }
    }
}
