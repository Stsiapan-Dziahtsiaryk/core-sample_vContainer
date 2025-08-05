using System;
using DG.Tweening;
using Gameplay.Presentation.Views;
using Gameplay.Services;
using Shared.Presentation;

namespace Gameplay.Presentation.Presenters
{
    public class PlayerPresenter : IPresenter
    {
        private readonly PlayerView _view;
        private readonly InputService _inputService;

        public PlayerPresenter(
            PlayerView view,
            InputService inputService)
        {
            _view = view ?? throw new ArgumentNullException(nameof(view));
            _inputService = inputService ?? throw new ArgumentNullException(nameof(inputService));
        }

        public void Enable()
        {
            _inputService.Swiped += OnSwipe;
            _inputService.InvokedUp += OnJump;
        }

        public void Disable()
        {
            _inputService.Swiped -= OnSwipe;
            _inputService.InvokedUp -= OnJump;            
        }

        private void OnJump()
        {
            _view.Jump();
        }

        private void OnSwipe(int direction)
        {
            
        }
    }
}