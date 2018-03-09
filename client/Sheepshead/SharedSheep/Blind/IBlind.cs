using SharedSheep.Card;
using System.Collections.Generic;

namespace SharedSheep.Blind
{
    public interface IBlind : IEnumerable<ICard>
    {
        List<ICard> BlindCards { get; }

        void AddCard(ICard card);

        ICard SwapCard(int index, ICard card);

        int BlindPoints();
    }
}