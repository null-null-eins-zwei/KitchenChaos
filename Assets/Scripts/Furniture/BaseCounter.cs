using System;
using UnityEngine;
using UnityEngine.EventSystems;
using ZZOT.KitchenChaos.Character;
using ZZOT.KitchenChaos.Items;

namespace ZZOT.KitchenChaos.Furniture
{
    public class BaseCounter : MonoBehaviour, IKitchenObjectParent
    {
        public static event EventHandler OnAnyObjectPlaced;
        public static void ResetStaticData()
        {
            OnAnyObjectPlaced = null;
        }

        [SerializeField] protected Transform _counterTopPoint;

        private KitchenObject _kitchenObject;

        public virtual void Interact(Player player)
        {
            Debug.LogError($"{nameof(BaseCounter)}.{nameof(Interact)}()");
        }

        public virtual void InteractAlternate(Player player)
        {
            // By default just do nothing to not confuse player
            return;
        }

        public Transform GetKitchenObjectFollowTransform() => _counterTopPoint;

        public KitchenObject GetKitchenObject() => _kitchenObject;

        public bool HasKitchenObject() => GetKitchenObject() != null;

        public void ClearKitchenObject() => SetKitchenObject(null);

        public void SetKitchenObject(KitchenObject kitchenObject)
        {
            _kitchenObject = kitchenObject;
            
            if(kitchenObject != null) 
            { 
                OnAnyObjectPlaced?.Invoke(this, EventArgs.Empty);
            }
        }

    }
}