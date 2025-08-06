using System;
using System.Collections.Generic;
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
        private readonly History _history;
        
        private List<Deck> _decks;
        private Queue<Card> _cards;
        
        private Queue<Card> _undoCash = new Queue<Card>();

        private int _undoCount;
        
        public Table(GameConfig config)
        {
            _config = config ?? throw new ArgumentNullException(nameof(config));
            _decks = new List<Deck>();
            _history = new History();
        }

        public event Action<Card> CreateEvent;
        public event Action ShowEndEvent;
        
        public IReadOnlyList<Deck> Decks => _decks;
        
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
            
            foreach (Deck deck in _decks)
                deck.Clean();
            _cards.Clear();
            _history.Clear();
            _undoCash.Clear();

            for (int i = 0; i < 2; i++)
                CreateCard();
        }

        public void OnUndo()
        {
            if (_history.TryGet(out var cardState))
            {
                Card card = new Card(cardState.ID, cardState.CardWeight);
                _decks[cardState.ID].Undo();
                RefreshCard(card);
            }   
        }

        private void AddToDeck(int deckID)
        {
            Card cardTop = _cards.Peek();
            if(_decks[deckID].AddCard(ref cardTop))
            {
                int value = _decks[deckID].TopCard.Weight.Value;
                if (_cardValues.Contains(value) == false)
                    _cardValues.Add(value);
                
                _cards.Dequeue();
                CreateCard();
                
                CheckDeck(deckID);
                _history.AddTo(deckID, cardTop.Weight.Value);
            }
        }

        private void RefreshCard(Card card)
        {
            if (_cards.Count >= 2)
            {
                Card cardTop = _cards.Dequeue();
                cardTop.ChangeState(Card.State.Destroyed);
                
                Card cardToCash = _cards.Dequeue();
                cardToCash.ChangeState(Card.State.Destroyed);
                
                _undoCash.Enqueue(cardToCash);

                cardTop.ChangeState(Card.State.Created);
                card.ChangeState(Card.State.Top);

                _cards.Enqueue(card);
                _cards.Enqueue(cardTop);
                CreateEvent?.Invoke(card);
                CreateEvent?.Invoke(cardTop);
            }
        }
        
        private void CreateCard()
        {
            if (_cards.Count > 0)
                _cards.Peek().ChangeState(Card.State.Top);
            Card card;
            if (_undoCash.Count > 0)
            {
                card = _undoCash.Dequeue();
                card.ChangeState(Card.State.Created);
            }
            else
            {
                int index = _random.Next(0, _cardValues.Count);
                int cardID = _random.Next(0, _config.Cards.Length);
                card = new Card(cardID, _cardValues[index]);
            }
        
            _cards.Enqueue(card);
            CreateEvent?.Invoke(card);
        }

        public void CheckDeck(int id)
        {
            if(_decks[id].AmountOfCards >= 10)
                ShowEndEvent?.Invoke();
        }
        
        public void RemoveCard()
        {
            _cards.Dequeue().ChangeState(Card.State.Destroyed);
            CreateCard();
        }
    }
}