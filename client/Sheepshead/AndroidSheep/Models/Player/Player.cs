using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input.Touch;

namespace AndroidSheep.Models.Player
{
    public class Player
    {
        public Player(string playername, int orderintable, Vector3 position)
        {
            this.PlayerName = playername;
            this.OrderInTable = orderintable;
            this.Position = position;
        }
        
        string PlayerName { get; set; }
        Vector3 Position { get; }
        int OrderInTable { get; }
        PlayerCamera Camera { get; set; }
        Card[] Cards { get; }

        public void SetPlayerCards(Card[] cards)
        {
            for(int i = 0; i < cards.Length; i++)
            {
                if (cards[i] != null)
                {
                    this.Cards[i] = cards[i];
                }
            }
        }
    }
}