using System;
using UnityEngine;
using UnityEngine.UI;

namespace Gameplay.Audio
{
    [RequireComponent(typeof(Toggle))]
    public class SwitchElement : MonoBehaviour
    {
        [SerializeField] private Toggle _toggle;
        [SerializeField] private RectTransform _knobTransform;
        [SerializeField] private Image _knobImage;
        [SerializeField] private SwitchType _type;
        [SerializeField] private Vector2 _knobOffset;
        [SerializeField] private Color _disabledColor;
        
        private void OnValidate()
        {
            if(_toggle == null)
                _toggle = GetComponent<Toggle>();
        }

        private void OnEnable()
        {
            _toggle.onValueChanged.AddListener(OnChange);
        }

        private void OnDisable()
        {
            _toggle.onValueChanged.RemoveListener(OnChange);
        }

        private void OnChange(bool isOn)
        {
            switch (_type)
            {
                case SwitchType.Invalid:
                    break;
                case SwitchType.Offset:
                    _knobImage.color = isOn ? Color.white: _disabledColor;
                    if(_knobTransform != null)
                        _knobTransform.anchoredPosition = isOn ? _knobOffset : -_knobOffset;
                    break;
                case SwitchType.Flip:
                    if(_knobTransform !=null)
                        _knobTransform.localScale = new Vector3(isOn ? -1 : 1, 1, 1);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private enum SwitchType
        {
            Invalid= 0,
            Offset = 1,
            Flip = 2
        }
    }
}