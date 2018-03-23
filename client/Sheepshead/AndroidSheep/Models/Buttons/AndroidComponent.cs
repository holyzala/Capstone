using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace AndroidSheep.Models.Buttons
{
    public abstract class AndroidComponent
    {
        public abstract void Draw(GameTime gameTime, SpriteBatch spriteBatch);

        public abstract void Update(GameTime gameTime);
    }
}