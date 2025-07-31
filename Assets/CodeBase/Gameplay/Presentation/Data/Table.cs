using System;
using System.Collections.Generic;
using System.Linq;

namespace Gameplay.Presentation.Data
{
    public class Table
    {
        private readonly List<int> _cardValues = new List<int>()
        {
            2, 4, 8, 16
        };
        
        private readonly Random _random = new Random();
        
        private List<Deck> _decks;
        private Queue<Card> _cards;
      
        
        public Table()
        {
            _decks = new List<Deck>();
        }

        public event Action HandleNewGameEvent;
        public event Action<Card> CreateEvent;

        public IReadOnlyList<Deck> Decks => _decks;
        public IReadOnlyList<Card> Cards => _cards.ToList();
        public Card CurrentTopCard { get; private set; }

        public void Initialize()
        {
            for (int i = 0; i < 4; i++)
            {
                Deck deck = new Deck(i);
                deck.AddEvent += AddToDeck;
                
                _decks.Add(deck);
            }
            
            _cards = new Queue<Card>();
            _cards.Enqueue(new Card(2));
            _cards.Enqueue(new Card(4));
        }
        
        public void HandleNewGame()
        {
            
            HandleNewGameEvent?.Invoke();
        }

        private void AddToDeck(int deckID)
        {
            Card cardTop = _cards.Dequeue();
            int value = _decks[deckID].AddCard(ref cardTop);
            if(_cardValues.Contains(value) == false)
                _cardValues.Add(value);
            
            CreateCard();
        }

        private void CreateCard()
        {
            _cards.Peek().SetToTop();
            
            int index = _random.Next(0, _cardValues.Count);
            Card card = new Card(_cardValues[index]);
            _cards.Enqueue(card);
            CreateEvent?.Invoke(card);
        }

        public void RemoveCard()
        {
            _cards.Dequeue().DestroyState();
            CreateCard();
        }
    }
}