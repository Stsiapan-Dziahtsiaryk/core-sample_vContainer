using System;
using Gameplay.Presentation.Data;
using Gameplay.Presentation.StaticData;
using Gameplay.Presentation.Views;
using Gameplay.Presentation.Views.Elements;
using R3;
using Shared.Presentation;
using UnityEngine.EventSystems;

namespace Gameplay.Presentation.Presenters
{
    public class ControlBarPresenter : ICanvasPresenter
    {
        private readonly ControlBarView _view;
        private readonly CardElement.Pool _cardPool;
        private readonly Table _model;
        private readonly IGameHandler _gameHandler;
        
        private readonly CompositeDisposable _disposable = new CompositeDisposable();
        
        public ControlBarPresenter(
            ControlBarView view,
            Table model,
            IGameHandler gameHandler, 
            CardElement.Pool cardPool)
        {
            _view = view ?? throw new ArgumentNullException(nameof(view));
            _model = model ?? throw new ArgumentNullException(nameof(model));
            _gameHandler = gameHandler ?? throw new ArgumentNullException(nameof(gameHandler));
            _cardPool = cardPool ?? throw new ArgumentNullException(nameof(cardPool));
        }

        public void Enable()
        {
            _model.CreateEvent += CreateCardFromPool;
            _view.TrashZone.OnDropped.AddListener(OnRemoveCard);
            _view.StepBackButton.onClick.AddListener(_model.OnUndo);
            _gameHandler.State.Subscribe(OnChangedState).AddTo(_disposable);
        }

        public void Disable()
        {
            _model.CreateEvent -= CreateCardFromPool;
        
            _view.TrashZone.OnDropped.RemoveListener(OnRemoveCard);
            _view.StepBackButton.onClick.RemoveListener(_model.OnUndo);
            _disposable.Clear();
        }

        public void HandleOpenedWindow()
        {
            _view.Show();
        }

        public void HandleClosedWindow()
        {
            _view.Hide();
        }
        
        private void CreateCardFromPool(Card model)
        {
            CardElement card = _cardPool.Spawn();
            card.Spawn(model.ID, _view.Buffer);
            card.UpdateValue(model.Weight.Value);
            
            model
                .StateProperty
                .Where(x => x == Card.State.Destroyed).Subscribe(x =>
                {
                    _cardPool.Despawn(card);
                }).AddTo(card.Disposable);
            
            model
                .StateProperty
                .Subscribe(card.OnChangeState).AddTo(card.Disposable);
            
            card.transform.SetParent(_view.DeckContainer.transform, false);
            card.transform.SetAsFirstSibling();

        }
        
        private void OnRemoveCard(PointerEventData eventData)
        {
            _model.RemoveCard();
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