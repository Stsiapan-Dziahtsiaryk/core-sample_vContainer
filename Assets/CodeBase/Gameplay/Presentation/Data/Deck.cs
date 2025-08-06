using System;
using System.Collections.Generic;
using System.Linq;

namespace Gameplay.Presentation.Data
{
    public class Deck
    {
        public readonly int ID;
        public readonly int MaxSize;

        private Stack<Card> _deck = new Stack<Card>();
        private DeckHistory _history;

        public Deck(int id, int maxSize)
        {
            ID = id;
            _history = new DeckHistory();
            MaxSize = maxSize;
        }

        public event Action<int> AddEvent;
        public event Action<Card[]> UpdateViewEvent;
        public event Action<int> AddScoreEvent;

        public Card TopCard { get; private set; }
        public int AmountOfCards => _deck.Count;

        public bool AddCard(ref Card card)
        {
            if (card.ID == ID)
            {
                card.ChangeState(Card.State.Destroyed);
                if (TopCard != null && TopCard.Weight.Value == card.Weight.Value)
                {
                    if (_deck.Count > 0)
                        _history.AddTo(_deck);
                    
                    TopCard.UpdateWeight();
                    AddScoreEvent?.Invoke(TopCard.Weight.Value);
                    card.ChangeState(Card.State.Cash);
                    CheckInDeep();
                }
                else
                {
                    if (_deck.Count > 0)
                        _history.AddTo(_deck);
                    _deck.Push(card);
                }

                UpdateViewEvent?.Invoke(_deck.ToArray());

                TopCard = _deck.Peek();
            }

            return card.ID == ID;
        }

        public void OnAdd()
        {
            AddEvent?.Invoke(ID);
        }

        public void Clean()
        {
            _deck.Clear();
            TopCard = null;
        }

        public void Undo()
        {
            _deck.Clear();
            TopCard = null;
            
            if (_history.TryGet(out CardDto[] cards))
            {
                for (var i = cards.Length - 1; i >= 0; i--)
                {
                    Card card = new Card(cards[i].ID, cards[i].CardWeight);
                    _deck.Push(card);
                }
            }
            if(_deck.Count > 0)
                TopCard = _deck.Peek();
            
            UpdateViewEvent?.Invoke(_deck.ToArray());
        }

        private void CheckInDeep()
        {
            if (_deck.Count <= 1) return;
            var cardTop = _deck.Pop();
            var cardNext = _deck.Peek();
            if (cardTop.Weight.Value == cardNext.Weight.Value)
            {
                cardNext.UpdateWeight();
                AddScoreEvent?.Invoke(cardNext.Weight.Value);
                cardTop.ChangeState(Card.State.Cash);
                CheckInDeep();
            }
            else
            {
                _deck.Push(cardTop);
            }
        }
    }
}