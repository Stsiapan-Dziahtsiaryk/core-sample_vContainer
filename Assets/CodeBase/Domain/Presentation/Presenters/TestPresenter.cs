using System;
using CodeBase.Shared;
using CodeBase.Shared.StaticData;
using Domain.Presentation.Views;
using Infrastructure.StateMachine;
using Infrastructure.UIStateMachine;
using Shared.Presentation;

namespace Domain.Presentation.Presenters
{
    public class TestPresenter : ICanvasPresenter
    {
        private readonly IAppStateMachine _stateMachine;
        private readonly IWindowFsm _windowFsm;
        private readonly TestView _view;
        
        private bool _isOpen = false;
        
        public TestPresenter(
            IAppStateMachine stateMachine,
            TestView view,
            IWindowFsm windowFsm)
        {
            _stateMachine = stateMachine ?? throw new ArgumentNullException(nameof(stateMachine));
            _view = view ?? throw new ArgumentNullException(nameof(view));
            _windowFsm = windowFsm ?? throw new ArgumentNullException(nameof(windowFsm));
        }

        public void Enable()
        {
            _view.ChangeStateButton.onClick.AddListener(OnChangeState);   
            _view.CheckWindowButton.onClick.AddListener(OnCheckWindow);
        }

        public void Disable()
        {
            _view.ChangeStateButton.onClick.RemoveListener(OnChangeState);   
        }
        
        private void OnChangeState()
        {
            _stateMachine.Enter(AppState.Gameplay);   
        }

        private void OnCheckWindow()
        {
            if(_isOpen)
            {
                _windowFsm.Close(WindowType.Curtain);
                _isOpen = false;
            }
            else
            {
                _windowFsm.Open(WindowType.Curtain);
                _isOpen = true;
            }
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