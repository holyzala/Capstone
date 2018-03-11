using SharedSheep.Card;
using SharedSheep.Player;
using System.Collections.Generic;

namespace SharedSheep.Trick
{
    public interface ITrick : IEnumerable<(IPlayer, ICard)>
    {
        List<(IPlayer, ICard)> TrickCards { get; }

        IPlayer TheWinnerPlayer();

        ICard TheWinnerCard();

        ICard LeadingCard();

        void AddCardAndPlayer(IPlayer player, ICard card);

        int TrickValue();
    }
}