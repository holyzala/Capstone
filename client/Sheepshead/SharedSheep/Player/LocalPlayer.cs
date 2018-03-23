using SharedSheep.Blind;
using SharedSheep.Card;
using SharedSheep.Round;
using SharedSheep.Trick;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SharedSheep.Player
{
    public class LocalPlayer : AbstractPlayer
    {
        public LocalPlayer(string name) : base(name)
        { }

        public override ICard PlayCard(Prompt prompt, List<IRound> rounds, IPlayer picker, IBlind blind, ICard partnerCard)
        {
            ICard card = null;
            bool done = false;
            ITrick currentTrick = rounds.Last().Trick;
            while (!done)
            {
                try
                {
                    List<ICard> cards = Hand.GetPlayableCards(currentTrick.LeadingCard());
                    string answer = prompt(PromptType.PlayCard, new Dictionary<PromptData, object>
                    {
                        { PromptData.Player, this},
                        { PromptData.Picker, picker },
                        { PromptData.Trick, currentTrick },
                        { PromptData.Cards, cards }
                    });
                    card = cards[Int32.Parse(answer)];
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

        public override bool WantPick(Prompt prompt, List<IPlayer> players)
        {
            string answer = prompt(PromptType.Pick, new Dictionary<PromptData, object>
            {
                { PromptData.Player, this }
            });
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
                string answer = prompt(PromptType.PickBlind, new Dictionary<PromptData, object>
                {
                    { PromptData.Player, this },
                    { PromptData.Blind, blind }
                });
                if (answer == "done" || answer == "")
                    break;
                string[] split = answer.Split(' ');
                int blindCard = Int32.Parse(split[0]);
                int handCard = Int32.Parse(split[1]);
                Hand.Cards[handCard] = blind.SwapCard(blindCard, Hand.Cards[handCard]);
            }
            if (forced && (Hand.Cards.Contains(partnerCard) || blind.BlindCards.Contains(partnerCard)))
            {
                string answer = prompt(PromptType.CallUp, new Dictionary<PromptData, object>
                {
                    { PromptData.Player, this }
                });
                if (answer.ToLower() == "yes" || answer.ToLower() == "y")
                    return CallUp(prompt, blind);
            }
            return partnerCard;
        }
    }
}