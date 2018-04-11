using SharedSheep.Blind;
using SharedSheep.Card;
using SharedSheep.Round;
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
            var currentTrick = rounds.Last().Trick;
            while (true)
            {
                var cards = Hand.GetPlayableCards(currentTrick.LeadingCard());
                var answer = prompt(PromptType.PlayCard, new Dictionary<PromptData, object>
                {
                    {PromptData.Player, this},
                    {PromptData.Picker, picker},
                    {PromptData.Trick, currentTrick},
                    {PromptData.Cards, cards}
                });
                if (answer == "")
                    continue;

                try
                {
                    card = cards[int.Parse(answer)];
                    break;
                }
                catch (System.FormatException)
                {
                }
            }

            Hand.RemoveCard(card);
            return card;
        }

        public override bool WantPick(Prompt prompt, List<IPlayer> players)
        {
            var answer = "";
            while (true)
            {
                answer = prompt(PromptType.Pick, new Dictionary<PromptData, object>
                {
                    { PromptData.Player, this }
                });
                if (answer != "")
                    break;
            }

            return answer.ToLower() == "yes" || answer.ToLower() == "y";
        }

        public override ICard Pick(Prompt prompt, IBlind blind, bool forced, ICard partnerCard)
        {
            while (true)
            {
                var answer = prompt(PromptType.PickBlind, new Dictionary<PromptData, object>
                {
                    { PromptData.Player, this },
                    { PromptData.Blind, blind }
                });
                if (answer == "")
                    continue;
                if (answer == "done")
                    break;

                var split = answer.Split(' ');
                int blindCard;
                int handCard;
                try
                {
                    blindCard = int.Parse(split[0]);
                    handCard = int.Parse(split[1]);
                }
                catch (System.FormatException)
                {
                    continue;
                }
                catch (System.IndexOutOfRangeException)
                {
                    continue;
                }
                Hand.Cards[handCard] = blind.SwapCard(blindCard, Hand.Cards[handCard]);
            }

            if (!forced || (!Hand.Cards.Contains(partnerCard) && !blind.BlindCards.Contains(partnerCard)))
                return partnerCard;
            {
                var answer = "";
                while (true)
                {
                    answer = prompt(PromptType.CallUp, new Dictionary<PromptData, object>
                    {
                        {PromptData.Player, this}
                    });
                    if (answer != "")
                        break;
                }

                if (answer.ToLower() == "yes" || answer.ToLower() == "y")
                    return CallUp(prompt, blind);
            }
            return partnerCard;
        }
    }
}