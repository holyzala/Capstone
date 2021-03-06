﻿using SharedSheep.Blind;
using SharedSheep.Card;
using SharedSheep.Hand;
using SharedSheep.Round;
using System.Collections.Generic;

namespace SharedSheep.Player
{
    public interface IPlayer
    {
        IHand Hand { get; set; }
        string Name { get; }

        ICard PlayCard(Prompt prompt, List<IRound> rounds, IPlayer picker, IBlind blind, ICard partnerCard);

        bool WantPick(Prompt prompt, List<IPlayer> players);

        ICard Pick(Prompt prompt, IBlind blind, bool forced, ICard partnerCard);

        void AddToHand(ICard card);
    }
}