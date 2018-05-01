using AndroidSheep.Models.Buttons;
using SharedSheep.Player;

namespace AndroidSheep.Models.Player
{
    public class AndroidPlayer
    {
        private IPlayer _player;
        public AndroidCard[] PlayableCards;
        public AndroidCard[] PlayedCards;
        public Prompt PlayerPrompt;
        private int _handCount;
        public bool ThisPlayerTurn;

        public AndroidPlayer(IPlayer player)
        {
            _player = player;
            _handCount = 0;
            PlayableCards = new AndroidCard[6];
            ThisPlayerTurn = false;
        }

        public void AddCardToHand(AndroidCard card)
        {
            if (card == null) return;
            PlayableCards[_handCount] = card;
            _handCount++;
        }
    }
}