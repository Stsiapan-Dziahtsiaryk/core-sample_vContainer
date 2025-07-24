using CodeBase.Shared.StaticData;
using VContainer;

namespace Infrastructure.StateMachine
{
    public interface IStateResolver
    {
        void Initialize(IObjectResolver resolver);
        void AddState(AppState type, IExitableState state);
    }
}