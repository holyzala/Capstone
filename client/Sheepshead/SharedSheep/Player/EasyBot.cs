using SharedSheep.Blind;
using SharedSheep.Card;
using SharedSheep.Round;
using SharedSheep.Trick;
using System.Collections.Generic;
using System.Linq;

namespace SharedSheep.Player
{
    public class EasyBot : AbstractPlayer
    {
        private bool IsPartner;

        public EasyBot(string name) : base(name)
        { }

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

        public override ICard PlayCard(Prompt prompt, List<IRound> rounds, IPlayer picker, IBlind blind, ICard partnerCard)
        {
            ITrick trick = rounds.Last().Trick;
            List<ICard> cards = Hand.GetPlayableCards(trick.LeadingCard());
            cards.Sort();
            cards.Reverse();
            ICard playThis = null;
            if (trick.Count() == 0)
            {
                if (picker == this)
                {
                    playThis = cards[0];
                }
                else if (IsPartner || Hand.Contains(partnerCard))
                {
                    IsPartner = true;
                    playThis = cards[0];
                }
                else
                {
                    playThis = cards.DefaultIfEmpty(cards[0]).FirstOrDefault(card => !card.IsTrump());
                }
            }
            else
            {
                playThis = cards[0];
            }
            Hand.RemoveCard(playThis);
            prompt(PromptType.BotPlayCard, new Dictionary<PromptData, object> {
                { PromptData.Player, this },
                { PromptData.Card, playThis },
                { PromptData.Trick, rounds.Last().Trick }
            });
            return playThis;
        }

        public override bool WantPick(Prompt prompt, List<IPlayer> players)
        {
            ICard JD = new Card.Card(CardID.Jack, CardPower.JackDiamond, Suit.Diamond);
            Hand.Cards.Sort();
            if (Hand.Cards[0].Power >= CardPower.JackDiamond)
                return true;
            ICard QC = new Card.Card(CardID.Queen, CardPower.QueenClub, Suit.Clubs);
            ICard QS = new Card.Card(CardID.Queen, CardPower.QueenSpade, Suit.Spades);
            int trumpCount = 0;
            int queenCount = 0;
            int pointCards = 0;
            foreach (ICard card in Hand)
            {
                if (card.IsTrump()) ++trumpCount;
                if (card.ID == CardID.Queen) ++queenCount;
                if (card.Value >= 10) ++pointCards;
            }
            if (queenCount == 4) return true;
            if (Hand.Cards.Contains(JD)) return false;
            if ((players.First() == this || players.Last() == this) && Hand.Cards.Contains(QC) && Hand.Cards.Contains(QS))
                return true;
            if (queenCount >= 2 && trumpCount >= 3 && pointCards >= 1) return true;
            if (queenCount >= 1 && trumpCount >= 4 && pointCards >= 1) return true;
            if (trumpCount >= 5) return true;
            return false;
        }
    }
}