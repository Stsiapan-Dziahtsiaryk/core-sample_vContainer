using System;
using Domain.Presentation.Views;
using Shared.Presentation;

namespace Domain.Presentation.Presenters
{
    public class CurtainPresenter : ICanvasPresenter
    {
        private readonly CurtainView _view;

        public CurtainPresenter(CurtainView view)
        {
            _view = view ?? throw new ArgumentNullException(nameof(view));
        }

        public void Enable()
        {
            _view.Show();
        }

        public void Disable()
        {
            
        }

        public void HandleOpenedWindow()
        {
            _view.ProgressElement.gameObject.SetActive(true);
            _view.Show();
        }

        public void HandleClosedWindow()
        {
            _view.ProgressElement.gameObject.SetActive(false);
            _view.Hide();
        }
    }
}