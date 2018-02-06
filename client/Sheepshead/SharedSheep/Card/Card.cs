using System;

namespace SharedSheep.Card
{
    public class Card : ICard
    {
        public CardID ID { get; private set; }
        public int Value { get; private set; }
        public CardPower Power { get; private set; }
        public Suit CardSuit { get; private set; }

        public Card(CardID num, int value, CardPower power, Suit cardSuit)
        {
            this.ID = num;
            this.Value = value;
            this.Power = power;
            this.CardSuit = cardSuit;
        }

        public override bool Equals(object obj)
        {
            // Check for null values and compare run-time types.
            if (obj == null || GetType() != obj.GetType())
                return false;

            Card c = (Card)obj;
            return (ID == c.ID) && (Value == c.Value) && (Power == c.Power) && (CardSuit == c.CardSuit);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public bool IsHigher(ICard otherCard)
        {
            return this.Power > otherCard.Power;
        }

        public bool IsTrump()
        {
            return this.CardSuit == Suit.Trump;
        }
    }
}