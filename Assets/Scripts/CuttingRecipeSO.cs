using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZZOT.KitchenChaos.Scriptable
{
    [CreateAssetMenu()]
    public class CuttingRecipeSO : ScriptableObject
    {
        public KitchenObjectSO input;
        public KitchenObjectSO output;
        public int cuttingProgressMax = 3;
    }
}
