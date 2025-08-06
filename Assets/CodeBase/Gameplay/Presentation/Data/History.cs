using System.Collections.Generic;

namespace Gameplay.Presentation.Data
{
    public class History
    {
        private readonly int _capacity;
        
        private readonly Stack<CardDto> _history = new Stack<CardDto>();

        public History(int capacity = 3)
        {
            _capacity = capacity;
        }

        public void AddTo(int deckId, int cardWeight)
        {
            CardDto lastCard = new CardDto(deckId, cardWeight);
            CheckingHistory();
            _history.Push(lastCard);
        }

        public bool TryGet(out CardDto cardHistoryDto) 
            => _history.TryPop(out cardHistoryDto);

        public void Clear() => _history.Clear();

        private void CheckingHistory()
        {
            if(_history.Count >= _capacity)
            {
                CardDto[] tempArray = new CardDto[_capacity];
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