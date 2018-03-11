﻿using SharedSheep.Blind;
using SharedSheep.Card;
using SharedSheep.Hand;
using SharedSheep.Round;
using System;
using System.Collections.Generic;

namespace SharedSheep.Player
{
    public interface IPlayer
    {
        IHand Hand { get; set; }
        String Name { get; }

        ICard PlayCard(Prompt prompt, List<IRound> rounds, IPlayer picker, IBlind blind);

        Boolean WantPick(Prompt prompt);

        ICard Pick(Prompt prompt, IBlind blind, bool forced, ICard partnerCard);

        void AddToHand(ICard card);
    }
}