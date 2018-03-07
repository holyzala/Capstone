using System;
using SharedSheep.Hand;
using SharedSheep.Card;
using SharedSheep.Blind;
using System.Collections.Generic;

namespace SharedSheep.Player
{
    public class LocalPlayer : IPlayer
    {
        public IHand Hand { get; set; }
        public string Name { get; private set; }

        public LocalPlayer(string name)
        {
            Hand = new Hand.Hand();
            Name = name;
        }

        public ICard PlayCard(Prompt prompt, ICard lead)
        {
            ICard card = null;
            bool done = false;
            while (!done)
            {
                try
                {
                    string answer = prompt(PromptType.PlayCard);
                    card = Hand.GetPlayableCards(lead)[Int32.Parse(answer)];
                    done = true;
                }
                catch (System.FormatException)
                {
                    continue;
                }
            }
            Hand.RemoveCard(card);
            return card;
        }

        public bool WantPick(Prompt prompt)
        {
            string answer = prompt(PromptType.Pick);
            if (answer.ToLower() == "yes" || answer.ToLower() == "y")
            {
                return true;
            }
            return false;
        }

        public IBlind Pick(Prompt prompt, IBlind blind)
        {
            while (true)
            {
                string answer = prompt(PromptType.PickBlind);
                if (answer == "done")
                    break;
                string[] split = answer.Split(' ');
                int blindCard = Int32.Parse(split[0]);
                int handCard = Int32.Parse(split[1]);
                Hand.Cards[handCard] = blind.SwapCard(blindCard, Hand.Cards[handCard]);
            }
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