using System;
using CodeBase.Shared;

namespace Infrastructure.UIStateMachine
{
    public interface IWindowFsm
    {
        event Action<WindowType> Opened;
        event Action<WindowType> Closed;

        WindowType Current { get; }

        /// <summary>
        /// Open UI Window
        /// </summary>
        /// <param name="window">Target window</param>
        /// <param name="inHistory">It has default value (false). When It is a true opening window will be written to a history</param>
        void Open(WindowType window, bool inHistory = false);
        void ReOpen(WindowType window, bool inHistory = false);
        void Close(WindowType window);
        void Close();
        void CleanUpHistory();
    }
}