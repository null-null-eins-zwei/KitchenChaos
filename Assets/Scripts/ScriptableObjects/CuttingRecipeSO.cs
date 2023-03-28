using UnityEngine;

namespace ZZOT.KitchenChaos.ScriptableObjects
{
    [CreateAssetMenu()]
    public class CuttingRecipeSO : ScriptableObject
    {
        public KitchenObjectSO input;
        public KitchenObjectSO output;
        public int cuttingProgressMax = 3;
    }
}
