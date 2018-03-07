using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input.Touch;

namespace AndroidSheep.Models
{
    public class Player
    {
        PlayerCamera camera;
        BasicEffect effect;
        GraphicsDeviceManager graphics;
        Card[] hand;
        int handcount = 0;

        public Player(PlayerCamera camera, BasicEffect effect, GraphicsDeviceManager graphics)
        {
            this.camera = camera;
            this.effect = effect;
            this.graphics = graphics;
            hand = new Card[6];
        }

        public void SetHand(Card hand)
        {
            this.hand[handcount] = hand;
            handcount++;
        }

        public Card GetCard(int index)
        {
            return this.hand[index];
        }

        private PlayerCamera SetCamera(Vector3 cameraposition, GraphicsDeviceManager graphics)
        {
            PlayerCamera playercamera = new PlayerCamera(cameraposition, graphics);
            return playercamera;
        }

        public PlayerCamera GetCamera()
        {
            return this.camera;
        }

        
        public void Update(GameTime gameTime)
        {
            TouchCollection touchCollection = TouchPanel.GetState();
        }
    }
}