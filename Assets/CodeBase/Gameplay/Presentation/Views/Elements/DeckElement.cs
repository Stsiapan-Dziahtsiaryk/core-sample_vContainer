using CodeBase.Shared.Extends;
using UnityEngine;
using UnityEngine.EventSystems;
using VContainer.Unity;

namespace Gameplay.Presentation.Views.Elements
{
    public class DeckElement : MonoBehaviour
    {
        public class Pool : MonoPool<DeckElement>
        {
            public Pool(LifetimeScope container, DeckElement prefab, int maxInstances = 10, string parentName = "Parent of Pool") 
                : base(container, prefab, maxInstances, parentName) { }
        }

        public void AddCard(PointerEventData eventData)
        {
            eventData.pointerDrag.transform.SetParent(transform, false);
        }
        
    }
}