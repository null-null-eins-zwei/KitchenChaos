using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZZOT.KitchenChaos.Scriptable;

namespace ZZOT.KitchenChaos
{
    public class KitchenObject : MonoBehaviour
    {
        [SerializeField] private KitchenObjectSO _kitchenObjectSo;

        public KitchenObjectSO ScriptableObject => _kitchenObjectSo;
    }
}
