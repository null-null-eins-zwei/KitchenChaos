using UnityEngine;
using ZZOT.KitchenChaos.Scriptable;
using ZZOT.KitchenChaos.User;

namespace ZZOT.KitchenChaos.Furniture
{
    public class ClearCounter : BaseCounter
    {
        [SerializeField] protected KitchenObjectSO _kitchenObjectSO;

        public override void Interact(Player player)
        {
            if (!HasKitchenObject())
            {
                var kitchenObjectTransform = Instantiate(
                                                original: _kitchenObjectSO.prefab,
                                                parent: _counterTopPoint);

                //kitchenObjectTransform.localPosition = Vector3.zero;

                kitchenObjectTransform.GetComponent<KitchenObject>().SetKitchenObjectParent(this);
            }
            else
            {
                GetKitchenObject().SetKitchenObjectParent(player);
            }
        }
    }
}