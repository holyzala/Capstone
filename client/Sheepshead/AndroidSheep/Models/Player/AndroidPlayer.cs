using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using AndroidSheep.Models.Buttons;
using SharedSheep.Player;

namespace AndroidSheep.Models.Player
{
    public class AndroidPlayer
    {
        private IPlayer _player;
        public AndroidCard[] playableCards;
        public AndroidCard[] playedCards;
        public Prompt playerPrompt;

        public AndroidPlayer(IPlayer player)
        {
            _player = player;
        }
    }
}