using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Shared.Presentation
{
    public class DraggableElement : MonoBehaviour,
        IDroppableElement
    {
        public Func<bool> OnCanceled;

        [SerializeField] private Canvas _canvas;
        [SerializeField] private RectTransform _buffer;

        [SerializeField] private UnityEvent OnDownPointer = new UnityEvent();
        [SerializeField] private UnityEvent OnUpPointer = new UnityEvent();
        [SerializeField] private UnityEvent OnBegin = new UnityEvent();
        [SerializeField] private UnityEvent<Vector2> OnDragging = new UnityEvent<Vector2>();
        [SerializeField] private UnityEvent<bool> OnEnd = new UnityEvent<bool>();


        private Transform _parent;
        private RectTransform _rTransform;
        private Image _image;
        private Camera _camera;
        private bool _isInteractionBlocked;

        private void Start()
        {
            _camera = Camera.main;
        }

        public RectTransform RTransform
        {
            get
            {
                if (_rTransform == null)
                    TryGetComponent(out _rTransform);
                return _rTransform;
            }
        }

        public Image TargetImage
        {
            get
            {
                if (_image == null)
                    _image = GetComponent<Image>();
                return _image;
            }
        }

        private RectTransform Buffer
        {
            get
            {
                if (_buffer == null && _canvas != null)
                    _canvas.TryGetComponent(out _buffer);
                return _buffer;
            }
            set => _buffer = value;
        }

        private Canvas CanvasB
        {
            get
            {
                if(_canvas == null)
                    _canvas = _buffer.GetComponent<Canvas>();
                return _canvas;
            }
        }

        public void SetBuffer(RectTransform buffer)
        {
            Buffer = buffer;
        }

        public void Set(RectTransform parent)
        {
            _parent = parent;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            if (_isInteractionBlocked)
                return;

            OnDownPointer?.Invoke();
            _parent = RTransform.parent;
            RTransform.SetParent(Buffer.transform, true);
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (_isInteractionBlocked)
                return;

            if (OnCanceled != null && OnCanceled.Invoke())
                return;

            OnDragging?.Invoke(eventData.delta);

            float scaleCoefficient = Camera.main.orthographicSize * 2 / Screen.height;

            Vector2 position = eventData.delta / CanvasB.scaleFactor;
            
            RTransform.anchoredPosition += position;
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            if (_isInteractionBlocked)
                return;

            if (OnCanceled != null && OnCanceled.Invoke())
                return;

            TargetImage.raycastTarget = false;
            OnBegin?.Invoke();
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            if (_isInteractionBlocked)
                return;

            if (OnCanceled != null && OnCanceled.Invoke())
                return;

            bool isOldParent = transform.root == transform.parent;
            TargetImage.raycastTarget = true;

            if (isOldParent)
                transform.SetParent(_parent);

            OnEnd?.Invoke(isOldParent);
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            if (_isInteractionBlocked)
                return;

            OnUpPointer?.Invoke();
            TargetImage.raycastTarget = true;
            RTransform.SetParent(_parent, true);
        }

        public void SetInteraction(bool isInteractionAllowed) =>
            _isInteractionBlocked = isInteractionAllowed;
    }
}