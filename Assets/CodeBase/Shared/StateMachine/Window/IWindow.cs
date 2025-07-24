using System;
using CodeBase.Shared;

namespace Infrastructure.UIStateMachine
{
    public interface IWindow
    {
        event Action<WindowType> Opened;
        event Action<WindowType> Closed;
        
        event Action InvokeOpen;
        event Action InvokeClose;
        
        void Open();
        void Close();
    }
}