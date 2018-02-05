﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharedSheep.Card;

namespace SharedSheepTest
{
    [TestClass]
    public class CardTests
    {
        [TestMethod]
        public void IsTrump()
        {
            ICard card1 = new Card(CardID.Eight, 0, CardPower.EightFail, Suit.Clubs);
            Assert.IsFalse(card1.IsTrump());
        }
    }
}