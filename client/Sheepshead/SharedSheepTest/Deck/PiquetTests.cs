using System;
using System.Collections.Generic;
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
            Card card = new Card(CardID.Eight, CardPower.JackDiamond, Suit.Hearts);
            Card card2 = new Card(CardID.Jack, CardPower.JackClub, Suit.Clubs);
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

            List<ICard> Cards = new List<ICard>
            {
                new Card(CardID.Seven, CardPower.SevenTrump, Suit.Diamond),
                new Card(CardID.Seven, CardPower.SevenFail, Suit.Hearts),
                new Card(CardID.Seven, CardPower.SevenFail, Suit.Spades),
                new Card(CardID.Seven, CardPower.SevenFail, Suit.Clubs),
                new Card(CardID.Eight, CardPower.EightTrump, Suit.Diamond),
                new Card(CardID.Eight, CardPower.EightFail, Suit.Hearts),
                new Card(CardID.Eight, CardPower.EightFail, Suit.Spades),
                new Card(CardID.Eight, CardPower.EightFail, Suit.Clubs),
                new Card(CardID.Nine, CardPower.NineTrump, Suit.Diamond),
                new Card(CardID.Nine, CardPower.NineFail, Suit.Hearts),
                new Card(CardID.Nine, CardPower.NineFail, Suit.Spades),
                new Card(CardID.Nine, CardPower.NineFail, Suit.Clubs),
                new Card(CardID.King, CardPower.KingTrump, Suit.Diamond),
                new Card(CardID.King, CardPower.KingFail, Suit.Hearts),
                new Card(CardID.King, CardPower.KingFail, Suit.Spades),
                new Card(CardID.King, CardPower.KingFail, Suit.Clubs),
                new Card(CardID.Ten, CardPower.TenTrump, Suit.Diamond),
                new Card(CardID.Ten, CardPower.TenFail, Suit.Hearts),
                new Card(CardID.Ten, CardPower.TenFail, Suit.Spades),
                new Card(CardID.Ten, CardPower.TenFail, Suit.Clubs),
                new Card(CardID.Ace, CardPower.AceTrump, Suit.Diamond),
                new Card(CardID.Ace, CardPower.AceFail, Suit.Hearts),
                new Card(CardID.Ace, CardPower.AceFail, Suit.Spades),
                new Card(CardID.Ace, CardPower.AceFail, Suit.Clubs),
                new Card(CardID.Jack, CardPower.JackDiamond, Suit.Diamond),
                new Card(CardID.Jack, CardPower.JackHeart, Suit.Hearts),
                new Card(CardID.Jack, CardPower.JackSpade, Suit.Spades),
                new Card(CardID.Jack, CardPower.JackClub, Suit.Clubs),
                new Card(CardID.Queen, CardPower.QueenDiamond, Suit.Diamond),
                new Card(CardID.Queen, CardPower.QueenHeart, Suit.Hearts),
                new Card(CardID.Queen, CardPower.QueenSpade, Suit.Spades),
                new Card(CardID.Queen, CardPower.QueenClub, Suit.Clubs)
            };

            int i = 0;
            foreach (ICard card in Cards)
            {
                Assert.AreEqual(deck.Cards[i++], card);
            }
        }
    }
}
