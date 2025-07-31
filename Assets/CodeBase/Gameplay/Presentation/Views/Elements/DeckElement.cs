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

        private GameObject CardCash { get; set; }
        
        public event Action AddCardEvent;

        private void OnEnable()
        {
            if(TryGetComponent(out DroppableZone dropZone))
                dropZone.OnDropped.AddListener(TryAddCard);
        }

        public void OnAddCard(bool isAdd)
        {
            if(isAdd)
            {
                CardCash.transform.SetParent(transform, false);
                if (CardCash.TryGetComponent(out DraggableElement draggable))
                    Destroy(draggable);
            }
            else
            {
                // get last added child
            }
        }

        private void TryAddCard(PointerEventData eventData)
        {
            CardCash = eventData.pointerDrag;
            
            AddCardEvent?.Invoke();
        }

        private void OnDisable()
        {
            if(TryGetComponent(out DroppableZone dropZone))
                dropZone.OnDropped.RemoveListener(TryAddCard);
        }
    }
}