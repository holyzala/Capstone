using SharedSheep.Card;
using System.Collections.Generic;

namespace SharedSheep.Hand
{
    public interface IHand : IEnumerable<ICard>
    {
        List<ICard> Cards { get; }

        int GetNumOfRemainingCards();

        ICard GetCard(int index);

        List<ICard> GetPlayableCards(ICard lead);

        int NumOfThisSuitInHand(Suit suit);

        void AddCard(ICard card);

        void RemoveCard(ICard card);
    }
}