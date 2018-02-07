using System;
using System.Collections.Generic;
using System.Text;
using SharedSheep.Blind;
using SharedSheep.Card;
using SharedSheep.Deck;
using SharedSheep.Player;
using SharedSheep.Round;

namespace SharedSheep.Game
{
    public class Game : IGame
    {
        public List<IRound> Ronds { get; set; }
        public IDeck Deck { get; set; }
        public bool IsCracked { get; set; }
        public IPlayer Picker { get; set; }
        public IPlayer Partner { get; set; }
        public IBlind Blind { get; set; }
        public bool ForcedToPick { get; set; }
        public ICard PartnerCard { get; set; }
        public bool CallOutForJack { get; set; }

        public Game(IDeck deck) { Deck = deck;  }

        public void DealCard(IPlayer player)
        {
            for (int i = 0; i < 6; ++i)
            {
                player.AddToHand(Deck.GetTopCard());
            }
        }

        public void StartGame(List<IPlayer> players)
        {
            foreach (IPlayer player in players)
            {
                DealCard(player);
            }

            foreach (IPlayer player in players)
            {
                if (player.WantPick())
                {
                    player.Pick(this.Blind);
                    break;
                }
            }
        }

        public void SwapCard(ICard card)
        {
            throw new NotImplementedException();
        }
    }
}
