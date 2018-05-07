using System.Collections.Generic;
using SharedSheep.Round;
using SharedSheep.Player;
using SharedSheep.Blind;
using SharedSheep.Card;

namespace SharedSheep.Game
{
    public interface IGame
    {
        List<IRound> Rounds { get; }
        bool IsCracked { get; }
        IPlayer Picker { get; }
        IPlayer Partner { get; }
        IBlind Blind { get; }
        bool ForcedToPick { get; }
        ICard PartnerCard { get; }

        void StartGame(List<IPlayer> players, Prompt prompt);

        //ScoreSheet gameScore()

        IPlayer GetCurrentPlayer();

        int GetPickerScore();

        int GetPickerTrickCount();
    }
}