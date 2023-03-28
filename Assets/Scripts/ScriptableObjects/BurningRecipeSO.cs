using UnityEngine;

namespace ZZOT.KitchenChaos.ScriptableObjects
{
    [CreateAssetMenu()]
    public class BurningRecipeSO : ScriptableObject
    {
        public KitchenObjectSO input;
        public KitchenObjectSO output;
        public float burningTimerSecMax = 3f;
    }
}
