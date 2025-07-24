using CodeBase.Shared.StaticData;

namespace Infrastructure.StateMachine
{
    public interface IAppStateMachine
    {
        void Enter(AppState type);
        void EnterAsync(AppState type);
    }
}