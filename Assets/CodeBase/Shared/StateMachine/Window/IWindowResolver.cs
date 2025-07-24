using CodeBase.Shared;

namespace Infrastructure.UIStateMachine
{
    public interface IWindowResolver
    {
        IWindow Resolve(WindowType type);
        void Registering(WindowType type, IWindow window);
        void CleanUp();
    }
}