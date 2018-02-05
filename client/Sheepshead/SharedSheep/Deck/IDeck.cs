using System;
using System.Collections.Generic;
using SharedSheep.Card;

namespace SharedSheep.Deck
{
    interface IDeck
    {
        List<ICard> Cards { get; set; }
        
        void Shuffle();
        int Size();
        void ResetDeck();
        bool RemoveCardByIndex(int index);
        ICard GetTopCard();
    }
}