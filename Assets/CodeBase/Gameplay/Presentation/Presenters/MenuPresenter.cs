using System;
using CodeBase.Shared;
using Gameplay.Presentation.Data;
using Gameplay.Presentation.Views;
using Infrastructure.UIStateMachine;
using Shared.Presentation;
using UnityEngine;

namespace Gameplay.Presentation.Presenters
{
    public class MenuPresenter : ICanvasPresenter
    {
        private readonly MenuView _view;
        private readonly IWindowFsm _windowFsm;
        private readonly IGameHandler _gameModel;
        private readonly string _linkToPolicy;
        
        public MenuPresenter(
            MenuView view,
            IWindowFsm windowFsm, 
            IGameHandler gameModel)
        {
            _view = view ?? throw new ArgumentNullException(nameof(view));
            _windowFsm = windowFsm ?? throw new ArgumentNullException(nameof(windowFsm));
            _gameModel = gameModel ?? throw new ArgumentNullException(nameof(gameModel));
        }

        public void Enable()
        {
            _view.PlayButton.onClick.AddListener(OnPlay);
            _view.PrivacyButton.onClick.AddListener(OnOpenPrivacy);
        }

        public void Disable()
        {
            _view.PlayButton.onClick.RemoveListener(OnPlay);
            _view.PrivacyButton.onClick.RemoveListener(OnOpenPrivacy);
        }

        public void HandleOpenedWindow()
        {
            _view.Show();
        }

        public void HandleClosedWindow()
        {
            _view.Hide();
        }

        private void OnPlay()
        {
            _gameModel.OnHandleNewGame();
            _windowFsm.Close(WindowType.Menu);
        }

        private void OnOpenPrivacy()
        {
            Application.OpenURL(_linkToPolicy);
        }
    }
}
