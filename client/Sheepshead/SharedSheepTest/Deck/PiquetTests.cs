using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharedSheep.Deck;
using SharedSheep.Card;

namespace SharedSheepTest.Deck
{
    [TestClass]
    public class PiquetTests
    {
        private Piquet deck;

        [TestInitialize()]
        public void Initialize()
        {
            deck = new Piquet();
        }

        [TestMethod()]
        public void GetTopCardTest()
        {
            Card card = new Card(CardID.Eight, 3, CardPower.JackDiamond, Suit.Hearts);
            Card card2 = new Card(CardID.Jack, 2, CardPower.JackClub, Suit.Clubs);
            deck.Cards.Insert(0, card);
            deck.Cards.Insert(1,card2);
            Assert.AreEqual(card, deck.GetTopCard());
            Assert.AreNotEqual(card, deck.Cards[0]);
            Assert.AreEqual(card2, deck.GetTopCard());

        }

        [TestMethod()]
        public void ResetDeckTest()
        {
            deck.ResetDeck();
            Assert.AreEqual(32, deck.Cards.Count);
            Card sevenTrump = new Card(CardID.Seven, 0, CardPower.SevenTrump, Suit.Trump);
            Assert.AreEqual(deck.Cards[0], sevenTrump);
            Card queenClubs = new Card(CardID.Queen, 3, CardPower.QueenClub, Suit.Trump);
            //System.Diagnostics.Trace.WriteLine($"{deck.Cards[31].Power}:{deck.Cards[31].ID}:{deck.Cards[31].CardSuit}");
            Assert.AreEqual(deck.Cards[31], queenClubs);
        }
    }
}
