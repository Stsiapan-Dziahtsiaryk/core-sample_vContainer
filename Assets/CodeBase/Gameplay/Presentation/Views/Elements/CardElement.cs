using System;
using CodeBase.Shared.Extends;
using Gameplay.Presentation.Data;
using R3;
using Shared.Presentation;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using VContainer.Unity;
using Object = UnityEngine.Object;

namespace Gameplay.Presentation.Views.Elements
{
    public class CardElement : MonoBehaviour
    {
        public class Pool : MonoPool<CardElement>
        {
            public Pool(LifetimeScope container, CardElement prefab, int maxInstances = 10, string parentName = "Parent of Pool") 
                : base(container, prefab, maxInstances, parentName) { }
        }

        [SerializeField] private Image _graphic;
        [SerializeField] private TMP_Text _cardNumber;
        [SerializeField] private DraggableElement _draggableElement;
        [SerializeField] private Sprite[] _skinsSprites;
        
        public readonly CompositeDisposable Disposable = new CompositeDisposable();
        
        public void Spawn(int id, RectTransform buffer = null)
        {
            if(buffer != null)
            {
                _draggableElement.SetBuffer(buffer);
                _draggableElement.SetInteraction(true);
            }
            else
            {
                if(_draggableElement != null)
                {
                    Destroy(_draggableElement);
                    _draggableElement = null;
                }    
            }
            
            _graphic.sprite = _skinsSprites[id];
        }

        public void UpdateValue(int value)
        {
            _cardNumber.text = value.ToString();
        }

        public void SetInteraction(bool isActive)
        {
            if (_draggableElement != null)
                _draggableElement.SetInteraction(isActive);
        }

        public void OnChangeState(Card.State state)
        {
            switch (state)
            {
                case Card.State.Invalid:
                    break;
                case Card.State.Created:
                    SetInteraction(true);
                    break;
                case Card.State.Top:
                    gameObject.SetActive(true);
                    SetInteraction(false);
                    break;
                case Card.State.Destroyed:
                    break;
                case Card.State.Cash:
                    gameObject.SetActive(false);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(state), state, null);
            }
        }

        private void OnDisable()
        {
            Disposable.Clear();
        }
    }
}