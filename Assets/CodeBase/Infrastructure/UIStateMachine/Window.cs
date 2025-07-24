using System;
using CodeBase.Shared;

namespace Infrastructure.UIStateMachine
{
    public class Window : IWindow
    {
        private readonly WindowType _window;

        public Window(WindowType window)
        {
            _window = window;
        }

        public event Action<WindowType> Opened;
        public event Action<WindowType> Closed;
        public event Action InvokeOpen;
        public event Action InvokeClose;

        public void Open()
        {
            InvokeOpen?.Invoke();
            Opened?.Invoke(_window);
        }
        public void Close()
        {
            InvokeClose?.Invoke();
            Closed?.Invoke(_window);
        }
    }
}