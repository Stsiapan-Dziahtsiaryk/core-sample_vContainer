using System;
using System.Collections.Generic;

namespace Gameplay.Presentation.Data
{
    public class Table
    {
        private List<Deck> _decks;
        
        public Table()
        {
            _decks = new List<Deck>();
        }

        public event Action HandleNewGameEvent;

        public IReadOnlyList<Deck> Decks => _decks;
        public List<Card> Cards { get; private set; } 
        public Card CurrentTopCard { get; private set; }
        
        public void OnHandleNewGame()
        {
            for (int i = 0; i < 4; i++)
            {
                Deck deck = new Deck();
                _decks.Add(deck);
            }
            
            Cards = new List<Card>();
            Cards.Add(new Card(2));
            Cards.Add(new Card(4));
            
            HandleNewGameEvent?.Invoke();
        }

        
        
    }
}