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

        Trick trick;


        [TestMethod]
        public void LeadingCardTest()
        {

            trick = new Trick();
            trick.AddCardAndPlayer(p1, c1);
            trick.AddCardAndPlayer(p2, c2);

            Assert.AreEqual(c1, trick.LeadingCard());
            Assert.AreNotEqual(c2, trick.LeadingCard());
        }

        [TestMethod]
        public void TheWinnerCardTest()
        {
            trick = new Trick();
            trick.AddCardAndPlayer(p1, c1);
            trick.AddCardAndPlayer(p2, c2);
            trick.AddCardAndPlayer(p3, c3);
            trick.AddCardAndPlayer(p4, c4);
            trick.AddCardAndPlayer(p5, c5);
            Assert.AreEqual(c4, trick.TheWinnerCard());
        }

        [TestMethod]
        public void TheWinnerPlayerTest()
        {
            trick = new Trick();
            trick.AddCardAndPlayer(p1, c1);
            trick.AddCardAndPlayer(p2, c2);
            trick.AddCardAndPlayer(p3, c3);
            trick.AddCardAndPlayer(p4, c4);
            trick.AddCardAndPlayer(p5, c5);
            Assert.AreEqual(p4.Name, trick.TheWinnerPlayer().Name);

        }

        [TestMethod]
        public void TrickValueTest()
        {
            trick = new Trick();
            trick.AddCardAndPlayer(p1, c1);
            trick.AddCardAndPlayer(p2, c2);
            trick.AddCardAndPlayer(p3, c3);
            trick.AddCardAndPlayer(p4, c4);
            trick.AddCardAndPlayer(p5, c5);
            Assert.AreEqual(17, trick.TrickValue());

        }

    }
}
