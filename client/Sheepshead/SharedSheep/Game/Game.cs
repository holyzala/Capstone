using SharedSheep.Blind;
using SharedSheep.Card;
using SharedSheep.Deck;
using SharedSheep.Player;
using SharedSheep.Round;
using System.Collections.Generic;
using System.Linq;

namespace SharedSheep.Game
{
    public class Game : IGame
    {
        public List<IRound> Ronds { get; private set; }
        public IDeck Deck { get; private set; }
        public bool IsCracked { get; private set; }
        public IPlayer Picker { get; private set; }
        public IPlayer Partner { get; private set; }
        public IBlind Blind { get; private set; }
        public bool ForcedToPick { get; private set; }
        public ICard PartnerCard { get; private set; }
        public bool CallOutForJack { get; private set; }

        public Game()
        {
            Deck = new Piquet();
        }

        public void DealCard(IPlayer player)
        {
            for (int i = 0; i < 6; ++i)
            {
                player.AddToHand(Deck.GetTopCard());
            }
        }

        public void StartGame(List<IPlayer> players, Prompt prompt)
        {
            foreach (IPlayer player in players)
            {
                DealCard(player);
            }

            // The dealer is the first, so skip them until last
            foreach (IPlayer player in players.Skip(1))
            {
                if (player.WantPick(prompt))
                {
                    ForcedToPick = false;
                    Picker = player;
                    break;
                }
            }
            // If nobody else picked, then dealer is forced
            if (Picker == null)
            {
                ForcedToPick = true;
                Picker = players[0];
            }
            Picker.Pick(this.Blind);
        }
    }
}