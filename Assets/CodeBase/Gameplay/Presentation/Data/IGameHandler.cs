using Gameplay.Presentation.StaticData;
using R3;

namespace Gameplay.Presentation.Data
{
    public interface IGameHandler
    {
        ReadOnlyReactiveProperty<GameState> State { get; }
        void OnHandleNewGame();
        void OnChangeGameState(GameState state);
    }
}