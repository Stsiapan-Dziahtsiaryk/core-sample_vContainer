using System;
using CodeBase.Shared.Extends;
using Gameplay.Presentation.Data;
using Shared.Presentation;
using UnityEngine;
using UnityEngine.EventSystems;
using VContainer.Unity;

namespace Gameplay.Presentation.Views.Elements
{
    public class DeckElement : MonoBehaviour
    {
        public class Pool : MonoPool<DeckElement>
        {
            public Pool(LifetimeScope container, DeckElement prefab, int maxInstances = 10,
                string parentName = "Parent of Pool")
                : base(container, prefab, maxInstances, parentName)
            {
            }
        }

        public event Action AddCardEvent;

        public void AddCard(PointerEventData eventData)
        {
            GameObject card = eventData.pointerDrag;
            card.transform.SetParent(transform, false);
            if(card.TryGetComponent(out DraggableElement draggable))
                Destroy(draggable);
            AddCardEvent?.Invoke();
        }
    }
}