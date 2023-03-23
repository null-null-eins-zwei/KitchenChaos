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
            var playerItem = player.GetKitchenObject();
            var counterItem = this.GetKitchenObject();

            if(counterItem != null)
            {
                player.ClearKitchenObject();
                counterItem.SetKitchenObjectParent(player);
            }

            if(playerItem != null)
            {
                this.ClearKitchenObject();
                playerItem.SetKitchenObjectParent(this);
            }
        }
    }
}