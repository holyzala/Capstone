using System;
using System.Collections.Generic;
using SharedSheep.Player;
using SharedSheep.Card;

namespace SharedSheep.Trick
{
    interface ITrick
    {
        List<(IPlayer, ICard)> TrickCards { get; }

        IPlayer TheWinnerPlayer();
        ICard TheWinnerCard();
        ICard LeadingCard();


    }
}
