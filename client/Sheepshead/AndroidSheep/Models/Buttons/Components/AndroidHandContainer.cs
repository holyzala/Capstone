using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SharedSheep.Player;

namespace AndroidSheep.Models.Buttons.Components
{
    class AndroidHandContainer
    {
        private List<AndroidCard> _cardsInHand;
        private IPlayer _player;
        private readonly List<Vector2> _cardPositions;
        public Vector2 Position { get; set; }
        
        public AndroidHandContainer(List<AndroidCard> cardsInHand, IPlayer player)
        {
            _cardsInHand = cardsInHand;
            SetCardPositions();
            _player = player;
            _cardPositions = new List<Vector2>();
        }
        
        private void SetCardPositions()
        {
            _cardPositions.Add(new Vector2(-500, 0));
            _cardPositions.Add(new Vector2(-150, 0));
            _cardPositions.Add(new Vector2(0, 0));
            _cardPositions.Add(new Vector2(250, 0));
            _cardPositions.Add(new Vector2(500, 0));
        }
    }
}