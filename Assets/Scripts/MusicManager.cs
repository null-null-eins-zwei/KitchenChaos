using UnityEngine;

namespace ZZOT.KitchenChaos
{
    public class MusicManager : MonoBehaviour
    {
        public static MusicManager Instance;

        private AudioSource _audioSource;
        private float _volume = 0.3f;

        private void Awake()
        {
            Instance = this;
            _audioSource = GetComponent<AudioSource>();
            _audioSource.volume = _volume;
        }

        public void ChangeVolume()
        {
            _volume += 0.1f;
            if (_volume > 1.01f)
            {
                _volume = 0f;
            }

            _audioSource.volume = _volume;
        }

        public float GetVolume() => _volume;
    }
}
