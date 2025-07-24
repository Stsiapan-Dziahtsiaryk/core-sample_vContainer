using UnityEngine;

namespace Shared.Presentation
{
    [RequireComponent(typeof(CanvasGroup))]
    public class CanvasGroupView : ViewBase
    {
        [field: SerializeField] public CanvasGroup CanvasGroup { get; private set; }

        private void OnValidate()
        {
            if(CanvasGroup == null)
                CanvasGroup = GetComponent<CanvasGroup>();
        }

        public void Show()
        {
            Debug.Log($"Show CanvasGroup: {gameObject.name}");
            CanvasGroup.alpha = 1;
            CanvasGroup.interactable = true;
            CanvasGroup.blocksRaycasts = true;
        }

        public void Hide()
        {
            Debug.Log($"Hide CanvasGroup: {gameObject.name}");
            CanvasGroup.alpha = 0;
            CanvasGroup.interactable = false;
            CanvasGroup.blocksRaycasts = false;
        }
    }
}