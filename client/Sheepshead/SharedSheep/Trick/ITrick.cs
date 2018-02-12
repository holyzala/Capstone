using System;
using System.Collections.Generic;
using SharedSheep.Player;
using SharedSheep.Card;

namespace SharedSheep.Trick
{
    public interface ITrick
    {
        List<(IPlayer, ICard)> TrickCards { get; }

        IPlayer TheWinnerPlayer();
        ICard TheWinnerCard();
        ICard LeadingCard();
        void AddCardAndPlayer(IPlayer player,ICard card);

    }
}
