using Unity.VisualScripting;
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

    public static class KitchenObjectParentExtensions
    {
        public static bool HasPlate(this IKitchenObjectParent kop) =>
            kop.HasKitchenObject() 
            && kop.GetKitchenObject() is PlateKitchenObject;
    }
}
