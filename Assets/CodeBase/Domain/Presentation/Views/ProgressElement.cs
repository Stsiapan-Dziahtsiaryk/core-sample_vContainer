using DG.Tweening;
using UnityEngine;

namespace Domain.Presentation.Views
{
    public class ProgressElement : MonoBehaviour
    {
        [SerializeField] private RectTransform _rectTransform;
        [SerializeField] private float _duration;
        
        private Tween _tween;

        private void OnValidate()
        {
            if(_rectTransform == null)
                _rectTransform = GetComponent<RectTransform>();
        }

        private void OnEnable()
        {
            PlayAnimation();
        }

        private void PlayAnimation()
        {
            _tween = _rectTransform
                .DOLocalRotate(new Vector3(0,0,360), _duration, RotateMode.LocalAxisAdd)
                .SetEase(Ease.InOutSine)
                .SetLoops(-1, LoopType.Incremental)
                .OnKill(() => _rectTransform.eulerAngles = Vector3.zero);
        }
        
        private void OnDisable()
        {
            _tween?.Kill();
        }
    }
}