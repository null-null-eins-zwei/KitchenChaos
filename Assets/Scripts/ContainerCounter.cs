using System;
using UnityEngine;
using ZZOT.KitchenChaos.Scriptable;
using ZZOT.KitchenChaos.User;

namespace ZZOT.KitchenChaos.Furniture
{
    public class ContainerCounter : BaseCounter
    {
        public event EventHandler OnPlayerGrabbedObject;

        [SerializeField] protected KitchenObjectSO _kitchenObjectSO;

        public override void Interact(Player player)
        {
            if (player.HasKitchenObject())
            {
                return;
            }

            var kitchenObjectTransform = Instantiate(
                                            original: _kitchenObjectSO.prefab,
                                            parent: _counterTopPoint);

            kitchenObjectTransform.GetComponent<KitchenObject>().SetKitchenObjectParent(player);

            OnPlayerGrabbedObject?.Invoke(this, EventArgs.Empty);
        }
    }
}
