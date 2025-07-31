using System;
using Gameplay.Presentation.Data;
using Gameplay.Presentation.StaticData;
using Gameplay.Presentation.Views;
using Gameplay.Presentation.Views.Elements;
using R3;
using Shared.Presentation;
using UnityEngine;
using UnityEngine.EventSystems;
using Object = UnityEngine.Object;

namespace Gameplay.Presentation.Presenters
{
    public class ControlBarPresenter : ICanvasPresenter
    {
        private readonly ControlBarView _view;
        private readonly CardElement.Factory _cardFactory;
        private readonly Table _model;
        private readonly IGameHandler _gameHandler;
        
        private readonly CompositeDisposable _disposable = new CompositeDisposable();
        
        public ControlBarPresenter(
            ControlBarView view,
            CardElement.Factory cardFactory,
            Table model,
            IGameHandler gameHandler)
        {
            _view = view ?? throw new ArgumentNullException(nameof(view));
            _cardFactory = cardFactory ?? throw new ArgumentNullException(nameof(cardFactory));
            _model = model ?? throw new ArgumentNullException(nameof(model));
            _gameHandler = gameHandler ?? throw new ArgumentNullException(nameof(gameHandler));
        }

        public void Enable()
        {
            _model.CreateEvent += CreateCard;
            _view.TrashZone.OnDropped.AddListener(OnRemoveCard);
            _gameHandler.State.Subscribe(OnChangedState).AddTo(_disposable);
        }

        public void Disable()
        {
            _model.CreateEvent -= CreateCard;
        }

        public void HandleOpenedWindow()
        {
            _view.Show();
        }

        public void HandleClosedWindow()
        {
            _view.Hide();
        }
        
        private void CreateCard(Card cardModel)
        {
            CardElement card = _cardFactory.Create();
            card.Spawn(cardModel.ID, _view.Buffer);
            cardModel
                .Weight
                .Subscribe(card.UpdateValue)
                .AddTo(card);
            
            cardModel
                .StateProperty
                .Where(x => x == Card.State.Destroyed)
                .Subscribe(_ => DestroyCards(card))
                .AddTo(card);
            
            cardModel
                .StateProperty
                .Where(x => x == Card.State.Top)
                .Subscribe(_ => card.SetInteraction())
                .AddTo(card);
            
            card.transform.SetParent(_view.DeckContainer.transform, false);
            card.transform.SetAsFirstSibling();
            
            void DestroyCards(CardElement cardElement)
            {
                Object.Destroy(cardElement.gameObject);
            }
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
                    for (int i = 0; i < _view.DeckContainer.childCount; i++)
                    {
                        Object.Destroy(_view.DeckContainer.GetChild(i).gameObject);
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