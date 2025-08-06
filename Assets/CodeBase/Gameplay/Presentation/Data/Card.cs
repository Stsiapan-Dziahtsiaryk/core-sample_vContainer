using System.Collections.Generic;
using R3;

namespace Gameplay.Presentation.Data
{
    public class Card
    {
        public readonly int ID;
        public readonly ReactiveProperty<int> Weight;
        private readonly ReactiveProperty<State> _state;

        private readonly Stack<int> _weightHistory;
        private bool _isDirty;

        public Card(int id, int weight)
        {
            ID = id;
            Weight = new ReactiveProperty<int>(weight);
            _state = new ReactiveProperty<State>(State.Invalid);
            _weightHistory = new Stack<int>();
            _isDirty = false;
        }

        public ReactiveProperty<State> StateProperty => _state;
        public bool IsDirty => _isDirty;

        public void UpdateWeight()
        {
            _weightHistory.Push(Weight.Value);
            Weight.Value *= 2;
            _isDirty = true;
        }

        public void UpdateWeight(int weight)
        {
            Weight.Value = weight;
        }

        public bool TryUndoWeight(int compareWeight)
        {
            if (_weightHistory.Count > 0)
            {
                int weight = _weightHistory.Peek();

                Weight.Value /= 2;
                _weightHistory.Pop();
                _isDirty = _weightHistory.Count > 0;
                return Weight.Value == compareWeight;
            }

            _isDirty = _weightHistory.Count > 0;
            return false;
        }

        public void ChangeState(State newState)
        {
            _state.Value = newState;
        }

        public CardDto GetDto() => new CardDto(ID, Weight.Value);
        
        public enum State
        {
            Invalid = 0,
            Created = 1,
            Top = 2,
            Destroyed = 3,
            Cash = 4,
            OnDeck = 5,
        }
    }
}