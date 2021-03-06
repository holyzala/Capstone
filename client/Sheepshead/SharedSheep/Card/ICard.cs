﻿using System;

namespace SharedSheep.Card
{
    public interface ICard : IComparable<ICard>, IEquatable<ICard>
    {
        CardID ID { get; }
        int Value { get; } //score value
        CardPower Power { get; }
        Suit CardSuit { get; }

        bool IsTrump();
    }
}