﻿using System;

namespace SharedSheep.Card
{
    public interface ICard
    {
        CardID ID { get; }
        int Value { get; } //score value
        CardPower Power { get; }
        Suit CardSuit { get; }

        Boolean IsTrump();
        Boolean IsHigher(ICard otherCard); //compare powers
    }
}