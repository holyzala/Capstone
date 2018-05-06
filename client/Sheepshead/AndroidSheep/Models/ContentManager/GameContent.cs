using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using SharedSheep.Card;
using System.Linq;
using System;

namespace AndroidSheep.Models
{
    public class GameContent
    {
        public List<ICard> Cards { get; set; }
        public List<Texture2D> CardGraphics { get; set; }
        public Dictionary<ICard, Texture2D> TextureDict;

        public Texture2D QueenOfSpades { get; set; }
        public Texture2D KingOfSpades { get; set; }
        public Texture2D JackOfSpades { get; set; }
        public Texture2D AceOfSpades { get; set; }
        public Texture2D TenOfSpades { get; set; }
        public Texture2D NineOfSpades { get; set; }
        public Texture2D EightOfSpades { get; set; }
        public Texture2D SevenOfSpades { get; set; }
        public Texture2D SixOfSpades { get; set; }

        public Texture2D QueenOfHearts { get; set; }
        public Texture2D KingOfHearts { get; set; }
        public Texture2D JackOfHearts { get; set; }
        public Texture2D AceOfHearts { get; set; }
        public Texture2D TenOfHearts { get; set; }
        public Texture2D NineOfHearts { get; set; }
        public Texture2D EightOfHearts { get; set; }
        public Texture2D SevenOfHearts { get; set; }
        public Texture2D SixOfHearts { get; set; }

        public Texture2D QueenOfClubs { get; set; }
        public Texture2D KingOfClubs { get; set; }
        public Texture2D JackOfClubs { get; set; }
        public Texture2D AceOfClubs { get; set; }
        public Texture2D TenOfClubs { get; set; }
        public Texture2D NineOfClubs { get; set; }
        public Texture2D EightOfClubs { get; set; }
        public Texture2D SevenOfClubs { get; set; }
        public Texture2D SixOfClubs { get; set; }

        public Texture2D QueenOfDiamonds { get; set; }
        public Texture2D KingOfDiamonds { get; set; }
        public Texture2D JackOfDiamonds { get; set; }
        public Texture2D AceOfDiamonds { get; set; }
        public Texture2D TenOfDiamonds { get; set; }
        public Texture2D NineOfDiamonds { get; set; }
        public Texture2D EightOfDiamonds { get; set; }
        public Texture2D SevenOfDiamonds { get; set; }
        public Texture2D SixOfDiamonds { get; set; }

        public Texture2D TableTop { get; set; }
        public SpriteFont Font { get; set; }
        public SpriteFont FinalScoreFont { get; set; }

        public Texture2D Button { get; set; }
        public Texture2D Button2 { get; set; }
        public Texture2D Button3 { get; set; }
        public Texture2D Button4 { get; set; }

