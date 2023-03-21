using UnityEngine;
using ZZOT.KitchenChaos.Interfaces;
using ZZOT.KitchenChaos.User;

namespace ZZOT.KitchenChaos.Furniture
{
    public class BaseCounter : MonoBehaviour, IKitchenObjectParent
    {
        [SerializeField] protected Transform _counterTopPoint;

        private KitchenObject _kitchenObject;

        public virtual void Interact(Player player)
        {
            Debug.LogError($"{nameof(BaseCounter)}.{nameof(Interact)}()");
        }

        public Transform GetKitchenObjectFollowTransform() => _counterTopPoint;

        public KitchenObject GetKitchenObject() => _kitchenObject;

        public bool HasKitchenObject() => GetKitchenObject() != null;

        public void ClearKitchenObject() => SetKitchenObject(null);

        public void SetKitchenObject(KitchenObject kitchenObject)
        {
            _kitchenObject = kitchenObject;
        }

    }
}