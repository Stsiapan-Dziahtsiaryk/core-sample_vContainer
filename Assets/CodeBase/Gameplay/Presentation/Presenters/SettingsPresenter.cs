using System;
using CodeBase.Shared;
using Gameplay.Audio;
using Gameplay.Presentation.Data;
using Gameplay.Presentation.StaticData;
using Gameplay.Presentation.Views;
using Infrastructure.UIStateMachine;
using Shared.Presentation;

namespace Gameplay.Presentation.Presenters
{
    public class SettingsPresenter : ICanvasPresenter
    {
        private readonly SettingsView _view;
        private readonly IWindowFsm _windowFsm;
        private readonly IGameHandler _gameModel;
        private readonly AudioService _audioService;
        
        public SettingsPresenter(
            SettingsView view,
            IWindowFsm windowFsm, 
            IGameHandler gameModel,
            AudioService audioService)
        {
            _view = view ?? throw new ArgumentNullException(nameof(view));
            _windowFsm = windowFsm ?? throw new ArgumentNullException(nameof(windowFsm));
            _gameModel = gameModel ?? throw new ArgumentNullException(nameof(gameModel));
            _audioService = audioService ?? throw new ArgumentNullException(nameof(audioService));
        }

        public void Enable()
        {
            _view.CloseButton.onClick.AddListener(OnClose);
            _view.MenuButton.onClick.AddListener(OnOpenMenu);
            _view.SoundToggle.onValueChanged.AddListener(_audioService.OnActiveAudio);
            _view.VibrationToggle.onValueChanged.AddListener(_audioService.OnActiveVibration);
            
        }

        public void Disable()
        {
            _view.CloseButton.onClick.RemoveListener(OnClose);
            _view.MenuButton.onClick.RemoveListener(OnOpenMenu);
            _view.SoundToggle.onValueChanged.RemoveListener(_audioService.OnActiveAudio);
            _view.VibrationToggle.onValueChanged.RemoveListener(_audioService.OnActiveVibration);
        }

        public void HandleOpenedWindow()
        {
            _view.Show();
        }

        public void HandleClosedWindow()
        {
            _view.Hide();
        }

        private void OnClose()
        {
            _audioService.OnPlayVibration();
            _gameModel.OnChangeGameState(GameState.Play);
            _windowFsm.Close();
        }

        private void OnOpenMenu()
        {
            _audioService.OnPlayVibration();
            _gameModel.OnChangeGameState(GameState.Menu);
            _windowFsm.Open(WindowType.Menu, true);
            _windowFsm.CleanUpHistory();
        }
    }
}