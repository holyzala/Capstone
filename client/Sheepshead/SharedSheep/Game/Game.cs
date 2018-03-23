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
        public List<IRound> Rounds { get; private set; }
        public bool IsCracked { get; private set; }
        public IPlayer Picker { get; private set; }
        public IPlayer Partner { get; private set; }
        public IBlind Blind { get; private set; }
        public bool ForcedToPick { get; private set; }
        public ICard PartnerCard { get; private set; }
        public bool CallOutForJack { get; private set; }

        public Game()
        {
            Rounds = new List<IRound>();
            IsCracked = false;
            Blind = new Blind.Blind();
            PartnerCard = new Card.Card(CardID.Jack, CardPower.JackDiamond, Suit.Diamond);
        }

        public IPlayer GetCurrentPlayer()
        {
            return Rounds.Last().CurrentPlayer;
        }

        private void DealCards(List<IPlayer> players)
        {
            bool missingTrump = true;
            while (missingTrump)
            {
                IDeck Deck = new Piquet();
                int timesAround = Deck.Cards.Count / players.Count;

                for (int i = 0; i < timesAround; ++i)
                {
                    foreach (IPlayer player in players)
                    {
                        player.AddToHand(Deck.GetTopCard());
                    }
                }
                Blind.AddCard(Deck.GetTopCard());
                Blind.AddCard(Deck.GetTopCard());

                players.ForEach(player =>
                {
                    missingTrump &= !player.Hand.Cards.Aggregate(false, (agg, card) => agg || card.IsTrump());
                });
                // If nobody got trump, wipe the hands and blind and start again
                if (missingTrump)
                {
                    players.ForEach(player =>
                    {
                        player.Hand = new Hand.Hand();
                    });
                    Blind = new Blind.Blind();
                }
            }
        }

        public void StartGame(List<IPlayer> players, Prompt prompt)
        {
            DealCards(players);
            prompt(PromptType.CardsDealt, new Dictionary<PromptData, object> {
                { PromptData.Players, players },
                {PromptData.Blind, Blind }
            });
            // The dealer is the first, so skip them until last
            foreach (IPlayer player in players.Skip(1))
            {
                if (player.WantPick(prompt, players.Skip(1).Concat(players.Take(1)).ToList()))
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
            PartnerCard = Picker.Pick(prompt, this.Blind, ForcedToPick, PartnerCard);

            Partner = players.Aggregate((IPlayer)null, (agg, player) => player.Hand.Cards.Contains(PartnerCard) && Picker != player ? player : agg);
            IPlayer roundStarter = players[1];
            while (Rounds.Count < 6)
            {
                IRound newRound = new Round.Round(Rounds.Count, roundStarter);
                Rounds.Add(newRound);
                int i = players.IndexOf(roundStarter);
                roundStarter = newRound.Start(prompt, players.Skip(i).Concat(players.Take(i)).ToList(), Rounds, Picker, Blind, PartnerCard);
                prompt(PromptType.RoundOver, new Dictionary<PromptData, object>
                {
                    { PromptData.Round, newRound }
                });
            }
        }

        public int GetPickerTrickCount()
        {
            int count = 0;
            Rounds.ForEach(round =>
            {
                IPlayer winner = round.Trick.TheWinnerPlayer();
                if (winner == Picker || winner == Partner)
                    ++count;
            });
            return count;
        }

        public int GetPickerScore()
        {
            int total = 0;
            int pickTricks = 0;
            Rounds.ForEach(round =>
            {
                IPlayer winner = round.Trick.TheWinnerPlayer();
                if (winner == Picker)
                    ++pickTricks;
                if (winner == Picker || winner == Partner)
                    total += round.Trick.TrickValue();
            });
            if (pickTricks > 0)
                total += Blind.BlindPoints();
            return total;
        }
    }
}