using R3;

namespace Gameplay.Presentation.Data
{
    public class Game
    {
        private readonly ReactiveProperty<int> _score;

        public Game()
        {
            _score = new ReactiveProperty<int>(0);
        }

        public ReadOnlyReactiveProperty<int> Score => _score;

        public void HandleNewGame()
        {
            _score.Value = 0;
        }
        
        public void IncrementScore(int value)
        {
            _score.Value += value;
        }

        public string GetScore()
        {
            

            return "";
        }
    }
}