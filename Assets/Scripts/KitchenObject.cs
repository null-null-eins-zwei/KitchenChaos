using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZZOT.KitchenChaos.Furniture;
using ZZOT.KitchenChaos.Interfaces;
using ZZOT.KitchenChaos.Scriptable;

namespace ZZOT.KitchenChaos
{
    public class KitchenObject : MonoBehaviour
    {
        [SerializeField] private KitchenObjectSO _kitchenObjectSo;

        private IKitchenObjectParent _kitchenObjectParent;

        public KitchenObjectSO GetKitchenObjectSo => _kitchenObjectSo;

        public IKitchenObjectParent GetKitchenObjectParent => _kitchenObjectParent;

        public void SetKitchenObjectParent(IKitchenObjectParent newParent) {
            if(_kitchenObjectParent != null)
            {
                _kitchenObjectParent.ClearKitchenObject();
            }

            if (newParent.HasKitchenObject())
            {
                Debug.LogError($"{nameof(IKitchenObjectParent)} {newParent} already has an KitchenObject.");
            }

            _kitchenObjectParent = newParent;
            _kitchenObjectParent.SetKitchenObject(this);

            transform.parent = _kitchenObjectParent.GetKitchenObjectFollowTransform();
            transform.localPosition = Vector3.zero;
        }
    }
}