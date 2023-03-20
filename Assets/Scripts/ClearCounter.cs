using UnityEngine;
using ZZOT.KitchenChaos.Interfaces;
using ZZOT.KitchenChaos.Scriptable;
using ZZOT.KitchenChaos.User;

namespace ZZOT.KitchenChaos.Furniture
{
    public class ClearCounter : MonoBehaviour, IKitchenObjectParent
    {
        [SerializeField] private Transform _counterTopPoint;
        [SerializeField] private KitchenObjectSO _kitchenObjectSO;

        private KitchenObject _kitchenObject;

        public void Interact(Player player)
        {
            if (_kitchenObject == null)
            {
                var kitchenObjectTransform = Instantiate(
                                                original: _kitchenObjectSO.prefab,
                                                parent: _counterTopPoint);

                //kitchenObjectTransform.localPosition = Vector3.zero;

                kitchenObjectTransform.GetComponent<KitchenObject>().SetKitchenObjectParent(this);
            }
            else
            {
                _kitchenObject.SetKitchenObjectParent(player);
            }

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