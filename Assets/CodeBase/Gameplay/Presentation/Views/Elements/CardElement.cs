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

        public void Spawn(RectTransform buffer)
        {
            _draggableElement.SetBuffer(buffer);
            _draggableElement.SetInteraction(true);
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