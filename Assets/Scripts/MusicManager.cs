using UnityEngine;

namespace ZZOT.KitchenChaos
{
    public class MusicManager : MonoBehaviour
    {
        private const string PLAYER_PREFS_MUSIC_VOLUME = "PLAYER_PREFS_MUSIC_VOLUME";

        public static MusicManager Instance;

        private AudioSource _audioSource;
        private float _volume = 0.3f;

        private void Awake()
        {
            Instance = this;
            _audioSource = GetComponent<AudioSource>();
            RestoreVolume();
        }

        public void ChangeVolume()
        {
            _volume += 0.1f;
            if (_volume > 1.01f)
            {
                _volume = 0f;
            }

            _audioSource.volume = _volume;
            SaveVolume();
        }

        private void SaveVolume()
        {
            PlayerPrefs.SetFloat(PLAYER_PREFS_MUSIC_VOLUME, _volume);
            PlayerPrefs.Save();
        }

        private void RestoreVolume()
        {
            _volume = PlayerPrefs.GetFloat(PLAYER_PREFS_MUSIC_VOLUME, defaultValue: 0.3f);
            _audioSource.volume = Mathf.Clamp01(_volume);
        }

        public float GetVolume() => _volume;
    }
}
