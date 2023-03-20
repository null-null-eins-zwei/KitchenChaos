
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZZOT.KitchenChaos.Interfaces
{
    public interface IKitchenObjectParent
    {
        Transform GetKitchenObjectFollowTransform();
        KitchenObject GetKitchenObject();
        bool HasKitchenObject();
        void ClearKitchenObject();
        void SetKitchenObject(KitchenObject kitchenObject);
    }
}
