using System;
using System.Collections.Generic;
using System.Linq;
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
        
        private List<CardElement> _cards = new List<CardElement>();
        
        public event Action AddCardEvent;

        private void OnEnable()
        {
            if(TryGetComponent(out DroppableZone dropZone))
                dropZone.OnDropped.AddListener(TryAddCard);
        }

        public void Setup(CardElement card)
        {
            _cards.Add(card);
        }

        public void OnUpdate(Card[] cards = null)
        {
            if(cards == null)
                _cards.ForEach(x => x.gameObject.SetActive(false));
            
            var rightOrder = cards.Reverse().ToArray();
            
            for (var i = 0; i < _cards.Count; i++)
            {
                if(i < rightOrder.Length)
                    _cards[i].UpdateValue(rightOrder[i].Weight.Value);
                _cards[i].gameObject.SetActive(i < rightOrder.Length);
            }
        }
        
        private void TryAddCard(PointerEventData eventData)
        {
            AddCardEvent?.Invoke();
        }

        private void OnDisable()
        {
            if(TryGetComponent(out DroppableZone dropZone))
                dropZone.OnDropped.RemoveListener(TryAddCard);
        }
    }
}