        public Texture2D LeaderboardPanel { get; set; }
        public Texture2D Logo { get; set; }
        public Texture2D Ranks { get; set; }
        public Texture2D PickWords { get; set; }
        public GameContent(ContentManager content)
        {
            TextureDict = new Dictionary<ICard, Texture2D>();
            Cards = new List<ICard>();
            CardGraphics = new List<Texture2D>();

            SetAllCards();

            CardGraphics.Add(SevenOfDiamonds = content.Load<Texture2D>("Diamonds/7_of_diamonds"));
            CardGraphics.Add(SevenOfHearts = content.Load<Texture2D>("Hearts/7_of_hearts"));
            CardGraphics.Add(SevenOfSpades = content.Load<Texture2D>("Spades/7_of_spades"));
            CardGraphics.Add(SevenOfClubs = content.Load<Texture2D>("Clubs/7_of_clubs"));

            CardGraphics.Add(EightOfDiamonds = content.Load<Texture2D>("Diamonds/8_of_diamonds"));
            CardGraphics.Add(EightOfHearts = content.Load<Texture2D>("Hearts/8_of_hearts"));
            CardGraphics.Add(EightOfSpades = content.Load<Texture2D>("Spades/8_of_spades"));
            CardGraphics.Add(EightOfClubs = content.Load<Texture2D>("Clubs/8_of_clubs"));

            CardGraphics.Add(NineOfDiamonds = content.Load<Texture2D>("Diamonds/9_of_diamonds"));
            CardGraphics.Add(NineOfHearts = content.Load<Texture2D>("Hearts/9_of_hearts"));
            CardGraphics.Add(NineOfSpades = content.Load<Texture2D>("Spades/9_of_spades"));
            CardGraphics.Add(NineOfClubs = content.Load<Texture2D>("Clubs/9_of_clubs"));

            CardGraphics.Add(KingOfDiamonds = content.Load<Texture2D>("Diamonds/king_of_diamonds2"));
            CardGraphics.Add(KingOfHearts = content.Load<Texture2D>("Hearts/king_of_hearts2"));
            CardGraphics.Add(KingOfSpades = content.Load<Texture2D>("Spades/king_of_spades2"));
            CardGraphics.Add(KingOfClubs = content.Load<Texture2D>("Clubs/king_of_clubs2"));

            CardGraphics.Add(TenOfDiamonds = content.Load<Texture2D>("Diamonds/10_of_diamonds"));
            CardGraphics.Add(TenOfHearts = content.Load<Texture2D>("Hearts/10_of_hearts"));
            CardGraphics.Add(TenOfSpades = content.Load<Texture2D>("Spades/10_of_spades"));
            CardGraphics.Add(TenOfClubs = content.Load<Texture2D>("Clubs/10_of_clubs"));

            CardGraphics.Add(AceOfDiamonds = content.Load<Texture2D>("Diamonds/ace_of_diamonds"));
            CardGraphics.Add(AceOfHearts = content.Load<Texture2D>("Hearts/ace_of_hearts"));
            CardGraphics.Add(AceOfSpades = content.Load<Texture2D>("Spades/ace_of_spades"));
            CardGraphics.Add(AceOfClubs = content.Load<Texture2D>("Clubs/ace_of_clubs"));

            CardGraphics.Add(JackOfDiamonds = content.Load<Texture2D>("Diamonds/jack_of_diamonds2"));
            CardGraphics.Add(JackOfHearts = content.Load<Texture2D>("Hearts/jack_of_hearts2"));
            CardGraphics.Add(JackOfSpades = content.Load<Texture2D>("Spades/jack_of_spades2"));
            CardGraphics.Add(JackOfClubs = content.Load<Texture2D>("Clubs/jack_of_clubs2"));

            CardGraphics.Add(QueenOfDiamonds = content.Load<Texture2D>("Diamonds/queen_of_diamonds2"));
            CardGraphics.Add(QueenOfHearts = content.Load<Texture2D>("Hearts/queen_of_hearts2"));
            CardGraphics.Add(QueenOfSpades = content.Load<Texture2D>("Spades/queen_of_spades2"));
            CardGraphics.Add(QueenOfClubs = content.Load<Texture2D>("Clubs/queen_of_clubs2"));

            for(var i = 0; i < Cards.Count; i++)
            {
                TextureDict.Add(Cards[i], CardGraphics[i]);
            }

            TableTop = content.Load<Texture2D>("Table/darktexture");
            Button = content.Load<Texture2D>("Buttons/button");
            Button2 = content.Load<Texture2D>("Buttons/button2");
            Button3 = content.Load<Texture2D>("Buttons/button3");
            Button4 = content.Load<Texture2D>("Buttons/button4");
            Ranks = content.Load<Texture2D>("Table/ranks");
            Font = content.Load<SpriteFont>("Fonts/Font");
            FinalScoreFont = content.Load<SpriteFont>("Fonts/FinalScoreFont");
            PickWords = content.Load<Texture2D>("Table/pickwords");
            LeaderboardPanel = content.Load<Texture2D>("Buttons/leaderboardpanel");
            Logo = content.Load<Texture2D>("Table/logo");
        }

        private void SetAllCards()
        {
            var pow = CardPower.SevenFail;
            foreach (CardID cID in Enum.GetValues(typeof(CardID)))
            {
                switch (cID)
                {
                    case CardID.Jack:
                        pow = CardPower.JackDiamond;
                        break;
                    case CardID.Queen:
                        pow = CardPower.QueenDiamond;
                        break;
                }

                foreach (Suit suit in Enum.GetValues(typeof(Suit)))
                {
                    switch (cID)
                    {
                        case CardID.Jack:
                            Cards.Add(new Card(cID, pow, suit));
                            ++pow;
                            break;

                        case CardID.Queen:
                            Cards.Add(new Card(cID, pow, suit));
                            ++pow;
                            break;

                        default:
                            Cards.Add(suit == Suit.Diamond ? new Card(cID, pow + 6, suit) : new Card(cID, pow, suit));
                            break;
                    }
                }
                ++pow;
            }
        }
    }
}