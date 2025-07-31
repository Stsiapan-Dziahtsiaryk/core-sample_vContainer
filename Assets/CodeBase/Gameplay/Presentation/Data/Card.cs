using R3;

namespace Gameplay.Presentation.Data
{
    public class Card
    {
        public readonly int ID;
        public readonly ReactiveProperty<int> Weight;
        
        private readonly ReactiveProperty<State> _state;

        public Card(int id, int weight)
        {
            ID = id;
            Weight = new ReactiveProperty<int>(weight);
            _state = new ReactiveProperty<State>(State.Created);
        }

        public ReactiveProperty<State> StateProperty => _state;

        public void UpdateWeight()
        {
            Weight.Value *= 2;
        }

        public void DestroyState()
        {
            _state.Value = State.Destroyed;
        }

        public void SetToTop()
        {
            _state.Value = State.Top;
        }

    public enum State
        {
            Invalid = 0,
            Created = 1,
            Top = 2,
            Destroyed = 3
        }
    }
}