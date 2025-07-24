using UnityEngine;
using UnityEngine.UI;

namespace Gameplay.Audio
{
    [RequireComponent(typeof(Button))]
    public class SoundButton : MonoBehaviour
    {
        [SerializeField] private Button _button;
        [SerializeField] private AudioSource _audioSource;
        
        private void OnValidate()
        {
            if(_button == null)
                _button = GetComponent<Button>();
            if(_audioSource == null)
                _audioSource = GetComponent<AudioSource>();
        }

        private void OnEnable()
        {
            _button.onClick.AddListener(_audioSource.Play);
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(_audioSource.Play);
        }
    }
}