using System;
using SharedSheep.Card;
using SharedSheep.Blind;

namespace SharedSheep.Player
{
    public class LocalPlayer : AbstractPlayer
    {
        public LocalPlayer(string name)
        {
            Hand = new Hand.Hand();
            Name = name;
        }

        public override ICard PlayCard(Prompt prompt, ICard lead)
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

        public override bool WantPick(Prompt prompt)
        {
            string answer = prompt(PromptType.Pick);
            if (answer.ToLower() == "yes" || answer.ToLower() == "y")
            {
                return true;
            }
            return false;
        }

        public override ICard Pick(Prompt prompt, IBlind blind, bool forced, ICard partnerCard)
        {
            while (true)
            {
                string answer = prompt(PromptType.PickBlind);
                if (answer == "done" || answer == "")
                    break;
                string[] split = answer.Split(' ');
                int blindCard = Int32.Parse(split[0]);
                int handCard = Int32.Parse(split[1]);
                Hand.Cards[handCard] = blind.SwapCard(blindCard, Hand.Cards[handCard]);
            }
            if (forced && (Hand.Cards.Contains(partnerCard) || blind.BlindCards.Contains(partnerCard)))
            {
                string answer = prompt(PromptType.CallUp);
                if (answer.ToLower() == "yes" || answer.ToLower() == "y")
                    return CallUp();
            }
            return partnerCard;
        }
    }
}