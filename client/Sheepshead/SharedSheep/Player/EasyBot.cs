using System;
using SharedSheep.Blind;
using SharedSheep.Card;
using SharedSheep.Round;
using System.Collections.Generic;
using System.Linq;

namespace SharedSheep.Player
{
    public class EasyBot : AbstractPlayer
    {
        private bool _isPartner;

        public EasyBot(string name) : base(name)
        { }

        public override ICard Pick(Prompt prompt, IBlind blind, bool forced, ICard partnerCard)
        {
            Hand.AddCard(blind.BlindCards[0]);
            Hand.AddCard(blind.BlindCards[1]);
            Hand.Cards.Sort((one, two) => two.Value - one.Value);
            var added = 0;
            var i = 0;
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
            var trick = rounds.Last().Trick;
            var cards = Hand.GetPlayableCards(trick.LeadingCard());
            cards.Sort();
            cards.Reverse();
            ICard playThis;
            if (!_isPartner && picker != this)
                _isPartner = Hand.Contains(partnerCard);
            if (!trick.Any())
            {
                if (picker == this)
                {
                    playThis = cards[0];
                }
                else if (_isPartner)
                {
                    playThis = cards[0];
                }
                else
                {
                    playThis = cards.Aggregate((acc, next) => acc.IsTrump() && !next.IsTrump() ? next : acc);
                }
            }
            else
            {
                var winCard = trick.TheWinnerCard();
                var isPickerWinning = trick.TheWinnerPlayer() == picker;
                if (isPickerWinning && _isPartner)
                {
                    playThis = cards.Aggregate((acc, next) => acc.Value >= next.Value ? acc : next);
                }
                else if (!isPickerWinning && _isPartner)
                {
                    playThis = cards[0];
                }
                else if (isPickerWinning && !_isPartner)
                {
                    if (cards[0].Power > winCard.Power)
                    {
                        playThis = cards[0];
                    }
                    else
                    {
                        cards.Sort();
                        playThis = cards.Aggregate((acc, next) => acc.Value <= next.Value ? acc : next);
                    }
                }
                else
                {
                    playThis = cards[0].Power > winCard.Power ? cards[0] : cards.Last();
                }
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
            var jd = new Card.Card(CardID.Jack, CardPower.JackDiamond, Suit.Diamond);
            Hand.Cards.Sort();
            if (Hand.Cards[0].Power >= CardPower.JackDiamond)
                return true;
            var qc = new Card.Card(CardID.Queen, CardPower.QueenClub, Suit.Clubs);
            var qs = new Card.Card(CardID.Queen, CardPower.QueenSpade, Suit.Spades);
            var trumpCount = 0;
            var queenCount = 0;
            var pointCards = 0;
            foreach (var card in Hand)
            {
                if (card.IsTrump()) ++trumpCount;
                if (card.ID == CardID.Queen) ++queenCount;
                if (card.Value >= 10) ++pointCards;
            }
            if (queenCount == 4) return true;
            if (Hand.Cards.Contains(jd)) return false;
            if ((players.First() == this || players.Last() == this) && Hand.Cards.Contains(qc) && Hand.Cards.Contains(qs))
                return true;
            if (queenCount >= 2 && trumpCount >= 3 && pointCards >= 1) return true;
            if (queenCount >= 1 && trumpCount >= 4 && pointCards >= 1) return true;
            return trumpCount >= 5;
        }
    }
}