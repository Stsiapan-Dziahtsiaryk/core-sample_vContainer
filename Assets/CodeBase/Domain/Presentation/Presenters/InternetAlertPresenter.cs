using System;
using Domain.Presentation.Views;
using Shared.Presentation;

namespace Domain.Presentation.Presenters
{
    public class InternetAlertPresenter : ICanvasPresenter
    {
        private InternetAlertView _view;

        public InternetAlertPresenter(InternetAlertView view)
        {
            _view = view ?? throw new ArgumentNullException(nameof(view));
        }

        public void Enable()
        {
            
        }

        public void Disable()
        {
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