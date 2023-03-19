using UnityEngine;
using ZZOT.KitchenChaos.Scriptable;

namespace ZZOT.KitchenChaos.Furniture
{
    public class ClearCounter : MonoBehaviour
    {
        [SerializeField] private Transform _counterTopPoint;
        [SerializeField] private KitchenObjectSO _kitchenObjectSO;
        [SerializeField] private ClearCounter _secontClearCounter;
        [SerializeField] private bool _testing;

        private KitchenObject _kitchenObject;

        private void Update()
        {
            if(_testing && Input.GetKeyDown(KeyCode.T)) 
            {
                if(_kitchenObject != null)
                {
                    _kitchenObject.SetClearCounter(_secontClearCounter);
                }
            }
        }

        public void Interact()
        {
            if(_kitchenObject == null)
            {
                var kitchenObjectTransform = Instantiate(
                                                original: _kitchenObjectSO.prefab,
                                                parent: _counterTopPoint);

                //kitchenObjectTransform.localPosition = Vector3.zero;

                kitchenObjectTransform.GetComponent<KitchenObject>().SetClearCounter(this);
            }

        }

        public Transform GetKitchenObjectFollowTransform()
        {
            return _counterTopPoint;    
        }

        public void SetKitchenObject(KitchenObject kitchenObject)
        {
            _kitchenObject = kitchenObject;
        }

        public KitchenObject GetKitchenObject() => _kitchenObject;

        public void ClearKitchenObject()
        {
            _kitchenObject = null;
        }

        public bool HasKitchenObject() => _kitchenObject != null;
    }
}