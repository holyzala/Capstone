using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharedSheep.Player;
using SharedSheep.Card;

namespace SharedSheepTest.Player
{
    [TestClass]
    public class LocalPlayerTests
    {
        [TestMethod]
        public void AddToHandTest()
        {
            IPlayer player = new LocalPlayer("Fred");
            ICard card = new Card(CardID.Eight, CardPower.EightFail, Suit.Clubs);
            player.AddToHand(card);
            Assert.AreEqual(card, player.Hand.GetCard(0));
        }
    }
}
