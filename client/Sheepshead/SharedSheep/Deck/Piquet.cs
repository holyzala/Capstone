using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using SharedSheep.Card;

namespace SharedSheep.Deck
{
    public class Piquet : IDeck
    {
        public List<ICard> Cards { get ; set ; }


        public Piquet()
        {
            Cards = new List<ICard>();
            SetAllCards();
            Shuffle();
        }

        public ICard GetTopCard()
        {
            ICard topCard = Cards[0];
            Cards.RemoveAt(0);
            return topCard;
        }


        public bool RemoveCardByIndex(int index)
        {
            if (Cards[index] == null)
                return false;
            else Cards.RemoveAt(index);
            return true;
        }

        public void ResetDeck()
        {
            Cards.Clear();
            SetAllCards();
        }


        public void Shuffle()
        {
            RNGCryptoServiceProvider provider = new RNGCryptoServiceProvider();
            int n = Cards.Count;
            while (n > 1)
            {
                byte[] box = new byte[1];
                do provider.GetBytes(box);
                while (!(box[0] < n * (Byte.MaxValue / n)));
                int k = (box[0] % n);
                n--;
                ICard value = Cards[k];
                Cards[k] = Cards[n];
                Cards[n] = value;
            }
        }

        public int Size()
        {
            return Cards.Count;
        }


        private void SetAllCards()
        {
            CardPower pow = CardPower.SevenFail;
            foreach (CardID cID in Enum.GetValues(typeof(CardID)))
            {
                if (cID == CardID.Jack)
                    pow = CardPower.JackDiamond;
                if (cID == CardID.Queen)
                    pow = CardPower.QueenDiamond;

                foreach (Suit suit in Enum.GetValues(typeof(Suit)))
                {
                    switch (cID)
                    {
                        case CardID.Jack:
                            Cards.Add(new Card.Card(cID, pow, suit));
                            ++pow;
                            break;
                        case CardID.Queen:
                            Cards.Add(new Card.Card(cID, pow, suit));
                            ++pow;
                            break;
                        default:
                            if (suit == Suit.Diamond)
                                Cards.Add(new Card.Card(cID, pow + 6, suit));
                            else
                                Cards.Add(new Card.Card(cID, pow, suit));
                            break;
                    }
                }
                ++pow;
            }
        }
    }
}