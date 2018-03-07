using System;
using System.Collections.Generic;
using SharedSheep.Card;

namespace SharedSheep.Hand
{
    public class Hand : IHand
    {
        public List<ICard> Cards { get; private set; }

        public Hand()
        {
            Cards = new List<ICard>();
        }

        public int GetNumOfRemainingCards()
        {
            int size = 0;
            for (int i = 0; i < Cards.Count; i++)
            {
                if (Cards[i] != null) size++;
            }
            return size;
        }

        public ICard GetCard(int index)
        {
            if (index >= Cards.Count || index < 0)
                throw new IndexOutOfRangeException();
            if (Cards[index] == null)
                throw new NullReferenceException("Card doesn't exist");
            ICard card = Cards[index];
            Cards.Remove(card);
            return card;

        }

        public List<ICard> GetPlayableCards(ICard lead)
        {
            if (lead == null)
                return new List<ICard>(Cards);
            List<ICard> cards = new List<ICard>();
            if (lead.IsTrump())
            {
                foreach(ICard c in Cards)
                {
                    if (c.IsTrump())
                    {
                        cards.Add(c);
                    }
                }
                if (cards.Count == 0)
                    cards.AddRange(Cards);
                return cards;
            }
            foreach(ICard c in Cards)
            {
                if (c.CardSuit == lead.CardSuit && !c.IsTrump())
                {
                    cards.Add(c);
                }
            }
            if (cards.Count == 0)
                cards.AddRange(Cards);
            return cards;
        }

        public int NumOfThisSuitInHand(Suit suit)
        {
            int numOfCards = 0;
            for (int i = 0; i < Cards.Count; i++)
            {
                if (this.Cards[i].CardSuit == suit)
                    numOfCards++;
            }
            return numOfCards;
        }

        public void AddCard(ICard card)
        {
            Cards.Add(card);
        }

        public void RemoveCard(ICard card)
        {
            Cards.Remove(card);
        }
    }
}