using Microsoft.VisualStudio.TestTools.UnitTesting;
using sheepshead.Resources.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace sheepshead.Resources.models.Tests
{
    [TestClass()]
    public class PiquetTests
    {
        private Piquet deck;

        [TestInitialize()]
        public void Initialize()
        {
            deck = new Piquet();
        }

        [TestMethod()]
        public void PiquetTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void GetTopCardTest()
        {
            Card card = new Card(CardID.Eight, 3, CardPower.JackDiamond, Suit.Hearts);
            deck.Cards.Insert(0, card);
            Assert.AreEqual(card, deck.GetTopCard());
            Assert.AreNotEqual(card, deck.Cards[0]);
        }

        [TestMethod()]
        public void RemoveCardByIndexTest()
        {
            Assert.Fail();
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

        [TestMethod()]
        public void ShuffleTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void SizeTest()
        {
            Assert.Fail();
        }
    }
}