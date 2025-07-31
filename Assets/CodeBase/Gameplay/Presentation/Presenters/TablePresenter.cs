using System;
using Gameplay.Presentation.Data;
using Gameplay.Presentation.StaticData;
using Gameplay.Presentation.Views;
using Gameplay.Presentation.Views.Elements;
using Infrastructure.UIStateMachine;
using R3;
using Shared.Presentation;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Gameplay.Presentation.Presenters
{
    public class TablePresenter : ICanvasPresenter
    {
        private readonly TableView _view;
        private readonly Table _model;
        private readonly DeckElement.Pool _deckPool;
        private readonly CardElement.Factory _cardFactory;

        private readonly IGameHandler _gameHandler;
        private readonly IWindowFsm _windowFsm;

        private readonly CompositeDisposable _disposables = new CompositeDisposable();

        public TablePresenter(
            TableView view,
            IWindowFsm windowFsm,
            DeckElement.Pool deckPool,
            CardElement.Factory cardFactory,
            Table model,
            IGameHandler gameHandler)
        {
            _view = view ?? throw new ArgumentNullException(nameof(view));
            _windowFsm = windowFsm ?? throw new ArgumentNullException(nameof(windowFsm));
            _deckPool = deckPool ?? throw new ArgumentNullException(nameof(deckPool));
            _cardFactory = cardFactory ?? throw new ArgumentNullException(nameof(cardFactory));
            _model = model ?? throw new ArgumentNullException(nameof(model));
            _gameHandler = gameHandler ?? throw new ArgumentNullException(nameof(gameHandler));
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
                deck.AddCardEvent += _model.Decks[i].OnAdd;
                _model.Decks[i].AddCardEvent += deck.OnAddCard;
            }
        }

        private void OnChangedState(GameState state)
        {
            switch (state)
            {
                case GameState.Invalid:
                    break;
                case GameState.Game:
                    for (int i = 0; i < _view.Content.childCount; i++)
                    {
                        Transform deck = _view.Content.GetChild(i);
                        for (int j = 0; j < deck.childCount; j++)
                        {
                            Object.Destroy(deck.GetChild(j).gameObject);
                        }
                    }
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