using System;
using System.Collections.Generic;
using SharedSheep.Card;

namespace SharedSheep.Blind
{
    public interface IBlind
    {
        List<ICard> BlindCards { get; set; }
        void AddCard(ICard card);
        ICard SwapCard(int index, ICard card);
        int BlindPoints();
    }
}