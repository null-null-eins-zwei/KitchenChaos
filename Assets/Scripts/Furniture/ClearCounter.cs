using UnityEngine;
using ZZOT.KitchenChaos.Character;
using ZZOT.KitchenChaos.Items;
using ZZOT.KitchenChaos.ScriptableObjects;

namespace ZZOT.KitchenChaos.Furniture
{
    public class ClearCounter : BaseCounter
    {
        [SerializeField] protected KitchenObjectSO _kitchenObjectSO;

        public override void Interact(Player player)
        {
            var counterItem = this.GetKitchenObject();
            var counterHasItem = this.HasKitchenObject();
            var counterHasPlate = this.HasPlate();

            var playerItem = player.GetKitchenObject();
            var playerHasItem = player.HasKitchenObject();
            var playerHasPlate = player.HasPlate();

            if (playerHasPlate
                    && counterHasItem
                    && !counterHasPlate)
            {
                var plate = playerItem as PlateKitchenObject;

                if (plate.TryAddIngridient(counterItem.KitchenObjectSo))
                {
                    counterItem.DestroySelf();
                }
            }
            else if (counterHasPlate
                        && playerHasItem
                        && !playerHasPlate)
            {
                var plate = counterItem as PlateKitchenObject;

                if (plate.TryAddIngridient(playerItem.KitchenObjectSo))
                {
                    playerItem.DestroySelf();
                }
            }
            else
            {
                if (counterHasItem)
                {
                    player.ClearKitchenObject();
                    counterItem.SetKitchenObjectParent(player);
                }

                if (playerHasItem)
                {
                    this.ClearKitchenObject();
                    playerItem.SetKitchenObjectParent(this);
                }
            }
        }
    }
}