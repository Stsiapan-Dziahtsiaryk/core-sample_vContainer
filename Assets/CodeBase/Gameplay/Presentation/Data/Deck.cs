using System.Collections;
using System.Collections.Generic;

namespace Gameplay.Presentation.Data
{
    public class Deck
    {
        private Stack<Card> _deck = new Stack<Card>();

        public void AddCard(Card card)
        {
            // Card topCard = _deck.Peek();
            //
            // if(topCard.Weight == card.Weight)
                
            
            _deck.Push(card);
        }
    }
}