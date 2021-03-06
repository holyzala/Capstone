﻿using SharedSheep.Blind;
using SharedSheep.Card;
using SharedSheep.Hand;
using SharedSheep.Round;
using System.Collections.Generic;

namespace SharedSheep.Player
{
    public abstract class AbstractPlayer : IPlayer
    {
        public IHand Hand { get; set; }
        public string Name { get; }

        protected AbstractPlayer(string name)
        {
            Name = name;
            Hand = new Hand.Hand();
        }

        public void AddToHand(ICard card)
        {
            Hand.AddCard(card);
        }

        public abstract ICard Pick(Prompt prompt, IBlind blind, bool forced, ICard partnerCard);

        public abstract ICard PlayCard(Prompt prompt, List<IRound> rounds, IPlayer picker, IBlind blind, ICard partnerCard);

        public abstract bool WantPick(Prompt prompt, List<IPlayer> players);

        public override string ToString()
        {
            return Name;
        }

        protected ICard CallUp(Prompt prompt, IBlind blind)
        {
            var callUp = new List<ICard> {
                    new Card.Card(CardID.Jack, CardPower.JackHeart, Suit.Hearts),
                    new Card.Card(CardID.Jack, CardPower.JackSpade, Suit.Spades),
                    new Card.Card(CardID.Jack, CardPower.JackClub, Suit.Clubs),
                    new Card.Card(CardID.Queen, CardPower.QueenDiamond, Suit.Diamond),
                    new Card.Card(CardID.Queen, CardPower.QueenHeart, Suit.Hearts),
                    new Card.Card(CardID.Queen, CardPower.QueenSpade, Suit.Spades),
                    new Card.Card(CardID.Queen, CardPower.QueenClub, Suit.Clubs)
                };
            foreach (var card in callUp)
            {
                if (Hand.Cards.Contains(card) || blind.BlindCards.Contains(card))
                    continue;
                prompt(PromptType.CalledUp, new Dictionary<PromptData, object>
                {
                    { PromptData.Card, card }
                });
                return card;
            }
            return null;
        }
    }
}