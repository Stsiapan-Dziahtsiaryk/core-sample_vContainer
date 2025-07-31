using System;
using System.Collections.Generic;
using System.Linq;
using Gameplay.Presentation.Data.Configs;

namespace Gameplay.Presentation.Data
{
    public class Table
    {
        private readonly GameConfig _config;

        private readonly List<int> _cardValues = new List<int>()
        {
            2, 4, 8, 16
        };

        private readonly Random _random = new Random();
        
        private List<Deck> _decks;
        private Queue<Card> _cards;

        public Table(GameConfig config)
        {
            _config = config ?? throw new ArgumentNullException(nameof(config));
            _decks = new List<Deck>();
        }

        public event Action<Card> CreateEvent;

        public IReadOnlyList<Deck> Decks => _decks;
        public IReadOnlyList<Card> Cards => _cards.ToList();
        public Card CurrentTopCard { get; private set; }

        public void Initialize()
        {
            for (int i = 0; i < _config.Cards.Length; i++)
            {
                Deck deck = new Deck(i, _config.MaxDeckSize);
                deck.AddEvent += AddToDeck;
                
                _decks.Add(deck);
            }

            _cards = new Queue<Card>();
        }

        public void HandleNewGame()
        {
            if(_cardValues.Count > 4)
                _cardValues.RemoveRange(4, _cardValues.Count - 4);
            
            for (int i = 0; i < 2; i++)
                CreateCard();
        }

        private void AddToDeck(int deckID)
        {
            Card cardTop = _cards.Dequeue();
            if(_decks[deckID].AddCard(ref cardTop))
            {
                int value = _decks[deckID].TopCard.Weight.Value;
                if (_cardValues.Contains(value) == false)
                    _cardValues.Add(value);

                CreateCard();
            }
            else
            {
                _cards.Enqueue(cardTop);
            }
        }

        private void CreateCard()
        {
            if (_cards.Count > 0)
                _cards.Peek().SetToTop();

            int index = _random.Next(0, _cardValues.Count);
            int cardID = _random.Next(0, _config.Cards.Length);
            Card card = new Card(cardID, _cardValues[index]);
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