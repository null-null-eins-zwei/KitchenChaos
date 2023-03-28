using UnityEngine;

namespace ZZOT.KitchenChaos.ScriptableObjects
{
    [CreateAssetMenu()]
    public class KitchenObjectSO : ScriptableObject
    {
        public Transform prefab;
        public Sprite sprite;
        public string objectName;
    }
}
