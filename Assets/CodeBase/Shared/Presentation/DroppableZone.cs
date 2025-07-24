using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace Shared.Presentation
{
    public class DroppableZone : MonoBehaviour,
        IDropHandler,
        IPointerEnterHandler,
        IPointerExitHandler
    {
        public UnityEvent<PointerEventData> OnDropped = new();
        public UnityEvent<PointerEventData> OnEnter = new();
        public UnityEvent<PointerEventData> OnExit = new();
        
        public void OnDrop(PointerEventData eventData)
        {
            if(eventData.pointerDrag == null) return;
            OnDropped?.Invoke(eventData);
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            if(eventData.pointerDrag == null) return;
            OnEnter?.Invoke(eventData);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if(eventData.pointerDrag == null) return;
            OnExit?.Invoke(eventData);
        }
    }
}