using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharedSheep.Card;
using SharedSheep.Blind;

namespace SharedSheepTest.BlindTests
{
    [TestClass]
    public class BlindTests
    {
        ICard card1 = new Card(CardID.Eight, 0, CardPower.EightFail, Suit.Clubs);
        ICard card2 = new Card(CardID.Jack, 2, CardPower.JackSpade, Suit.Trump);
        ICard card3 = new Card(CardID.King, 4, CardPower.KingFail, Suit.Spades);


        [TestMethod]
        public void AddCardTest()
        {
            Blind blind = new Blind();
            blind.AddCard(card1);
            blind.AddCard(card2);
            Assert.AreEqual(card1, blind.BlindCards[0]);
            Assert.AreEqual(card2, blind.BlindCards[1]);
            blind.AddCard(card3);
            Assert.AreEqual(2, blind.BlindCards.Count);
            Assert.AreEqual(card1, blind.BlindCards[0]);
            Assert.AreEqual(card2, blind.BlindCards[1]);
        }

        [TestMethod]
        public void BlindPointsTest()
        {
            Blind blind = new Blind();
            blind.AddCard(card1);
            blind.AddCard(card2);
            Assert.AreEqual(2, blind.BlindPoints());
        }

        [TestMethod]
        public void SwapCardTest()
        {
            Blind blind = new Blind();
            blind.AddCard(card1);
            blind.AddCard(card2);
            blind.SwapCard(0,card3);
            Assert.AreEqual(card3, blind.BlindCards[0]);
            Assert.AreEqual(card2, blind.BlindCards[1]);
            blind.SwapCard(1, card1);
            Assert.AreEqual(card1, blind.BlindCards[1]);
        }
    }
}
