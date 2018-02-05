using System;
using System.Collections.Generic;
using SharedSheep.Card;

namespace SharedSheep.Hand
{
    public interface IHand
    {
        List<ICard> Cards { get; set; }

        int GetNumOfRemainingCards();
        ICard GetCard(int index);
        int NumOfThisSuit(Suit suit);
    }
}
