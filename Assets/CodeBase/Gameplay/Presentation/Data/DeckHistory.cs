using System.Collections.Generic;
using System.Linq;

namespace Gameplay.Presentation.Data
{
    public class DeckHistory
    {
        private readonly int _capacity;
        
        private readonly Stack<CardDto[]> _history = new Stack<CardDto[]>();

        public DeckHistory(int capacity = 3)
        {
            _capacity = capacity;
        }

        public void AddTo(Stack<Card> cards)
        {
            CardDto[] cardArray = cards.Select(x => x.GetDto()).ToArray();
            
            CheckingHistory();
            _history.Push(cardArray);
        }

        public bool TryGet(out CardDto[] cardHistoryDto) 
            => _history.TryPop(out cardHistoryDto);

        public void Clear() => _history.Clear();

        private void CheckingHistory()
        {
            if(_history.Count >= _capacity)
            {
                CardDto[][] tempArray = new CardDto[_capacity][];
                _history.CopyTo(tempArray, 0);
                _history.Clear();
                for (var i = tempArray.Length - 2; i >= 0; i--)
                {
                    _history.Push(tempArray[i]);
                }
            }
        }
    }
}