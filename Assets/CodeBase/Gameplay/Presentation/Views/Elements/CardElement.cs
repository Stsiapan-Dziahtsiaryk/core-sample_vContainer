using CodeBase.Shared.Extends;
using Shared.Presentation;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using VContainer.Unity;

namespace Gameplay.Presentation.Views.Elements
{
    public class CardElement : MonoBehaviour
    {
        public class Factory : MonoFactory<CardElement>
        {
            public Factory(LifetimeScope container, CardElement prefab) : base(container, prefab)
            {
            }
        }

        [SerializeField] private Image _graphic;
        [SerializeField] private TMP_Text _cardNumber;
        [SerializeField] private DraggableElement _draggableElement;
        [SerializeField] private Sprite[] _skinsSprites;
        
        public void Spawn(int id, RectTransform buffer)
        {
            _draggableElement.SetBuffer(buffer);
            _draggableElement.SetInteraction(true);
            
            _graphic.sprite = _skinsSprites[id];
        }

        public void UpdateValue(int value)
        {
            _cardNumber.text = value.ToString();
        }

        public void SetInteraction()
        {
            _draggableElement.SetInteraction(false);
        }
    }
}