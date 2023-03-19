using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZZOT.KitchenChaos.Furniture;
using ZZOT.KitchenChaos.Scriptable;

namespace ZZOT.KitchenChaos
{
    public class KitchenObject : MonoBehaviour
    {
        [SerializeField] private KitchenObjectSO _kitchenObjectSo;

        private ClearCounter _clearCounter;

        public KitchenObjectSO ScriptableObject => _kitchenObjectSo;

        public ClearCounter GetClearCounter => _clearCounter;

        public void SetClearCounter(ClearCounter newCounter) {
            if(_clearCounter != null)
            {
                _clearCounter.ClearKitchenObject();
            }

            if (newCounter.HasKitchenObject())
            {
                Debug.LogError($"ClearCounter {newCounter} already has an KitchenObject.");
            }

            _clearCounter = newCounter;
            _clearCounter.SetKitchenObject(this);

            transform.parent = _clearCounter.GetKitchenObjectFollowTransform();
            transform.localPosition = Vector3.zero;
        }
    }
}