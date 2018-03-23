using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using AndroidSheep.Models;
namespace AndroidSheep.Models.States
{
    public abstract class AndroidState
    {
        #region Fields
        protected ContentManager _content;
        protected GraphicsDevice _graphicsDevice;
        protected AndroidSheepGame _table;
        #endregion

        #region Methods
        public abstract void Draw(GameTime gameTime, SpriteBatch spriteBatch);

        public abstract void PostUpdate(GameTime gameTime);

        public AndroidState(AndroidSheepGame table, GraphicsDevice graphicsDevice, ContentManager content)
        {
            _table = table;
            _graphicsDevice = graphicsDevice;
            _content = content;
        }
        public abstract void Update(GameTime gameTime);
        #endregion
    }
}