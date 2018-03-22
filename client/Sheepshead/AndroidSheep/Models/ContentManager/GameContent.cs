using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace AndroidSheep.Models
{
    class GameContent
    {
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
        public GameContent(ContentManager Content)
        {
            QueenOfSpades = Content.Load<Texture2D>("Spades/queen_of_spades2");
            KingOfSpades = Content.Load<Texture2D>("Spades/king_of_spades2");
            JackOfSpades = Content.Load<Texture2D>("Spades/jack_of_spades2");
            TenOfSpades = Content.Load<Texture2D>("Spades/10_of_spades");
            NineOfSpades = Content.Load<Texture2D>("Spades/9_of_spades");
            EightOfSpades = Content.Load<Texture2D>("Spades/8_of_spades");
            SevenOfSpades = Content.Load<Texture2D>("Spades/7_of_spades");
            SixOfSpades = Content.Load<Texture2D>("Spades/6_of_spades");

            QueenOfDiamonds = Content.Load<Texture2D>("Diamonds/queen_of_diamonds2");
            KingOfDiamonds = Content.Load<Texture2D>("Diamonds/king_of_diamonds2");
            JackOfDiamonds = Content.Load<Texture2D>("Diamonds/jack_of_diamonds2");
            TenOfDiamonds = Content.Load<Texture2D>("Diamonds/10_of_diamonds");
            NineOfDiamonds = Content.Load<Texture2D>("Diamonds/9_of_diamonds");
            EightOfDiamonds = Content.Load<Texture2D>("Diamonds/8_of_diamonds");
            SevenOfDiamonds = Content.Load<Texture2D>("Diamonds/7_of_diamonds");
            SixOfDiamonds = Content.Load<Texture2D>("Diamonds/6_of_diamonds");

            QueenOfHearts = Content.Load<Texture2D>("Hearts/queen_of_hearts2");
            KingOfHearts = Content.Load<Texture2D>("Hearts/king_of_hearts2");
            JackOfHearts = Content.Load<Texture2D>("Hearts/jack_of_hearts2");
            TenOfHearts = Content.Load<Texture2D>("Hearts/10_of_hearts");
            NineOfHearts = Content.Load<Texture2D>("Hearts/9_of_hearts");
            EightOfHearts = Content.Load<Texture2D>("Hearts/8_of_hearts");
            SevenOfHearts = Content.Load<Texture2D>("Hearts/7_of_hearts");
            SixOfHearts = Content.Load<Texture2D>("Hearts/6_of_hearts");

            QueenOfClubs = Content.Load<Texture2D>("Clubs/queen_of_clubs2");
            KingOfClubs = Content.Load<Texture2D>("Clubs/king_of_clubs2");
            JackOfClubs = Content.Load<Texture2D>("Clubs/jack_of_clubs2");
            TenOfClubs = Content.Load<Texture2D>("Clubs/10_of_clubs");
            NineOfClubs = Content.Load<Texture2D>("Clubs/9_of_clubs");
            EightOfClubs = Content.Load<Texture2D>("Clubs/8_of_clubs");
            SevenOfClubs = Content.Load<Texture2D>("Clubs/7_of_clubs");
            SixOfClubs = Content.Load<Texture2D>("Clubs/6_of_clubs");

            TableTop = Content.Load<Texture2D>("Table/darktexture");

            Font = Content.Load<SpriteFont>("Fonts/Font");
            //.Equals equatable
        }
    }
}