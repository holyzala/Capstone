using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input.Touch;
using System.Diagnostics;

namespace AndroidSheep.Models
{
    internal interface IPlayer
    {
        PlayerCamera Camera { get; set; }
        BasicEffect Effect { get; set; }
        GraphicsDeviceManager Graphics { get; set; }
        //Cards

        //Later
        //
    }
}