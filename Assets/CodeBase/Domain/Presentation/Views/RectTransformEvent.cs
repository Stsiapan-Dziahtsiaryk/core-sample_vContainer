using System;
using System.Collections;
using UnityEngine;

namespace Domain.Presentation.Views
{
    public class RectTransformEvent : MonoBehaviour
    {
        [SerializeField] private RectTransform _rectTransform;
 
        public event Action OnUpdateRectTransform;
 
        private void OnValidate()
        {
            if(_rectTransform == null)
                _rectTransform = GetComponent<RectTransform>();
        }
        
        private void OnRectTransformDimensionsChange()
        {
            Debug.Log("Update RectTransform event");
            // OnUpdateRectTransform?.Invoke();
            StartCoroutine(UpdateFame());
        }

        private IEnumerator UpdateFame()
        {
            yield return new WaitForEndOfFrame();
            
            OnUpdateRectTransform?.Invoke();
            yield return null;
        }
    }
}