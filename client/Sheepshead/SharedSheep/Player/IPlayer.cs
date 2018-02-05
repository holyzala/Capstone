using System;
using SharedSheep.Card;
using SharedSheep.Hand;

namespace SharedSheep.Player
{
    public interface IPlayer
    {
        IHand Hand { get; set; }
        String Name { get; }

        Boolean IsPartner();
        ICard PlayCard();
        Boolean WantPick();
        // IBlind Pick(IBlind blind);

    }
}
