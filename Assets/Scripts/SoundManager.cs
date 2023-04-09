using Unity.Mathematics;
using UnityEngine;
using ZZOT.KitchenChaos.Furniture;

namespace ZZOT.KitchenChaos
{
    public class SoundManager : MonoBehaviour
    {
        [SerializeField] private AudioClipRefsSO _clips;

        private void Start()
        {
            DeliveryManager.Instance.OnRecipeSuccess += Delivery_OnRecipeSuccess;
            DeliveryManager.Instance.OnRecipeFailed += Delivery_OnRecipeFailed;
        }

        private void Delivery_OnRecipeSuccess(object sender, System.EventArgs e) =>
            PlaySound(_clips.deliverySuccess, DeliveryCounter.Instance);

        private void Delivery_OnRecipeFailed(object sender, System.EventArgs e) =>
            PlaySound(_clips.deliveryFail, DeliveryCounter.Instance);

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
