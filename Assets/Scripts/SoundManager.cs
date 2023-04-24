using UnityEngine;
using ZZOT.KitchenChaos.Character;
using ZZOT.KitchenChaos.Furniture;

namespace ZZOT.KitchenChaos
{
    public class SoundManager : MonoBehaviour
    {
        public static SoundManager Instance;

        [SerializeField] private AudioClipRefsSO _sounds;

        private float _volume = 1.0f;

        private void Awake()
        {
            Instance = this;
        }

        private void Start()
        {
            DeliveryManager.Instance.OnRecipeSuccess += Delivery_OnRecipeSuccess;
            DeliveryManager.Instance.OnRecipeFailed += Delivery_OnRecipeFailed;

            Player.Instance.OnPickedUpSomething += Player_OnPickedUpSomething;

            BaseCounter.OnAnyObjectPlaced += AnyCounter_OnAnyObjectPlaced;
            CuttingCounter.OnAnyCut += CuttingCounter_OnAnyCut;
            TrashCounter.OnAnyObjectTrash += Trash_OnAnyObjectThrown;
        }

        public void PlayFootstepSound(Vector3 position) => PlaySound(_sounds.footstep, position);

        private void Trash_OnAnyObjectThrown(object sender, System.EventArgs e)
        {
            var counter = sender as TrashCounter;
            PlaySound(_sounds.trash, counter);
        }

        private void AnyCounter_OnAnyObjectPlaced(object sender, System.EventArgs e)
        {
            var counter = sender as BaseCounter;
            PlaySound(_sounds.objectDrop, counter);
        }

        private void Player_OnPickedUpSomething(object sender, System.EventArgs e)
        {
            var player = sender as Player;
            PlaySound(_sounds.objectPickup, player);
        }

        private void CuttingCounter_OnAnyCut(object sender, System.EventArgs e)
        {
            var senderCounter = sender as CuttingCounter;
            PlaySound(_sounds.chop, senderCounter);
        }

        private void Delivery_OnRecipeSuccess(object sender, System.EventArgs e) =>
            PlaySound(_sounds.deliverySuccess, DeliveryCounter.Instance);

        private void Delivery_OnRecipeFailed(object sender, System.EventArgs e) =>
            PlaySound(_sounds.deliveryFail, DeliveryCounter.Instance);

        private void PlaySoundOnCamera(AudioClip[] soundLib, float volumeMultiplyer = 1f) =>
            PlaySound(soundLib, Camera.main, volumeMultiplyer);

        private void PlaySound(AudioClip[] soundLib, Component component, float volumeMultiplyer = 1f) =>
            PlaySound(soundLib, component.transform.position, volumeMultiplyer);

        private void PlaySound(AudioClip[] soundLib, Vector3 position, float volumeMultiplyer = 1f)
        {
            var sound = GetRandomOne(soundLib);
            AudioSource.PlayClipAtPoint(
                sound,
                position,
                Mathf.Clamp01(_volume * volumeMultiplyer));
        }

        private AudioClip GetRandomOne(AudioClip[] clips)
        {
            var i = UnityEngine.Random.Range(0, clips.Length);
            return clips[i];
        }

        public void ChangeVolume()
        {
            _volume += 0.1f;
            if (_volume > 1.01f)
            {
                _volume = 0f;
            }

            return;
        }

        public float GetVolume() => _volume;
    }
}
