using System;
using System.Collections.Generic;
using SharedSheep.Card;

namespace SharedSheep.Hand
{
    public interface IHand
    {
        List<ICard> Cards { get;}

        int GetNumOfRemainingCards();
        ICard GetCard(int index);
        List<ICard> GetPlayableCards(ICard lead);
        int NumOfThisSuitInHand(Suit suit);
        void AddCard(ICard card);
        void RemoveCard(ICard card);
    }
}
