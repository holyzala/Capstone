using System;
using System.Collections.Generic;
using System.Text;
using SharedSheep.Round;
using SharedSheep.Deck;
using SharedSheep.Player;
using SharedSheep.Blind;
using SharedSheep.Card;


namespace SharedSheep.Game
{
    public interface IGame
    {
        List<IRound> Ronds { get; set; }
        IDeck Deck { get; set; }
        bool IsCracked{get; set;}
        IPlayer Picker { get; set; }
        IPlayer Partner { get; set; }
        IBlind Blind { get; set; }
        bool ForcedToPick { get; set; }
        ICard PartnerCard { get; set; }
        bool CallOutForJack { set; get; }

        void StartGame(List<IPlayer> players);
        void SwapCard(ICard card);
        //ScoreSheet gameScore()
        void DealCard(IPlayer player);
        
    }
}
