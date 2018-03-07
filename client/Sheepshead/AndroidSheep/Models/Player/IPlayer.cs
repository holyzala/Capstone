using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AndroidSheep.Models
{
    internal interface IPlayer
    {
        PlayerCamera Camera { get; set; }
        BasicEffect Effect { get; set; }
        GraphicsDeviceManager Graphics { get; set; }
        Card[] Hand { get; set; }
    }
}