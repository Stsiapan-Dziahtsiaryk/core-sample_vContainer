using System;
using Gameplay.Presentation.Data;
using Gameplay.Presentation.Views;
using Gameplay.Presentation.Views.Elements;
using Infrastructure.UIStateMachine;
using R3;
using Shared.Presentation;

namespace Gameplay.Presentation.Presenters
{
    public class TablePresenter : ICanvasPresenter
    {
        private readonly TableView _view;
        private readonly Table _model;
        private readonly DeckElement.Pool _deckPool;
        private readonly CardElement.Pool _cardPool;
                
        private readonly IWindowFsm _windowFsm;

        private readonly CompositeDisposable _disposables = new CompositeDisposable();
        
        public TablePresenter(
            TableView view,
            IWindowFsm windowFsm,
            DeckElement.Pool deckPool,
            CardElement.Pool cardPool,
            Table model)
        {
            _view = view ?? throw new ArgumentNullException(nameof(view));
            _windowFsm = windowFsm ?? throw new ArgumentNullException(nameof(windowFsm));
            _deckPool = deckPool ?? throw new ArgumentNullException(nameof(deckPool));
            _cardPool = cardPool ?? throw new ArgumentNullException(nameof(cardPool));
            _model = model ?? throw new ArgumentNullException(nameof(model));
        }
        
        public void Enable()
        {
            _model.HandleNewGameEvent += CreateDecks;
        }

        public void Disable()
        {
            _model.HandleNewGameEvent -= CreateDecks;
        }

        public void HandleOpenedWindow()
        {
            _view.Show();
        }

        public void HandleClosedWindow()
        {
            _view.Hide();
        }

        private void CreateDecks()
        {
            for (var i = 0; i < _model.Decks.Count; i++)
            {
                DeckElement deck = _deckPool.Spawn();
                deck.transform.SetParent(_view.Content.transform, false);                
                
            }
        }

        private void OnSetToDeck(int deckId)
        {
            
        }
    }
}