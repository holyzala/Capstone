using System;

namespace SharedSheep.Card
{
    public class Card : ICard
    {
        public CardID ID { get; private set; }
        public int Value { get; private set; }
        public CardPower Power { get; private set; }
        public Suit CardSuit { get; private set; }
        private bool isTrump = false;

        public Card(CardID num, CardPower power, Suit cardSuit)
        {
            this.ID = num;
            Value = GetValue();
            this.Power = power;
            this.CardSuit = cardSuit;
            if (ID == CardID.Queen || ID == CardID.Jack || CardSuit == Suit.Diamond)
                isTrump = true;
        }

        private int GetValue()
        {
            switch (ID)
            {
                case CardID.Ace:
                    return 11;

                case CardID.Ten:
                    return 10;

                case CardID.King:
                    return 4;

                case CardID.Queen:
                    return 3;

                case CardID.Jack:
                    return 2;

                default:
                    return 0;
            }
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
            return isTrump;
        }

        public override string ToString()
        {
            return String.Format("ID: {0}, Value: {1}, Power: {2}, Suit: {3}", ID, Value, (int)Power, CardSuit);
        }
    }
}