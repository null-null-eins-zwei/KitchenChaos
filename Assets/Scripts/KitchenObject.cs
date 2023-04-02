using UnityEngine;
using ZZOT.KitchenChaos.Interfaces;
using ZZOT.KitchenChaos.ScriptableObjects;

namespace ZZOT.KitchenChaos
{
    public class KitchenObject : MonoBehaviour
    {
        [SerializeField] private KitchenObjectSO _kitchenObjectSo;

        private IKitchenObjectParent _kitchenObjectParent;

        public KitchenObjectSO KitchenObjectSo => _kitchenObjectSo;

        public IKitchenObjectParent KitchenObjectParent => _kitchenObjectParent;

        public void SetKitchenObjectParent(IKitchenObjectParent newParent)
        {
            if (_kitchenObjectParent != null
                    && _kitchenObjectParent.GetKitchenObject() == this)
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

        public void DestroySelf()
        {
            _kitchenObjectParent.ClearKitchenObject();
            Destroy(gameObject);
        }

        public bool TryGetPlate(out PlateKitchenObject plateKitchenObject)
        {
            if(this is PlateKitchenObject)
            {
                plateKitchenObject = this as PlateKitchenObject;
                return true;
            }

            plateKitchenObject = null;
            return false;
        }

        public static KitchenObject SpawnKitchenObject(KitchenObjectSO kitchenObjectSo, IKitchenObjectParent parent)
        {
            var kitchenObjectTransform = Instantiate(kitchenObjectSo.prefab);

            var kitchenObject = kitchenObjectTransform.GetComponent<KitchenObject>();
            kitchenObject.SetKitchenObjectParent(parent);

            return kitchenObject;
        }
    }
}