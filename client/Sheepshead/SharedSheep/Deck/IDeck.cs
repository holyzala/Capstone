using System;
using System.Collections.Generic;
using SharedSheep.Card;

namespace SharedSheep.Deck
{
    public interface IDeck
    {
        List<ICard> Cards { get; set; }
        
        void Shuffle();
        int Size();
        void ResetDeck();
        bool RemoveCardByIndex(int index);
        ICard GetTopCard();
        void CardsFactory();
    }
}