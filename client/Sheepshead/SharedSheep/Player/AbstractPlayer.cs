using System.Linq;
using System.Collections.Generic;
using SharedSheep.Blind;
using SharedSheep.Card;
using SharedSheep.Hand;

namespace SharedSheep.Player
{
    public abstract class AbstractPlayer : IPlayer
    {
        public IHand Hand { get; set; }
        public string Name { get; protected set; }

        public void AddToHand(ICard card)
        {
            Hand.AddCard(card);
        }

        public abstract ICard Pick(Prompt prompt, IBlind blind, bool forced, ICard partnerCard);

        public abstract ICard PlayCard(Prompt prompt, ICard lead);

        public abstract bool WantPick(Prompt prompt);

        public override string ToString()
        {
            return Name;
        }

        protected ICard CallUp()
        {
            List<ICard> callUp = new List<ICard> {
                    new Card.Card(CardID.Jack, CardPower.JackHeart, Suit.Hearts),
                    new Card.Card(CardID.Jack, CardPower.JackSpade, Suit.Spades),
                    new Card.Card(CardID.Jack, CardPower.JackClub, Suit.Clubs),
                    new Card.Card(CardID.Queen, CardPower.QueenDiamond, Suit.Diamond),
                    new Card.Card(CardID.Queen, CardPower.QueenHeart, Suit.Hearts),
                    new Card.Card(CardID.Queen, CardPower.QueenSpade, Suit.Spades),
                    new Card.Card(CardID.Queen, CardPower.QueenClub, Suit.Clubs)
                };
            foreach (ICard card in callUp)
            {
                if (!Hand.Cards.Contains(card))
                    return card;
            }
            return null;
        }
    }
}