using System;
using System.Collections.Generic;
using SharedSheep.Card;

namespace SharedSheep.Blind
{
    class Blind : IBlind
    {
        public List<ICard> BlindCards { get; set ; }

        public void AddCard(ICard card)
        {
            if (BlindCards.Count < 3) //what if not?
                BlindCards.Add(card);
        }

        public int BlindPoints()
        {
            return BlindCards[0].Value + BlindCards[1].Value;
        }

        public ICard SwapCard(int index, ICard card)
        {
            ICard outCard = BlindCards[index];
            BlindCards[index] = card;

            return outCard;
          
        }
    }
}