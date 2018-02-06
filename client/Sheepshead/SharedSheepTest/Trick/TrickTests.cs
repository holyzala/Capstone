using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharedSheep.Trick;
using SharedSheep.Player;
using SharedSheep.Card;


namespace SharedSheepTest.TrickTests
{
    [TestClass]
    public class TrickTests
    {
        Card c1 = new Card(CardID.Ace, 11, CardPower.AceFail, Suit.Clubs);
        Card c2 = new Card(CardID.Eight, 0, CardPower.EightTrump, Suit.Trump);
        Card c3 = new Card(CardID.King, 4, CardPower.KingFail, Suit.Clubs);
        Card c4 = new Card(CardID.Jack, 2, CardPower.JackSpade, Suit.Trump);
        Card c5 = new Card(CardID.Seven, 0, CardPower.SevenFail, Suit.Clubs);

        LocalPlayer p1 = new LocalPlayer("p1");
        LocalPlayer p2 = new LocalPlayer("p2");
        LocalPlayer p3 = new LocalPlayer("p3");
        LocalPlayer p4 = new LocalPlayer("p4");
        LocalPlayer p5 = new LocalPlayer("p5");

        List<(IPlayer, ICard)> list = new List<(IPlayer, ICard)>(5);

        Trick trick;


        [TestMethod]
        public void LeadingCardTest()
        {
            list.Add((p1, c1));
            list.Add((p2, c2));
            trick = new Trick(list);
            Assert.AreEqual(c1, trick.LeadingCard());
            Assert.AreNotEqual(c2, trick.LeadingCard());
        }

        [TestMethod]
        public void TheWinnerCardTest()
        {
            list.Add((p1, c1));
            list.Add((p2, c2));
            list.Add((p3, c3));
            list.Add((p4, c4));
            list.Add((p5, c5));
            trick = new Trick(list);
            Assert.AreEqual(c4, trick.TheWinnerCard());
        }

        [TestMethod]
        public void TheWinnerPlayer()
        {
            list.Add((p1, c1));
            list.Add((p2, c2));
            list.Add((p3, c3));
            list.Add((p4, c4));
            list.Add((p5, c5));
            trick = new Trick(list);
            Assert.AreEqual(p4.Name, trick.TheWinnerPlayer().Name);

        }


    }
}
