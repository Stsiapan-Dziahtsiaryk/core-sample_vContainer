using System;
using UnityEngine;
using UnityEngine.Audio;
using Object = UnityEngine.Object;

namespace Gameplay.Audio
{
  public class AudioService
    {
        private const string MUSIC_VOLUME = "MusicVolume";
        private const string SOUND_VOLUME = "SoundVolume";
        private const string MasterVolume = "MasterVolume";
        private const string VibrationVolume = "VibrationVolume";

        private float _musicVolume;
        private float _soundVolume;

        private AudioSource _soundSource;
        private AudioSource _musicSource;

        private readonly AudioMixerGroup _soundMixerGroup;
        private readonly AudioMixerGroup _musicMixerGroup;
        private readonly AudioMixerGroup _masterMixerGroup;

        public AudioService(AudioConfig config)
        {
            _masterMixerGroup = config.MasterGroup ?? throw new ArgumentNullException(nameof(config.MasterGroup));
            _musicVolume = 1f;
            _soundVolume = 1f;
        }

        public float MusicVolume => _musicVolume;
        public float SoundVolume => _soundVolume;

        public bool IsEnabledAudio {
            get => PlayerPrefs.GetInt(MasterVolume, 1) == 1;
            private set
            {
                PlayerPrefs.SetInt(MasterVolume, value ? 1 : 0);
                PlayerPrefs.Save();
            }
        }

        public bool IsEnabledVibration
        {
            get => PlayerPrefs.GetInt(VibrationVolume, 1) == 1;
            private set
            {
                PlayerPrefs.SetInt(VibrationVolume, value ? 1 : 0);
                PlayerPrefs.Save();
            }
        }


        public void Initialize()
        {
            if (_soundSource == null && _soundMixerGroup != null)
            {
                _soundSource = CreateComponent("Sound", _soundMixerGroup);
                _soundSource.playOnAwake = false;
                _soundSource.loop = false;
            }

            if (_musicSource == null && _musicMixerGroup != null)
            {
                _musicSource = CreateComponent("Music", _musicMixerGroup);
                _musicSource.loop = true;
            }

            Load();
            Vibration.Init();
        }

        public void Load()
        {
            if (PlayerPrefs.HasKey(MUSIC_VOLUME))
            {
                _musicVolume = PlayerPrefs.GetFloat(MUSIC_VOLUME);
            }

            if (PlayerPrefs.HasKey(SOUND_VOLUME))
            {
                _soundVolume = PlayerPrefs.GetFloat(SOUND_VOLUME);
            }

            ApplySetting(AudioType.Invalid);
            _masterMixerGroup.audioMixer.SetFloat(MasterVolume, IsEnabledAudio ? 0 : -80);
        }

        public void OnActiveAudio(bool isOn)
        {
            IsEnabledAudio = isOn;
            _masterMixerGroup.audioMixer.SetFloat(MasterVolume, isOn ? 0 : -80);
        }

        public void OnActiveVibration(bool isOn)
        {
            IsEnabledVibration = isOn;   
        }
        
        public void OnPlayVibration()
        {
#if UNITY_ANDROID
            if(IsEnabledVibration)
                Vibration.Vibrate();
#endif
        }

        private void ApplySetting(AudioType type)
        {
            // ToDo
            if (_musicMixerGroup == null && _soundMixerGroup == null) return;
            
            switch (type)
            {
                case AudioType.Music:
                    _musicMixerGroup.audioMixer.SetFloat(MUSIC_VOLUME, Mathf.Lerp(-80, 0, _musicVolume));
                    break;
                case AudioType.Sound:
                    _soundMixerGroup.audioMixer.SetFloat(SOUND_VOLUME, Mathf.Lerp(-80, 0, _soundVolume));
                    break;
                default:
                    _musicMixerGroup.audioMixer.SetFloat(MUSIC_VOLUME, Mathf.Lerp(-80, 0, _musicVolume));
                    _soundMixerGroup.audioMixer.SetFloat(SOUND_VOLUME, Mathf.Lerp(-80, 0, _soundVolume));
                    break;
            }
        }

        private AudioSource CreateComponent(string name, AudioMixerGroup mixerGroup)
        {
            GameObject source = new GameObject(name, typeof(AudioSource));
            AudioSource component = source.GetComponent<AudioSource>();
            component.outputAudioMixerGroup = mixerGroup;
            Object.DontDestroyOnLoad(source);
            return component;
        }
    }
    
    public enum AudioType
    {
        Invalid = 0,
        Music = 1,
        Sound = 2
    }
}