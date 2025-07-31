using System;
using CodeBase.Shared;
using Gameplay.Presentation.Data;
using Gameplay.Presentation.StaticData;
using Gameplay.Presentation.Views;
using Infrastructure.UIStateMachine;
using Shared.Presentation;

namespace Gameplay.Presentation.Presenters
{
    public class HUDPresenter : ICanvasPresenter
    {
        private readonly HUDView _view;
        private readonly IWindowFsm _windowFsm;
        private readonly IGameHandler _gameHandler;
        private readonly Game _model;

        public HUDPresenter(
            HUDView view,
            IWindowFsm windowFsm,
            IGameHandler gameHandler,
            Game model)
        {
            _view = view ?? throw new ArgumentNullException(nameof(view));
            _windowFsm = windowFsm ?? throw new ArgumentNullException(nameof(windowFsm));
            _gameHandler = gameHandler ?? throw new ArgumentNullException(nameof(gameHandler));
            _model = model ?? throw new ArgumentNullException(nameof(model));
        }

        public void Enable()
        {
            _view.HomeButton.onClick.AddListener(OnGoHome);
            _view.RestartButton.onClick.AddListener(OnRestartGame);
        }

        public void Disable()
        {
            _view.HomeButton.onClick.RemoveListener(OnGoHome);
            _view.RestartButton.onClick.RemoveListener(OnRestartGame);
        }

        public void HandleOpenedWindow()
        {
            _view.Show();
        }

        public void HandleClosedWindow()
        {
            _view.Hide();
        }
        
        private void OnRestartGame()
        {
            _gameHandler.OnHandleNewGame();
        }

        private void OnGoHome()
        {
            _windowFsm.Open(WindowType.Menu);
            _gameHandler.OnChangeGameState(GameState.Menu);
        }
    }
}