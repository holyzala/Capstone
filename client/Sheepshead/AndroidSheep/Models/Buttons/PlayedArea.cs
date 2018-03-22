using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AndroidSheep.Models.Buttons
{
    public class PlayedArea : Component
    {
        public Card[] playedCards;
        private Vector2[] _playedVectors;
        private int _screenHeight;
        private int _screenWidth;
        public int numCards;
        public PlayedArea(int screenWidth, int screenHeight)
        {
            playedCards = new Card[5];
            _playedVectors = new Vector2[5];
            _screenHeight = screenHeight;
            _screenWidth = screenWidth;
            numCards = 0;
            this.SetVectors();
        }

        private void SetVectors()
        {
            _playedVectors[0] = new Vector2(_screenWidth / 2 - 200, _screenHeight / 3);
            _playedVectors[1] = new Vector2(_screenWidth / 2 - 100, _screenHeight / 3);
            _playedVectors[2] = new Vector2(_screenWidth / 2, _screenHeight / 3);
            _playedVectors[3] = new Vector2(_screenWidth / 2 + 100, _screenHeight / 3);
            _playedVectors[4] = new Vector2(_screenWidth / 2 + 200, _screenHeight / 3);
        }

        public Card[] AddCardToPlayArea(Card card)
        {
            if(numCards < 6)
            {
                playedCards[numCards] = card;
                numCards++;
            }
            return playedCards;
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch, int height, int width)
        {
            if (numCards == 0)
                return;
            for(int i = 0; i < numCards; i++)
            {
                Texture2D cardTexture = playedCards[i].texture;
                Vector2 position = _playedVectors[i];
                Vector2 origin = new Vector2(playedCards[i].texture.Width / 2, playedCards[i].texture.Height / 2);
                float scale = 0.25f;
                spriteBatch.Draw(cardTexture, position, null, Color.White, 0f, origin, scale, SpriteEffects.None, 1);
            }
                        
        }

        public override void Update(GameTime gameTime)
        {
            throw new NotImplementedException();
        }
    }
}