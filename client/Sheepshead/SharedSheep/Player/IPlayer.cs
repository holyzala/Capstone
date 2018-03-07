using System;
using SharedSheep.Card;
using SharedSheep.Hand;
using SharedSheep.Blind;

namespace SharedSheep.Player
{
    public interface IPlayer
    {
        IHand Hand { get; set; }
        String Name { get; }

        ICard PlayCard(Prompt prompt, ICard lead);

        Boolean WantPick(Prompt prompt);

        ICard Pick(Prompt prompt, IBlind blind, bool forced, ICard partnerCard);

        void AddToHand(ICard card);
    }
}