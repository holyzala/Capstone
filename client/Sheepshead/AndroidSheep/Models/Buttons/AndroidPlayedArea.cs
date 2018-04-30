using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AndroidSheep.Models.Buttons
{
    public class AndroidPlayedArea : AndroidComponent
    {
        public AndroidCard[] PlayedCards;
        private readonly Vector2[] _playedVectors;
        private readonly int _screenHeight;
        private readonly int _screenWidth;
        public int NumCards;

        public AndroidPlayedArea(int screenWidth, int screenHeight)
        {
            PlayedCards = new AndroidCard[5];
            _playedVectors = new Vector2[5];
            _screenHeight = screenHeight;
            _screenWidth = screenWidth;
            NumCards = 0;
            this.SetVectors();
        }

        private void SetVectors()
        {
            _playedVectors[0] = new Vector2((_screenWidth / 2.0f) - 200, _screenHeight / 3.0f);
            _playedVectors[1] = new Vector2((_screenWidth / 2.0f) - 100, _screenHeight / 3.0f);
            _playedVectors[2] = new Vector2((_screenWidth / 2.0f), _screenHeight / 3.0f);
            _playedVectors[3] = new Vector2((_screenWidth / 2.0f) + 100, _screenHeight / 3.0f);
            _playedVectors[4] = new Vector2((_screenWidth / 2.0f) + 200, _screenHeight / 3.0f);
        }
        
        public AndroidCard[] AddCardToPlayArea(AndroidCard card)
        {
            if (NumCards >= 6) return PlayedCards;
            PlayedCards[NumCards] = card;
            NumCards++;
            return PlayedCards;
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if (NumCards == 0)
                return;
            var scale = 0.25f;
            for (var i = 0; i < NumCards; i++)
            {
                var cardTexture = PlayedCards[i].Texture;
                var position = _playedVectors[i];
                var origin = new Vector2(PlayedCards[i].Texture.Width / 2.0f, PlayedCards[i].Texture.Height / 2.0f);
                spriteBatch.Draw(cardTexture, position, null, Color.White, 0f, origin, scale, SpriteEffects.None, 1);
            }

        }

        public override void Update(GameTime gameTime)
        {
            return;
        }
    }
}