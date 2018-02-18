using System;
using SharedSheep.Hand;
using SharedSheep.Card;
using SharedSheep.Blind;

namespace SharedSheep.Player
{
    public class LocalPlayer : IPlayer
    {
        public IHand Hand { get; set; }
        public string Name { get; private set; }
        private Boolean Partner;

        public LocalPlayer(string name)
        {
            Hand = new Hand.Hand();
            Partner = false;
            Name = name;
        }

        public bool IsPartner()
        {
            return Partner;
        }

        public ICard PlayCard()
        {
            return this.Hand.Cards[0];
        }

        public bool WantPick(Prompt prompt)
        {
            string answer = prompt("Do you want to pick? (yes/no)");
            if (answer.ToLower() == "yes" || answer.ToLower() == "y")
            {
                return true;
            }
            return false;
        }

        public IBlind Pick(IBlind blind)
        {
            return blind;
        }

        public void AddToHand(ICard card)
        {
            Hand.AddCard(card);
        }
    }
}