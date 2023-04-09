using Unity.Mathematics;
using UnityEngine;
using ZZOT.KitchenChaos.Character;
using ZZOT.KitchenChaos.Furniture;

namespace ZZOT.KitchenChaos
{
    public class SoundManager : MonoBehaviour
    {
        public static SoundManager Instance;

        [SerializeField] private AudioClipRefsSO _sounds;


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

        private void PlaySoundOnCamera(AudioClip[] soundLib, float volume = 1f) =>
            PlaySound(soundLib, Camera.main, volume);

        private void PlaySound(AudioClip[] soundLib, Component component, float volume = 1f) =>
            PlaySound(soundLib, component.transform.position, volume);

        private void PlaySound(AudioClip[] soundLib, Vector3 position, float volume = 1f)
        {
            var sound = GetRandomOne(soundLib);
            AudioSource.PlayClipAtPoint(sound, position, volume);
        }

        private AudioClip GetRandomOne(AudioClip[] clips)
        {
            var i = UnityEngine.Random.Range(0, clips.Length);
            return clips[i];
        }
    }
}
