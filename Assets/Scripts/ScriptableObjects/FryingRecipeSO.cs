using UnityEngine;

namespace ZZOT.KitchenChaos.ScriptableObjects
{
    [CreateAssetMenu()]
    public class FryingRecipeSO : ScriptableObject
    {
        public KitchenObjectSO input;
        public KitchenObjectSO output;
        public float fryingTimerSecMax = 3f;
    }
}
