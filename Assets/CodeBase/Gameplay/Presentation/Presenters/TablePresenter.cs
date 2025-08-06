using System;
using Gameplay.Presentation.Data;
using Gameplay.Presentation.StaticData;
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

        private readonly IGameHandler _gameHandler;
        private readonly IWindowFsm _windowFsm;

        private readonly CompositeDisposable _disposables = new CompositeDisposable();

        public TablePresenter(
            TableView view,
            IWindowFsm windowFsm,
            DeckElement.Pool deckPool,
            Table model,
            IGameHandler gameHandler,
            CardElement.Pool cardPool)
        {
            _view = view ?? throw new ArgumentNullException(nameof(view));
            _windowFsm = windowFsm ?? throw new ArgumentNullException(nameof(windowFsm));
            _deckPool = deckPool ?? throw new ArgumentNullException(nameof(deckPool));
            _model = model ?? throw new ArgumentNullException(nameof(model));
            _gameHandler = gameHandler ?? throw new ArgumentNullException(nameof(gameHandler));
            _cardPool = cardPool ?? throw new ArgumentNullException(nameof(cardPool));
        }

        public void Enable()
        {
            CreateDecks();
            _gameHandler.State.Subscribe(OnChangedState).AddTo(_disposables);
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

        private void CreateDecks()
        {
            for (var i = 0; i < _model.Decks.Count; i++)
            {
                DeckElement deck = _deckPool.Spawn();
                deck.transform.SetParent(_view.Content.transform, false);
                
                CreateCardsPool(_model.Decks[i], deck);
                _model.Decks[i].UpdateViewEvent += deck.OnUpdate;
                deck.AddCardEvent += _model.Decks[i].OnAdd;
            }
        }

        private void CreateCardsPool(Deck model, DeckElement deck)
        {
            for (int i = 0; i < model.MaxSize; i++)
            {
                CardElement card = _cardPool.Spawn();
                card.Spawn(model.ID);
                card.transform.SetParent(deck.transform, false);
                card.gameObject.SetActive(false);
                deck.Setup(card);
            }
        }

        private void OnChangedState(GameState state)
        {
            switch (state)
            {
                case GameState.Invalid:
                    break;
                case GameState.Game:
                    break;
                case GameState.Pause:
                    break;
                case GameState.Play:
                    break;
                case GameState.End:
                    break;
                case GameState.Menu:
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(state), state, null);
            }
        }
    }
}