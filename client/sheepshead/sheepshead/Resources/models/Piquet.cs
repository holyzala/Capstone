﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System.Security.Cryptography;


using iface = sheepshead.Resources.models.interfaces;


namespace sheepshead.Resources.models
{
    public class Piquet : iface.IDeck
    {
        public List<iface.ICard> Cards { get ; set ; }


        public Piquet()
        {
            Cards = new List<iface.ICard>();
            SetAllCards();
            Shuffle();
        }

        public iface.ICard GetTopCard()
        {
            iface.ICard topCard= null;

            for (int i = 0; i < Cards.Count; i++)
            {
                if (Cards[i] != null)
                {
                    topCard = Cards[i];
                    return topCard;
                }
            }
            return topCard;
        }


        public bool RemoveCardByIndex(int index)
        {
            if (Cards[index] == null)
                return false;
            else Cards.RemoveAt(index);
            return true;
        }

        public void ResetDeck()
        {
            SetAllCards();
        }


        public void Shuffle()
        {
            RNGCryptoServiceProvider provider = new RNGCryptoServiceProvider();
            int n = Cards.Count;
            while (n > 1)
            {
                byte[] box = new byte[1];
                do provider.GetBytes(box);
                while (!(box[0] < n * (Byte.MaxValue / n)));
                int k = (box[0] % n);
                n--;
                iface.ICard value = Cards[k];
                Cards[k] = Cards[n];
                Cards[n] = value;
            }
        }

        public int Size()
        {
            return Cards.Count;
        }


        private void SetAllCards()
        {
            foreach (CardID cID in Enum.GetValues(typeof(CardID)))
            {
                switch (cID)
                {
                    case CardID.Seven:
                        for (int i = 0; i < 4; i++)
                        {
                            if (i == 3)
                                Cards.Add(new Card(cID, 0, CardPower.SevenTrump, Suit.Trump));
                            else
                                Cards.Add(new Card(cID, 0, CardPower.SevenFail, (Suit)i + 1));
                        }
                        break;

                    case CardID.Eight:
                        for (int i = 0; i < 4; i++)
                        {
                            if (i == 3)
                                Cards.Add(new Card(cID, 0, CardPower.EightTrump, Suit.Trump));
                            else
                                Cards.Add(new Card(cID, 0, CardPower.EightFail, (Suit)i + 1));
                        }
                        break;

                    case CardID.Nine:
                        for (int i = 0; i < 4; i++)
                        {
                            if (i == 3)
                                Cards.Add(new Card(cID, 0, CardPower.NineTrump, Suit.Trump));
                            else
                                Cards.Add(new Card(cID, 0, CardPower.NineFail, (Suit)i + 1));
                        }
                        break;

                    case CardID.Ten:
                        for (int i = 0; i < 4; i++)
                        {
                            if (i == 3)
                                Cards.Add(new Card(cID, 10, CardPower.TenTrump, Suit.Trump));
                            else
                                Cards.Add(new Card(cID, 10, CardPower.TenFail, (Suit)i + 1));
                        }
                        break;

                    case CardID.Jack:
                        int pow = 13;
                        for (int i = 0; i < 4; i++)
                        {
                            Cards.Add(new Card(cID, 2, (CardPower)pow, Suit.Trump));
                            pow++;
                        }
                        break;

                    case CardID.Queen:
                        pow = 17;
                        for (int i = 0; i < 4; i++)
                        {
                            Cards.Add(new Card(cID, 3, (CardPower)pow, Suit.Trump));
                            pow++;
                        }
                        break;

                    case CardID.King:
                        for (int i = 0; i < 4; i++)
                        {
                            if (i == 3)
                                Cards.Add(new Card(cID, 4, CardPower.KingTrump, Suit.Trump));
                            else
                                Cards.Add(new Card(cID, 4, CardPower.KingFail, (Suit)i + 1));
                        }
                        break;

                    case CardID.Ace:
                        for (int i = 0; i < 4; i++)
                        {
                            if (i == 3)
                                Cards.Add(new Card(cID, 11, CardPower.AceTrump, Suit.Trump));
                            else
                                Cards.Add(new Card(cID, 11, CardPower.AceTrump, (Suit)i + 1));
                        }
                        break;
                }
            }
        }




    }
}