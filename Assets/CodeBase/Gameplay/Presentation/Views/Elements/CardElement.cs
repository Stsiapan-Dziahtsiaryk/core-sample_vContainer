using CodeBase.Shared.Extends;
using Shared.Presentation;
using TMPro;
using UnityEngine;
using VContainer.Unity;

namespace Gameplay.Presentation.Views.Elements
{
    public class CardElement : MonoBehaviour
    {
        public class Pool : MonoPool<CardElement>
        {
            public Pool(LifetimeScope container, CardElement prefab, int maxInstances = 10, string parentName = "Parent of Pool") 
                : base(container, prefab, maxInstances, parentName) { }
        }
        
        [SerializeField] private TMP_Text _cardNumber;
        [SerializeField] private DraggableElement _draggableElement;

        public void Spawn(RectTransform buffer)
        {
            _draggableElement.SetBuffer(buffer);
        }
    }
}