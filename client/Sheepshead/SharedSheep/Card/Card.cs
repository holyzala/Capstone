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
            isTrump = (ID == CardID.Queen || ID == CardID.Jack || CardSuit == Suit.Diamond);
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

        public override int GetHashCode()
        {
            unchecked // Overflow is fine, just wrap
            {
                int hash = 17;
                hash = hash * 23 + ID.GetHashCode();
                hash = hash * 23 + Value.GetHashCode();
                hash = hash * 23 + Power.GetHashCode();
                hash = hash * 23 + CardSuit.GetHashCode();
                return hash;
            }
        }

        public bool IsTrump()
        {
            return isTrump;
        }

        public override string ToString()
        {
            return String.Format("ID: {0}, Value: {1}, Power: {2}, Suit: {3}", ID, Value, (int)Power, CardSuit);
        }

        public int CompareTo(ICard other)
        {
            return Power.CompareTo(other.Power);
        }

        public bool Equals(ICard other)
        {
            return (ID == other.ID) && (Value == other.Value) && (Power == other.Power) && (CardSuit == other.CardSuit);
        }
    }
}