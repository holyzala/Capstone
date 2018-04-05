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
        private bool isVisible;
        private IPlayer _player;
        private List<Vector2> _cardPositions;
        public Vector2 Position { get; set; }
        
        public AndroidHandContainer(List<AndroidCard> cardsInHand, IPlayer player)
        {
            _cardsInHand = cardsInHand;
            SetCardPositions();
            _player = player;
        }
        
        private void SetCardPositions()
        {
            _cardPositions.Add(new Vector2(-300, 0));
            _cardPositions.Add(new Vector2(-150, 0));
            _cardPositions.Add(new Vector2(0, 0));
            _cardPositions.Add(new Vector2(150, 0));
            _cardPositions.Add(new Vector2(300, 0));
        }
    }
}