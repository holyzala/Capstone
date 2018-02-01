using System;
using System.Collections.Generic;

namespace sheepshead.Resources.models.interfaces
{
    public interface IHand
    {
        List<ICard> Cards { get; set; }

        int GetNumOfRemainingCards();
        ICard GetCard(int index);
        int NumOfThisSuit(Suit suit);
    }
}
