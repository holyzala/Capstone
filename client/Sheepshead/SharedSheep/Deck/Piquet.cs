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
            foreach (CardID cID in Enum.GetValues(typeof(CardID)))
            {
                switch (cID)
                {
                    case CardID.Seven:
                        foreach (Suit suit in Enum.GetValues(typeof(Suit)))
                        {
                            if (suit == Suit.Trump)
                                Cards.Add(new Card.Card(cID, 0, CardPower.SevenTrump, suit));
                            else
                                Cards.Add(new Card.Card(cID, 0, CardPower.SevenFail, suit));
                        }
                        break;

                    case CardID.Eight:
                        foreach (Suit suit in Enum.GetValues(typeof(Suit)))
                        {
                            if (suit == Suit.Trump)
                                Cards.Add(new Card.Card(cID, 0, CardPower.EightTrump, suit));
                            else
                                Cards.Add(new Card.Card(cID, 0, CardPower.EightFail, suit));
                        }
                        break;

                    case CardID.Nine:
                        foreach (Suit suit in Enum.GetValues(typeof(Suit)))
                        {
                            if (suit == Suit.Trump)
                                Cards.Add(new Card.Card(cID, 0, CardPower.NineTrump, suit));
                            else
                                Cards.Add(new Card.Card(cID, 0, CardPower.NineFail, suit));
                        }
                        break;

                    case CardID.Ten:
                        foreach (Suit suit in Enum.GetValues(typeof(Suit)))
                        {
                            if (suit == Suit.Trump)
                                Cards.Add(new Card.Card(cID, 0, CardPower.TenTrump, suit));
                            else
                                Cards.Add(new Card.Card(cID, 0, CardPower.TenFail, suit));
                        }
                        break;

                    case CardID.Jack:
                        CardPower pow = CardPower.JackDiamond;
                        foreach (Suit suit in Enum.GetValues(typeof(Suit)))
                        {
                            Cards.Add(new Card.Card(cID, 2, (CardPower)pow, Suit.Trump));
                            pow++;
                        }
                        break;

                    case CardID.Queen:
                        pow = CardPower.QueenDiamond;
                        foreach (Suit suit in Enum.GetValues(typeof(Suit)))
                        {
                            Cards.Add(new Card.Card(cID, 3, (CardPower)pow, Suit.Trump));
                            pow++;
                        }
                        break;

                    case CardID.King:
                        foreach (Suit suit in Enum.GetValues(typeof(Suit)))
                        {
                            if (suit == Suit.Trump)
                                Cards.Add(new Card.Card(cID, 0, CardPower.KingTrump, suit));
                            else
                                Cards.Add(new Card.Card(cID, 0, CardPower.KingFail, suit));
                        }
                        break;

                    case CardID.Ace:
                        foreach (Suit suit in Enum.GetValues(typeof(Suit)))
                        {
                            if (suit == Suit.Trump)
                                Cards.Add(new Card.Card(cID, 0, CardPower.AceTrump, suit));
                            else
                                Cards.Add(new Card.Card(cID, 0, CardPower.AceFail, suit));
                        }
                        break;
                }
            }
        }
    }
}