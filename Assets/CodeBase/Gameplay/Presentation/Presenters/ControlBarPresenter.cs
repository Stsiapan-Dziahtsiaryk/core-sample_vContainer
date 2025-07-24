using System;
using Gameplay.Presentation.Data;
using Gameplay.Presentation.Views;
using Gameplay.Presentation.Views.Elements;
using Shared.Presentation;

namespace Gameplay.Presentation.Presenters
{
    public class ControlBarPresenter : ICanvasPresenter
    {
        private readonly ControlBarView _view;
        private readonly CardElement.Pool _cardPool;
        private readonly Table _model;
        
        public ControlBarPresenter(
            ControlBarView view,
            CardElement.Pool cardPool,
            Table model)
        {
            _view = view ?? throw new ArgumentNullException(nameof(view));
            _cardPool = cardPool ?? throw new ArgumentNullException(nameof(cardPool));
            _model = model ?? throw new ArgumentNullException(nameof(model));
        }

        public void Enable()
        {
            _model.HandleNewGameEvent += CreateCards;
        }

        private void CreateCards()
        {
            for (var i = 0; i < _model.Cards.Count; i++)
            {
                CardElement card =  _cardPool.Spawn();
                card.Spawn(_view.Buffer);
                card.transform.SetParent(_view.DeckContainer.transform, false);
            }
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