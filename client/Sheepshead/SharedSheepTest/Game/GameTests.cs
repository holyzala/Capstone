using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharedSheep.Player;
using SharedSheep.Game;
using SharedSheep.Deck;


namespace SharedSheepTest.GameTests
{
    [TestClass]
    public class GameTests
    {
        LocalPlayer p = new LocalPlayer("p1");
        Piquet deck = new Piquet();
      


        [TestMethod]
        public void DealCardTest()
        {
            Game game = new Game();
            game.DealCard(p);
            Assert.AreEqual(6, p.Hand.GetNumOfRemainingCards());
            Assert.AreNotEqual(1, p.Hand.GetNumOfRemainingCards());

        }
    }
}
