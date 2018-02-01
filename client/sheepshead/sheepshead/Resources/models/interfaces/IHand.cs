using System;
namespace sheepshead.Resources.models.interfaces
{
    public interface IHand
    {
        ICard[] Cards { get; set; }

        int GetNumOfRemainingCards();
        ICard GetCard(int index);
        int NumOfThisSuit(Suit suit);
    }
}
