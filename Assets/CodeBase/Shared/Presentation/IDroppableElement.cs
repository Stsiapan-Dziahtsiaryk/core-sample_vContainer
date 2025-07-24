using UnityEngine;
using UnityEngine.EventSystems;

namespace Shared.Presentation
{
    public interface IDroppableElement :
        IBeginDragHandler, 
        IDragHandler, 
        IEndDragHandler,
        IPointerDownHandler,
        IPointerUpHandler
    {
        RectTransform RTransform { get; }
        void Set(RectTransform parent);

    }
}