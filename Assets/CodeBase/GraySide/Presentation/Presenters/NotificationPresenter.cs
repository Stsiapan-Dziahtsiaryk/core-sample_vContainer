using System;
using GraySide.Presentation.Views;
using Infrastructure.UIStateMachine;
using Shared.Presentation;

namespace GraySide.Presentation.Presenters
{
    public class NotificationPresenter : ICanvasPresenter
    {
        private readonly NotificationView _view;
        private readonly IWindowFsm _windowFsm;
        
        public NotificationPresenter(
            NotificationView view,
            IWindowFsm windowFsm)
        {
            _view = view ?? throw new ArgumentNullException(nameof(view));
            _windowFsm = windowFsm;
        }

        public void Enable()
        {
            _view.AcceptButton.onClick.AddListener(OnAcceptPush);
            _view.SkipButton.onClick.AddListener(OnSkip);
        }

        public void Disable()
        {
            _view.AcceptButton.onClick.RemoveListener(OnAcceptPush);
            _view.SkipButton.onClick.RemoveListener(OnSkip);
        }

        private void OnAcceptPush()
        {
            // ToDo: Add logic. Invoke an agreement
            _windowFsm.Close();
        }
        
        private void OnSkip()
        {
            _windowFsm.Close();
        }

        public void HandleOpenedWindow()
        {
            _view.Show();
        }

        public void HandleClosedWindow()
        {
            _view.Hide();
        }
    }
}