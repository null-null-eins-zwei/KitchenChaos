using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZZOT.KitchenChaos.Scriptable
{
    [CreateAssetMenu()]
    public class KitchenObjectSO : ScriptableObject
    {
        public Transform prefab;
        public Sprite sprite;
        public string objectName;
    }
}
