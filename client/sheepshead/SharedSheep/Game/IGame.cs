using System;
using System.Collections.Generic;
using System.Text;
using SharedSheep.Round;
using SharedSheep.Deck;
using SharedSheep.Player;
using SharedSheep.Blind;
using SharedSheep.Card;
using SharedSheep.ScoreSheet;


namespace SharedSheep.Game
{
    public interface IGame
    {
        List<IRound> Rounds { get; }
        IDeck Deck { get; }
        bool IsCracked{get; }
        IPlayer Picker { get; }
        IPlayer Partner { get; }
        IBlind Blind { get;  }
        bool ForcedToPick { get; }
        ICard PartnerCard { get; }
        bool CallOutForJack { get; }

        void StartGame(List<IPlayer> players);
        List<IScoreSheet> GameScore();
        void DealCard(IPlayer player);
        
    }
}
