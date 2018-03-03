using System;
using SharedSheep.Card;
using SharedSheep.Hand;
using SharedSheep.Blind;
using System.Collections.Generic;

namespace SharedSheep.Player
{
    public class SimpleBot : IPlayer
    {
        public IHand Hand { get; set; }
        public String Name { get; private set; }
        private Boolean Partner;

        public SimpleBot(String name)
        {
            Partner = false;
            Name = name;
            Hand = new Hand.Hand();
        }

        public Boolean IsPartner()
        {
            return Partner;
        }

        public Boolean WantPick(Prompt prompt)
        {
            return false;
        }

        public ICard PlayCard(Prompt prompt, ICard lead)
        {
            List<ICard> cards = Hand.GetPlayableCards(lead);
            Hand.RemoveCard(cards[0]);
            return cards[0];
        }

        public IBlind Pick(Prompt prompt, IBlind blind)
        {
            return blind;
        }

        public void AddToHand(ICard card)
        {
            Hand.AddCard(card);
        }

        public override string ToString()
        {
            return Name;
        }
    }
}