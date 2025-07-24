using UnityEngine;
using UnityEngine.Audio;

namespace Gameplay.Audio
{
    [CreateAssetMenu(fileName = "Audio Config", menuName = "Data/Configs/Create Audio Config", order = 51)]
    public class AudioConfig : ScriptableObject
    {
        [field: SerializeField] public AudioMixerGroup MasterGroup { get; private set; }
        [field: SerializeField] public AudioMixerGroup SoundGroup { get; private set; }
        [field: SerializeField] public AudioMixerGroup MusicGroup { get; private set; }
    }
}