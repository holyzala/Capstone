using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SharedSheep.Player;
using SharedSheep.ScoreSheet;
using SharedSheep.Trick;

namespace AndroidSheep.Models.Buttons.Components
{
    public class AndroidLeaderBoardPanel : AndroidComponent
    {
        #region Fields
        private SpriteFont _font;
        private Texture2D _texture;
        private bool IsInputPressed;
        #endregion

        #region Properties
        public bool Clicked { get; set; }
        public Color PenColor { get; set; }
        public Vector2 Position { get; set; }
        public Color color;
        public AndroidCard card;
        public Rectangle Rectangle
        {
            get
            {
                return new Rectangle((int)Position.X, (int)Position.Y, _texture.Width / 2, _texture.Height / 2);
            }
        }
        public string Text;
        public string PlayerName;
        #endregion

        public AndroidLeaderBoardPanel(Texture2D texture, SpriteFont font)
        {
            _texture = texture;
            _font = font;
            PenColor = Color.Black;
            color = Color.White;
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, Rectangle, color);


            if (!string.IsNullOrEmpty(Text))
            {
                var x = (Rectangle.X + (Rectangle.Width / 2)) - (_font.MeasureString(Text).X / 2);
                var y = (Rectangle.Y + 40) - (_font.MeasureString(Text).Y / 2);

                spriteBatch.DrawString(_font, Text, new Vector2(x, y), PenColor);
            }

            if (string.IsNullOrEmpty(PlayerName)) return;
            {
                var x = (Rectangle.X + (Rectangle.Width / 2)) - (_font.MeasureString(PlayerName).X / 2);
                var y = (Rectangle.Y + 20) - (_font.MeasureString(PlayerName).Y / 2);

                spriteBatch.DrawString(_font, PlayerName, new Vector2(x, y), PenColor);
            }
            var cardX = (Rectangle.X + Rectangle.Width / 2) - 25;
            var cardY = (Rectangle.Y + Rectangle.Height / 2) - 15;
            spriteBatch.Draw(card._texture, new Vector2(cardX, cardY), null, Color.White, 0f,
                Vector2.Zero, 0.1f, SpriteEffects.None, 0f);
        }

        public override void Update(GameTime gameTime)
        {

        }
    }
}