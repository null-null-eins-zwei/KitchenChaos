using System;
using UnityEngine;
using ZZOT.KitchenChaos.ScriptableObjects;
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

            KitchenObject.SpawnKitchenObject(_kitchenObjectSO, player);

            OnPlayerGrabbedObject?.Invoke(this, EventArgs.Empty);
        }
    }
}
