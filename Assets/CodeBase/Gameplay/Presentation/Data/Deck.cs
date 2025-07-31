using System;
using System.Collections.Generic;

namespace Gameplay.Presentation.Data
{
    public class Deck
    {
        public readonly int ID;
        
        private readonly int _maxSize;
        private Stack<Card> _deck = new Stack<Card>();

        public Deck(int id)
        {
            ID = id;
        }

        public event Action<int> AddEvent;
        public Card TopCard { get; private set; }

        public int AddCard(ref Card card)
        {
            if (TopCard != null && TopCard.Weight.Value == card.Weight.Value)
            {
                TopCard.UpdateWeight();
                card.DestroyState();
                CheckInDeep();
            }
            else
            {
                _deck.Push(card);
            }

            TopCard = _deck.Peek();
            
            return TopCard.Weight.Value;
        }

        public void OnAdd()
        {
            AddEvent?.Invoke(ID);
        }

        private void CheckInDeep()
        {
            if(_deck.Count <= 1) return;
            var cardTop = _deck.Pop();
            var cardNext = _deck.Peek();
            if (cardTop.Weight.Value == cardNext.Weight.Value)
            {
                cardNext.UpdateWeight();
                cardTop.DestroyState();
                CheckInDeep();
            }
            else
                _deck.Push(cardTop);
        }
    }
}