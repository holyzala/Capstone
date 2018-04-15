using SharedSheep.Blind;
using SharedSheep.Card;
using SharedSheep.Round;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SharedSheep.Player
{
    public class SimpleBot : AbstractPlayer
    {
        public SimpleBot(String name) : base(name)
        { }

        public override Boolean WantPick(Prompt prompt, List<IPlayer> players)
        {
            ICard JD = new Card.Card(CardID.Jack, CardPower.JackDiamond, Suit.Diamond);
            if (Hand.Cards.Contains(JD)) return false;
            int handPower = Hand.Cards.Aggregate(0, (total, card) => total + (int)card.Power);
            if (handPower >= (int)CardPower.QueenHeart + (int)CardPower.QueenDiamond + (int)CardPower.JackClub + (int)CardPower.KingTrump)
                return true;
            return false;
        }

        public override ICard PlayCard(Prompt prompt, List<IRound> rounds, IPlayer picker, IBlind blind, ICard partnerCard)
        {
            List<ICard> cards = Hand.GetPlayableCards(rounds.Last().Trick.LeadingCard());
            cards.Sort();
            cards.Reverse();
            Hand.RemoveCard(cards[0]);
            prompt(PromptType.BotPlayCard, new Dictionary<PromptData, object> {
                { PromptData.Player, this },
                { PromptData.Card, cards[0] },
                { PromptData.Trick, rounds.Last().Trick }
            });
            return cards[0];
        }

        public override ICard Pick(Prompt prompt, IBlind blind, bool forced, ICard partnerCard)
        {
            Hand.AddCard(blind.BlindCards[0]);
            Hand.AddCard(blind.BlindCards[1]);
            Hand.Cards.Sort((one, two) => two.Value - one.Value);
            int added = 0;
            int i = 0;
            while (i < Hand.Cards.Count)
            {
                if (added == 2) break;
                if (Hand.Cards[i].IsTrump())
                {
                    ++i;
                    continue;
                }
                blind.BlindCards[added++] = Hand.GetCard(i);
            }
            i = 0;
            while (added < 2 && i < Hand.Cards.Count)
            {
                if (Hand.Cards[i].ID == CardID.Queen || Hand.Cards[i].ID == CardID.Jack)
                {
                    ++i;
                    continue;
                }
                blind.BlindCards[added++] = Hand.GetCard(i);
            }
            if (added < 2)
            {
                Hand.Cards.Sort();
                while (added < 2)
                    blind.BlindCards[added++] = Hand.Cards[0];
            }
            if (forced && (Hand.Cards.Contains(partnerCard) || blind.BlindCards.Contains(partnerCard)))
                return CallUp(prompt, blind);

            return partnerCard;
        }
    }
}