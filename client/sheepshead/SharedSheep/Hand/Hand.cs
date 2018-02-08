using System;
using System.Collections.Generic;
using SharedSheep.Card;

namespace SharedSheep.Hand
{
    public class Hand: IHand
    {
        public List<ICard> Cards { get; set; }

        public Hand()
        {
            Cards = new List<ICard>();
        }
        
        public int GetNumOfRemainingCards()
        {
            int size = 0;
            for (int i=0; i < Cards.Count; i++)
            {
                if(Cards[i] != null)  size++;
            }
            return size;
        }

        public ICard GetCard(int index)
        {
            if (index >= 6 || index < 0)
                throw new IndexOutOfRangeException();
            else if (Cards[index] == null)
                throw new NullReferenceException("Card doesn't exist");

            else
            return Cards[index];
        }


        public int NumOfThisSuit(Suit suit)
        {
            int numOfCards = 0;
            for(int i=0; i< Cards.Count; i++)
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
    }
}