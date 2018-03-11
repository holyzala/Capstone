using System.Collections;
using System.Collections.Generic;
using SharedSheep.Card;

namespace SharedSheep.Blind
{
    public class Blind : IBlind
    {
        public List<ICard> BlindCards { get; private set; }

        public Blind()
        {
            BlindCards = new List<ICard>();
        }

        public void AddCard(ICard card)
        {
            if (BlindCards.Count < 2) //what if not?
                BlindCards.Add(card);
        }

        public int BlindPoints()
        {
            return BlindCards[0].Value + BlindCards[1].Value;
        }

        public ICard SwapCard(int index, ICard card)
        {
            ICard outCard = BlindCards[index];
            BlindCards[index] = card;

            return outCard;
        }

        public IEnumerator<ICard> GetEnumerator()
        {
            return BlindCards.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}