using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using AndroidSheep.Models;
namespace AndroidSheep.Models.States
{
    public abstract class AndroidState
    {
        #region Fields
        protected GameContent GameContent;
        protected GraphicsDevice GraphicsDevice;
        protected AndroidSheepGame Table;
        #endregion

        #region Methods
        public abstract void Draw(GameTime gameTime, SpriteBatch spriteBatch);

        protected AndroidState(AndroidSheepGame table, GraphicsDevice graphicsDevice, GameContent gameContent)
        {
            Table = table;
            GraphicsDevice = graphicsDevice;
            GameContent = gameContent;
        }
        public abstract void Update(GameTime gameTime);
        #endregion
    }
}