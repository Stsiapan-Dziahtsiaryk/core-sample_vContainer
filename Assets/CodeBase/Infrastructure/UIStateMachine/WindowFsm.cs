using System;
using System.Collections.Generic;
using System.Linq;
using CodeBase.Shared;
using UnityEngine;

namespace Infrastructure.UIStateMachine
{
    public class WindowFsm : IWindowFsm, IWindowResolver
    {
        private readonly Dictionary<WindowType, IWindow> _windows = new Dictionary<WindowType, IWindow>();
        private readonly Stack<WindowType> _history = new Stack<WindowType>();

        public WindowFsm()
        {
        }

        public WindowType Current { get; private set; }

        public event Action<WindowType> Opened;
        public event Action<WindowType> Closed;

        public IWindow Resolve(WindowType type)
        {
            if (_windows.TryGetValue(type, out IWindow window))
                return window;
            else
            {
                throw new ArgumentNullException($"[App.Window]: Window type {type} is not registered! " +
                                                $"\nPlease register it first!");
            }
        }

        public void Registering(WindowType type, IWindow window)
        {
            if (_windows.TryAdd(type, window) == true)
            {
#if UNITY_EDITOR
                Debug.Log($"Window {type} was registered successfully");
#endif
            }
            else
            {
#if UNITY_EDITOR
                Debug.LogError($"Window {type} was already registered");
#endif
            }
        }

        public void CleanUp()
        {
            if (_windows.Any() == false) return;
            _windows.Clear();
        }

        public void Open(WindowType window, bool inHistory = false)
        {
            if (inHistory)
                WritingToHistory(window);
            Opening(window);
        }
        
        public void ReOpen(WindowType window, bool inHistory = false)
        {
            if (inHistory)
                WritingToHistory(window);
            if(Current == window)
                _windows[Current]?.Close();
            _windows[Current]?.Open();
        }

        public void Close(WindowType window)
        {
            if (Current == window && _history.Count > 0)
                Current = _history.Peek();
            _windows[window]?.Close();
        }

        public void Close()
        {
            WindowType closeWindow = _history.Pop();
            _windows[closeWindow]?.Close();

            if (_history.Count == 0)
            {
                Current = WindowType.Invalid;
                return;
            }
            
            Current = _history.Peek();
            _windows[Current]?.Open();
        }

        public void CleanUpHistory()
        {
            if (_history.Any() == false) return;
            _history.Clear();
            _history.Push(Current);
        }

        private void Opening(WindowType window)
        {
#if !UNITY_EDITOR
            if (Current == window) return;
#endif
            _windows[window]?.Open();
            Current = window;
        }

        private void WritingToHistory(WindowType window)
        {
            if (Current == window) return;
            if(_history.Count > 0)
            {
                WindowType current = _history.Peek();
                _windows[current]?.Close();
            }
            _history.Push(window);
        }

        private void OnOpened(WindowType window) => Opened?.Invoke(window);
        private void OnClosed(WindowType window) => Closed?.Invoke(window);
    }
}